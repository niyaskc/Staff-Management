using System;
using System.Collections.Generic;
using System.Text;

namespace Staff_console
{
    class TeachingStaff : Staff
    {
        public String SubjectName { get; set; }

        public static readonly String HeadLinePrintable = "Id\t\tName\t\tSubject Name";

        public TeachingStaff(int id) : base(id)
        {

        }

        public override string GetPrintable()
        {
            return $"{Id}\t\t{Name}\t\t{SubjectName}";
        }
    }
}
