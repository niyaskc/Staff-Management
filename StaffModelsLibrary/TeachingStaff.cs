using System;
using System.Collections.Generic;
using System.Text;

namespace StaffModelsLibrary
{
    public class TeachingStaff : Staff
    {
        #region Class Member Variables
        public String SubjectName { get; set; }

        public static readonly String HeadLinePrintable = "Id\t\tName\t\tSubject Name\t\tType";
        #endregion

        #region Validation Methods

        //Validate Subject Name
        public bool ValidateSubjectName(String subjectName)
        {
            if (subjectName?.Length > 1)
            {
                return true;
            }
            return false;
        }
        #endregion

        #region Overriding Methods

        public override string GetPrintable()
        {
            return $"{Id}\t\t{Name}\t\t{SubjectName}\t\t{StaffType}";
        }

        public override string GetHeadLinePrintable()
        {
            return HeadLinePrintable;
        }
        #endregion

        public TeachingStaff(int id) : base(id, StaffType.teachingStaff)
        {

        }
    }
}
