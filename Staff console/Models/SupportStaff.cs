using System;
using System.Collections.Generic;
using System.Text;

namespace Staff_console
{
    class SupportStaff : Staff
    {
        public String Role { get; set; }

        public static readonly String HeadLinePrintable = "Id\t\tName\t\tRole";

        public SupportStaff(int id) : base(id)
        {
            
        }

        public override string GetPrintable()
        {
            return $"{Id}\t\t{Name}\t\t{Role}";
        }
    }
}
