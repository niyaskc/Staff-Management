
using StaffModelsLibrary;
using System;
using System.Collections.Generic;

namespace StaffConsoleApp
{
    class ConsoleHelper
    {
        private static int _staffId = 1;

        public static Staff ReadAndCreateStaffFromType()
        {
            int staffsType = ReadStaffType();
            Staff newStaff;
            switch (staffsType)
            {
                case 1:
                    Console.WriteLine("\n-> Teaching Staff Selected\n");
                    newStaff =  new TeachingStaff(_staffId++);
                    break;
                case 2:
                    Console.WriteLine("\n-> Administrative Staff Selected\n");
                    newStaff = new AdministrativeStaff(_staffId++);
                    break;
                case 3:
                    Console.WriteLine("\n-> Support Staff Selected\n");
                    newStaff = new SupportStaff(_staffId++);
                    break;
                default:
                    Console.WriteLine("\n-> Invalid Staff Selected\n");
                    return null;
            }

            ReadOrUpdateStaffDetails(newStaff);
            return newStaff;
        }

        public static int ReadStaffType()
        {
            int choosedStaff;
            while (true)
            {
                Console.WriteLine("Enter Staff Type:\n  1)Teaching Staff\n  2)Administrative Staff\n  3)Support Staff\n");
                choosedStaff = Convert.ToInt32(Console.ReadLine());
                if (choosedStaff > 3 || choosedStaff < 1)
                {
                    Console.WriteLine("\nInvalid Staff Type. Try Again\n");
                }
                else
                {
                    break;
                }
            }
            return choosedStaff;
        }

        public static int ReadStaffId()
        {
            Console.WriteLine("Enter Staff Id: ");
            return Convert.ToInt32(Console.ReadLine());
        }

        public static void ReadOrUpdateStaffDetails(Staff staff)
        {

            //Read Common Inputs
            staff.Name = ReadAndValidateInput<String>(staff.Name,
                "Enter Name : ",
                s => String.IsNullOrWhiteSpace(s),
                staff.ValidateName,
                "!!! Invalid Name. Minimum 4 Letters.");

            //Read Specific Details
            switch (staff.StaffType)
            {
                case StaffType.teachingStaff:
                    ((TeachingStaff)staff).SubjectName = ReadAndValidateInput<String>(((TeachingStaff)staff).SubjectName, 
                        "Enter Subject Name : ", 
                        s => String.IsNullOrWhiteSpace(s), 
                        ((TeachingStaff)staff).ValidateSubjectName, 
                        "!!! Invalid Subject Name. Minimum 2 Letters.");
                    break;
                case StaffType.administrativeStaff:
                    ((AdministrativeStaff)staff).Position = ReadAndValidateInput<String>(((AdministrativeStaff)staff).Position,
                        "Enter Position : ",
                        s => String.IsNullOrWhiteSpace(s),
                        ((AdministrativeStaff)staff).ValidatePosition,
                        "!!! Invalid Position. Minimum 3 letters required.");
                    break;
                case StaffType.supportStaff:
                    ((SupportStaff)staff).Role = ReadAndValidateInput<String>(((SupportStaff)staff).Role,
                        "Enter Role : ",
                        s => String.IsNullOrWhiteSpace(s),
                        ((SupportStaff)staff).ValidateRole,
                        "!!! Invalid Role. Minimum 5 Letters.");
                    break;
            }
        }

        public static void ViewStaff(Staff staff)
        {
            Console.WriteLine(GetHeadLineFromStaffType(staff.StaffType));
            Console.WriteLine("-----------------------------------------------------------------------------------------");
            Console.WriteLine(GetAsRowFromStaff(staff));
            Console.WriteLine("-----------------------------------------------------------------------------------------\n");
        }

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
                //Print Headline
                Console.WriteLine(GetHeadLineFromStaffType(lstStaffFilteredByType[0].StaffType));
                Console.WriteLine("-----------------------------------------------------------------------------------------");
                foreach (Staff s in lstStaffFilteredByType)
                {
                    
                    Console.WriteLine(GetAsRowFromStaff(s));

                }
                Console.WriteLine("------------------------------------------------------------------------------------------\n");
            }
        }

        //Generic Function to take inputs, validate and return.
        private static T ReadAndValidateInput<T>(T OldValue, 
            String promptString, IsEmpty<T> isEmpty, 
            ValidationFunction<T> validationFunction, String invalidPrompt)
        {
            T readValue;
            bool isReadDone;
            do
            {
                Console.Write(promptString);
                if (!isEmpty(OldValue))
                {
                    Console.Write($" ({OldValue}) ");
                }

                readValue = (T)Convert.ChangeType(Console.ReadLine(), typeof(T));
                readValue = isEmpty(readValue) ? OldValue : readValue;
                isReadDone = validationFunction(readValue);
                if (!isReadDone)
                {
                    Console.WriteLine("\n"+invalidPrompt+"\n");
                }
            } while (!isReadDone);
            return readValue;
        }
        
        public static String GetHeadLineFromStaffType(StaffType staffType)
        {
            //Common Fields
            String headLine = "ID\t\tName\t\t{0}\t\tStaff Type"; 
            //Staff Type Specific Fields
            switch (staffType)
            {
                case StaffType.teachingStaff:
                    headLine = String.Format(headLine, "Subject Type");
                    break;
                case StaffType.administrativeStaff:
                    headLine = String.Format(headLine, "Position");
                    break;
                case StaffType.supportStaff:
                    headLine = String.Format(headLine, "Role");
                    break;
                default:
                    return null;
            }
            return headLine;
        }

        public static String GetAsRowFromStaff(Staff staff)
        {
            //Common Fields
            String row = $"{staff.Id}\t\t{staff.Name}\t\t{{0}}\t\t{staff.StaffType}"; 
            switch (staff.StaffType)
            {
                case StaffType.teachingStaff:
                    row = String.Format(row, ((TeachingStaff)staff).SubjectName);
                    break;
                case StaffType.administrativeStaff:
                    row = String.Format(row, ((AdministrativeStaff)staff).Position);
                    break;
                case StaffType.supportStaff:
                    row = String.Format(row, ((SupportStaff)staff).Role);
                    break;
                default:
                    return null;
            }
            return row;
        }
    }
}
