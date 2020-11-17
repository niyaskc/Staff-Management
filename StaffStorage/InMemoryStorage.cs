using StaffModelsLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace StaffStorage
{
    public class InMemoryStorage : IRepository
    {
        protected List<Staff> staffs;

        public InMemoryStorage()
        {
            this.staffs = new List<Staff>();
        }

        public bool AddStaff(Staff staff)
        {
            staff.Id = staffs.Any() ? staffs.Max(s => s.Id) + 1 : 1;
            staffs.Add(staff);
            return true;
        }

        public bool DeleteStaff(int id)
        {
            return staffs.Remove(staffs.Find(s => s.Id == id));
        }

        public bool UpdateStaff(int id, Staff staff)
        {
            int indexForUpdate = staffs.FindIndex(s => s.Id == id);
            if (indexForUpdate < 0) return false;
            staffs[indexForUpdate] = staff;
            return true;
        }

        public List<Staff> ViewAllStaff()
        {
            return staffs;
        }

        public Staff ViewStaff(int id)
        {
            return staffs.Find(s => s.Id == id);
        }
    }
}
