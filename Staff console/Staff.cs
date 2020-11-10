using System;
using System.Collections.Generic;
using System.Text;

namespace Staff_console
{
    class Staff
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public String Location { get; set; }
        public String Gender { get; set; }

        public Staff(int id, string name, string location, string gender)
        {
            Id = id;
            Name = name;
            Location = location;
            Gender = gender;
        }
    }
}
