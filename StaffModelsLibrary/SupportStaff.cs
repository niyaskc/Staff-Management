using System;
using System.Collections.Generic;
using System.Text;

namespace StaffModelsLibrary
{
    public class SupportStaff : Staff
    {
        #region Class Member Variables
        public String Role { get; set; }
        #endregion

        #region Validation Methods
        
        //Validate Role
        public bool ValidateRole(String role)
        {
            if (role?.Length > 4)
            {
                return true;
            }
            return false;
        }

        #endregion

        public SupportStaff() : base(StaffType.supportStaff)
        {

        }
    }
}
