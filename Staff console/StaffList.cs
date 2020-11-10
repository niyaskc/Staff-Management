using System;
using System.Collections.Generic;
using System.Text;

namespace Staff_console
{
    class StaffList
    {
        public List<TeachingStaff> TeachingStaffs { get; set; }
        public List<AdministrativeStaff> AdministrativeStaffs { get; set;}
        public List<SupportStaff> SupportStaffs { get; set;}

        private int _staffId = 1;

        public StaffList()
        {
            TeachingStaffs = new List<TeachingStaff>();
            AdministrativeStaffs = new List<AdministrativeStaff>();
            SupportStaffs = new List<SupportStaff>();
        }

        public int AddStaff(Staff staff, bool isAutoIdEnabled)
        {
            //id setted
            if (isAutoIdEnabled)
            {
                staff.Id = _staffId;
                _staffId++;
            }

            if(staff.GetType() == typeof(TeachingStaff))
            {
                TeachingStaffs.Add((TeachingStaff)staff);
            }else if (staff.GetType() == typeof(AdministrativeStaff))
            {
                AdministrativeStaffs.Add((AdministrativeStaff)staff);
            }else if (staff.GetType() == typeof(SupportStaff))
            {
                SupportStaffs.Add((SupportStaff)staff);
            }
            else {
                return -1;
            }

            return staff.Id;
        }

    }
}
