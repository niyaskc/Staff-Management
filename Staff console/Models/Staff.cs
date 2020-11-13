
using System;
using System.Collections.Generic;
using System.Text;

public delegate bool ValidationFunction<T>(T inputForValidation);
public delegate bool IsEmpty<T>(T item);

namespace Staff_console.Models
{
    abstract class Staff
    {
        #region Class Member Variables
        public int Id { get; set; }
        public String Name { get; set; }

        public StaffType StaffType { get; }
        #endregion

        #region Abstract Methods
        public abstract Staff Add();

        public abstract void Update();

        public abstract void View();

        public abstract String GetPrintable();

        public abstract String GetHeadLinePrintable();
        #endregion

        #region Validation Methods

        //Validate Name
        private bool ValidateName(String name)
        {
            if (name?.Length > 3)
            {
                return true;
            }
            Console.WriteLine("\n!!! Invalid Name. Minimum 4 Letters\n");
            return false;
        }
        #endregion

        #region Virtual Methods
        public virtual void GetConsoleInput()
        {
            this.Name = ReadAndValidateInput<String>(this.Name, "Enter Name: ", s => String.IsNullOrEmpty(s), ValidateName); ;
        }
        #endregion

        #region Static Methods
        public static void ViewAll(List<Staff> lstStaffs)
        {
            Console.WriteLine("\n->View All Staffs:-");
            if (lstStaffs.Count == 0)
            {
                Console.WriteLine("\n---No Staffs---\n");
                return;
            }

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

            if (lstStaffFilteredByType.Count != 0)
            {
                Console.WriteLine("Printing " + staffType + " :");
                Console.WriteLine(lstStaffFilteredByType[0].GetHeadLinePrintable());
                Console.WriteLine("-----------------------------------------------------------------------------------------");
                foreach (Staff s in lstStaffFilteredByType)
                {
                    Console.WriteLine(s.GetPrintable());

                }
                Console.WriteLine("------------------------------------------------------------------------------------------\n");
            }
        }
        #endregion


        //Generic Function to take inputs, validate and return. 
        public T ReadAndValidateInput<T>(T inputItem, String promptString, IsEmpty<T> isEmpty, ValidationFunction<T> validationFunction)
        {
            T readValue;
            do
            {
                Console.Write(promptString);
                if (!isEmpty(inputItem))
                {
                    Console.Write($" ({inputItem}) ");
                }

                readValue = (T)Convert.ChangeType(Console.ReadLine(), typeof(T));
                readValue = isEmpty(readValue) ? inputItem : readValue;
            } while (!validationFunction(readValue));
            return readValue;
        }

        protected Staff(int id, StaffType staffType)
        {
            Id = id;
            StaffType = staffType;
        }
    }
}
