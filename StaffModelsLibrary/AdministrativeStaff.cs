using System;
using System.Collections.Generic;
using System.Text;

namespace StaffModelsLibrary
{
    public class AdministrativeStaff : Staff
    {
        #region Class Member Variables
        public String Position { get; set; }

        public static readonly String HeadLinePrintable = "Id\t\tName\t\tPosition\t\tType";
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

        #region Overriding Methods

        public override string GetPrintable()
        {
            return $"{Id}\t\t{Name}\t\t{Position}\t\t{StaffType}";
        }

        public override string GetHeadLinePrintable()
        {
            return HeadLinePrintable;
        }
        #endregion

        public AdministrativeStaff(int id) : base(id, StaffType.administrativeStaff)
        {

        }
    }
}
