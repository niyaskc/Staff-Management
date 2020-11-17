using System;
using System.Collections.Generic;
using System.Text;

namespace StaffModelsLibrary
{
    public class AdministrativeStaff : Staff
    {
        #region Class Member Variables
        public String Position { get; set; }
        #endregion

        #region Validation Methods

        //Validate Position
        public bool ValidatePosition(String position)
        {
            if (position?.Length > 2)
            {
                return true;
            }
            return false;
        }
        #endregion

        public AdministrativeStaff() : base(StaffType.administrativeStaff)
        {

        }
    }
}
