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
            }
            return NotFound();
        }

        [HttpGet("{id:int}")]
        public IActionResult GetStaff(int id)
        {
            Staff staff = _sqlDbStorage.ViewStaff(id);
            if(staff != null)
            {
                return Ok(staff);
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult CreateStaff(JObject staffJson)
        {
            Staff staff = _GetStaffFromJson(staffJson);

            if (staff == null)
            {
                return NoContent();
            }
            bool isCreated = _sqlDbStorage.AddStaff(staff);
            if (isCreated)
            {
                return StatusCode(201);
            }
            return BadRequest();
        }
            
        [HttpPut("{id:int}")]
        public IActionResult UpdateStaff(int id, JObject staffJson)
        {
            Staff staff = _GetStaffFromJson(staffJson);

            if (staff == null)
            {
                return NoContent();
            }
            bool isUpdated = _sqlDbStorage.UpdateStaff(id, staff);
            if (isUpdated)
            {
                return Ok();
            }
            if (_sqlDbStorage.ViewStaff(id) != null )
            {
                return NoContent();
            }
            return NotFound();
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteStaff(int id)
        {
            bool isDeleted = _sqlDbStorage.DeleteStaff(id);
            if (isDeleted)
            {
                return Ok();
            }
            return NotFound();
        }

        private Staff _GetStaffFromJson(JObject staffJson)
        {
            try
            {
                switch ((int)staffJson["StaffType"])
                {
                    case (int)StaffType.teachingStaff:
                        staffJson.ToObject<TeachingStaff>();
                        break;
                    case (int)StaffType.administrativeStaff:
                        staffJson.ToObject<AdministrativeStaff>();
                        break;
                    case (int)StaffType.supportStaff:
                        staffJson.ToObject<SupportStaff>();
                        break;
                }
            }
            catch{}
            return null;
        }
    }
}
