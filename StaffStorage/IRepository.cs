using StaffModelsLibrary;
using System;
using System.Collections.Generic;
using System.Text;

namespace StaffStorage
{
    public interface IRepository
    {
        bool AddStaff(Staff staff);
        bool UpdateStaff(int id, Staff staff);
        bool DeleteStaff(int id);
        Staff ViewStaff(int id);
        List<Staff> ViewAllStaff();

    }
}
