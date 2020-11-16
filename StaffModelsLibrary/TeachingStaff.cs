using System;
using System.Collections.Generic;
using System.Text;

namespace StaffModelsLibrary
{
    public class TeachingStaff : Staff
    {
        #region Class Member Variables
        public String SubjectName { get; set; }
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

        public TeachingStaff(int id) : base(id, StaffType.teachingStaff)
        {

        }
    }
}
