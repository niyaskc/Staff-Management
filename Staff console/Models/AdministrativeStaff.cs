using System;
using System.Collections.Generic;
using System.Text;

namespace Staff_console
{
    class AdministrativeStaff : Staff
    {
        public String Position { get; set; }

        public static readonly String HeadLinePrintable = "Id\t\tName\t\tPosition";

        public AdministrativeStaff(int id): base(id)
        {
        }

        public override string GetPrintable()
        {
            return $"{Id}\t\t{Name}\t\t{Position}";
        }
    }
}
