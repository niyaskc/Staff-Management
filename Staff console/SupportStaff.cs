using System;
using System.Collections.Generic;
using System.Text;

namespace Staff_console
{
    class SupportStaff : Staff
    {
        public String Role { get; set; }

        public SupportStaff(int id, string name, string location, string gender, string role) : base(id, name, location, gender)
        {
            Role = role;
        }
    }
}
