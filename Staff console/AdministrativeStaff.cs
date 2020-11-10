using System;
using System.Collections.Generic;
using System.Text;

namespace Staff_console
{
    class AdministrativeStaff : Staff
    {
        public String Position { get; set; }

        public AdministrativeStaff(int id, string name, string location, string gender, string position): base(id, name, location, gender)
        {
            Position = position;
        }
    }
}
