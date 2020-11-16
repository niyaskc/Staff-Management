using System;
using System.Collections.Generic;
using System.Text;

namespace StaffModelsLibrary
{
    public class SupportStaff : Staff
    {
        #region Class Member Variables
        public String Role { get; set; }

        public static readonly String HeadLinePrintable = "Id\t\tName\t\tRole\t\tType";
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

        #region Overriding Methods

        public override string GetPrintable()
        {
            return $"{Id}\t\t{Name}\t\t{Role}\t\t{StaffType}";
        }

        public override string GetHeadLinePrintable()
        {
            return HeadLinePrintable;
        }
        #endregion


        public SupportStaff(int id) : base(id, StaffType.supportStaff)
        {

        }
    }
}
