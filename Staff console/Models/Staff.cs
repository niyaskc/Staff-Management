
using System;
using System.Collections.Generic;
using System.Text;

namespace Staff_console.Models
{
    abstract class Staff
    {
        public int Id { get; set; }
        public String Name { get; set; }

        public StaffType StaffType { get; }

        public abstract Staff Add();

        public abstract void Update();

        public abstract void Delete();

        public abstract void View();

        public static void ViewAll(List<Staff> lstStaffs)
        {
            Console.WriteLine("\n->View All Staffs:-\n");

            //Print Teaching Staffs
            ViewAllByType(lstStaffs, StaffType.teachingStaff);
            //Print Administrative Staffs
            ViewAllByType(lstStaffs, StaffType.administrativeStaff);
            //Print Support Staffs
            ViewAllByType(lstStaffs, StaffType.supportStaff);
        }

        private static void ViewAllByType(List<Staff> lstStaffs, StaffType staffType)
        {
            List<Staff> lstStaffFilteredByType = lstStaffs.FindAll(staff => staff.StaffType == staffType);
            Console.WriteLine("Printing "+ staffType + " :");
            if (lstStaffFilteredByType.Count == 0)
            {
                Console.WriteLine("---Empty---\n");
            }
            else
            {
                Console.WriteLine(lstStaffFilteredByType[0].GetHeadLinePrintable());
                Console.WriteLine("-----------------------------------------------------------------------------------------");
                foreach (Staff s in lstStaffFilteredByType)
                {
                    Console.WriteLine(s.GetPrintable());

                }
                Console.WriteLine("------------------------------------------------------------------------------------------\n");
            }
        }


        public abstract String GetPrintable();

        public abstract String GetHeadLinePrintable();


        public virtual void ClearValues()
        {
            Name = "";
        }

        public virtual void GetConsoleInput()
        {
            Console.WriteLine("Enter Name: ");
            Name = Console.ReadLine();
        }


        protected Staff(int id, StaffType staffType)
        {
            Id = id;
            StaffType = staffType;
        }
    }
}
