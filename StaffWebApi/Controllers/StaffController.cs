using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using StaffModelsLibrary;
using StaffStorage;

namespace StaffWebApi.Controllers
{
    [ApiController]
    [Route("staff/")]
    public class StaffController : ControllerBase
    {

        private readonly ILogger<StaffController> _logger;
        private SqlDbStorage _sqlDbStorage;

        public StaffController(ILogger<StaffController> logger)
        {
            _logger = logger;
            _sqlDbStorage = new SqlDbStorage();
        }

        [HttpGet]
        public IActionResult GetStaffByType(String type)
        {
            List<Staff> staffs = _sqlDbStorage.ViewAllStaff();
            if (staffs?.Count > 0)
            {
                List<Staff> resStafs;
                switch (type)
                {
                    case "teaching":
                        resStafs = staffs.FindAll(s => s.StaffType == StaffType.teachingStaff);
                        break;

                    case "admin":
                        resStafs = staffs.FindAll(s => s.StaffType == StaffType.administrativeStaff);
                        break;

                    case "support":
                        resStafs = staffs.FindAll(s => s.StaffType == StaffType.supportStaff);
                        break;

                    default:
                        return NotFound();
                }
                if (resStafs.Count > 0) {
                    return Ok(resStafs);
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("{id:int}")]
        public IActionResult GetStaff(int id)
        {
            Staff staff = _sqlDbStorage.ViewStaff(id);
            if(staff != null)
            {
                return Ok(staff);
            }
            else
            {
                return NotFound();
            }
        }

        [Route("pages/")]
        [HttpGet]
        public IActionResult GetStaffByTypeWithPagination(String type, int pageNo, int pageSize, String sortByField, String sortOrder)
        {
            Dictionary<string, object> resStafs;
            switch (type)
            {
                case "teaching":
                    resStafs = _sqlDbStorage.GetStaffsByPagination(StaffType.teachingStaff, pageNo, pageSize, sortByField,sortOrder);
                    break;

                case "admin":
                    resStafs = _sqlDbStorage.GetStaffsByPagination(StaffType.administrativeStaff, pageNo, pageSize, sortByField, sortOrder);
                    break;

                case "support":
                    resStafs = _sqlDbStorage.GetStaffsByPagination(StaffType.supportStaff, pageNo, pageSize, sortByField, sortOrder);
                    break;

                default:
                    return NotFound();
            }
            if (((List<Staff>)resStafs["staffs"]).Count > 0)
            {
                return Ok(resStafs);
            }
            else
            {
                return NotFound();
            }
        }


        [HttpPost]
        public IActionResult CreateStaff(JObject staffJson)
        {
            Staff staff = _GetStaffFromJson(staffJson);

            if (staff == null)
            {
                return BadRequest();
            }
            bool isCreated = _sqlDbStorage.AddStaff(staff);
            if (isCreated)
            {
                return StatusCode(201);
            }
            else
            {
                return BadRequest();
            }
        }
            
        [HttpPut("{id:int}")]
        public IActionResult UpdateStaff(int id, JObject staffJson)
        {
            Staff staff = _GetStaffFromJson(staffJson);
           
            if (staff == null || id != staff.Id)
            {
                return BadRequest();
            }
            bool isUpdated = _sqlDbStorage.UpdateStaff(id, staff);
            if (isUpdated)
            {
                return Ok();
            }else if(_sqlDbStorage.ViewStaff(id) == null)
            {
                return NotFound();
            }
            else
            {
                return BadRequest();
            }
            
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteStaff(int id)
        {
            bool isDeleted = _sqlDbStorage.DeleteStaff(id);
            if (isDeleted)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        private Staff _GetStaffFromJson(JObject staffJson)
        {
            try
            {
                switch ((int)staffJson["staffType"])
                {
                    case (int)StaffType.teachingStaff:
                        return staffJson.ToObject<TeachingStaff>();
                    case (int)StaffType.administrativeStaff:
                        return staffJson.ToObject<AdministrativeStaff>();
                    case (int)StaffType.supportStaff:
                        return staffJson.ToObject<SupportStaff>();
                }
            }
            catch{}
            return null;
        }
    }
}
