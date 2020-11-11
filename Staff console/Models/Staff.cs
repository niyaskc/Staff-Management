using System;
using System.Collections.Generic;
using System.Text;

namespace Staff_console
{
    abstract class Staff
    {
        public int Id { get; set; }
        public String Name { get; set; }

        public abstract String GetPrintable();


        protected Staff(int id)
        {
            Id = id;
        }
    }
}
