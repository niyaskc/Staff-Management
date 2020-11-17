using Newtonsoft.Json;
using StaffModelsLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace StaffStorage
{
    public class JsonStorage : IRepository
    {
        private readonly String _filePath = System.AppDomain.CurrentDomain.BaseDirectory + "\\JsonRepoData.txt";
        private JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto , NullValueHandling = NullValueHandling.Ignore};

        private bool SerializeJson(List<Staff> staffs)
        {
            try
            {
                String json = JsonConvert.SerializeObject(staffs, Formatting.Indented, settings);
                File.WriteAllText(_filePath, json);
                return true;
            }
            catch(Exception e)
            {
                throw e;
            }
            
        }

        private List<Staff> DeSerializeJson()
        {
            try
            {
                if (File.Exists(_filePath))
                {
                    List<Staff> staffs = JsonConvert.DeserializeObject<List<Staff>>(File.ReadAllText(_filePath), settings);
                    return staffs;
                }
            }
            catch(Exception e) { throw e; }

            return null;
        }

        public bool AddStaff(Staff staff)
        {
            List<Staff> staffs = DeSerializeJson();
            if (staffs == null)
            {
                staffs = new List<Staff>();
            }
            staff.Id = staffs.Any() ? staffs.Max(s => s.Id) + 1 : 1;
            staffs.Add(staff);
            return SerializeJson(staffs);
        }

        public bool UpdateStaff(int id, Staff staff)
        {
            List<Staff> staffs = DeSerializeJson();
            if (staffs == null || staff == null)
            {
                return false;
            }
            int indexForUpdate = staffs.FindIndex(s => s.Id == id);
            if (indexForUpdate < 0) return false;
            staffs[indexForUpdate] = staff;
            return SerializeJson(staffs);
        }

        public bool DeleteStaff(int id)
        {
            List<Staff> staffs = DeSerializeJson();
            bool isFound = staffs.Remove(staffs.Find(s => s.Id == id));
            if (!isFound) return false;
            return SerializeJson(staffs);
        }

        public Staff ViewStaff(int id)
        {
            return DeSerializeJson()?.Find(s => s.Id == id);
        }

        public List<Staff> ViewAllStaff()
        {
            return DeSerializeJson();
        }
    }
}
