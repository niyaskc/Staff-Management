using System;
using System.Collections.Generic;
using System.Text;

namespace Staff_console
{
    class TeachingStaff : Staff
    {
        public String SubjectName { get; set; }
        public String Dept { get; set; }

        public TeachingStaff(int id, string name, string location, string gender, string subjectName, string dept) : base(id, name, location, gender)
        {
            SubjectName = subjectName;
            Dept = dept;
            
        }
    }
}
