using StaffModelsLibrary;
using System;
using System.Collections.Generic;
using System.Text;

namespace StaffStorage
{
    public class InMemoryStorage : IRepository
    {
        List<Staff> staffs;
        private static int _staffId = 0;

        public InMemoryStorage()
        {
            this.staffs = new List<Staff>();
        }

        public bool AddStaff(Staff staff)
        {
            staffs.Add(staff);
            return true;
        }

        public bool DeleteStaff(int id)
        {
            return staffs.Remove(staffs.Find(s => s.Id == id));
        }

        public bool UpdateStaff(int id, Staff staff)
        {
            Staff oldStaff = staffs.Find(s => s.Id == id);
            oldStaff = staff;
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
