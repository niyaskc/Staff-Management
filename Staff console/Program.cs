using System;
using System.Collections.Generic;

namespace Staff_console
{
    class Program
    {
        public static List<TeachingStaff> TeachingStaffs { get; set; }
        public static List<AdministrativeStaff> AdministrativeStaffs { get; set; }
        public static List<SupportStaff> SupportStaffs { get; set; }

        private static int _staffId = 1;

        public static readonly String[] staffTypes = { "Teaching Staff", "Administrative Staff", "Support Staff" };
        public static readonly String line = "-------------------------------------------------------------------------";
        public static readonly String selectStaffMenu = "Select Staff \n  1) " + staffTypes[0] + "\n  2) " + staffTypes[1] + "\n  3) " + staffTypes[2] + "\n  4) Exit";
        public static readonly String optionsMenu = "Select menu \n  1) Add a {0}\n  2) Update details of a {0}\n  3) Delete a {0}\n  4) View one specific {0}\n  5) View all {0}\n  6) Back";

        public static void AddStaff(int staffType)
        {
            switch (staffType)
            {
                case 1:

                    //Create new Teaching Staff
                    TeachingStaff newTeachingStaff = new TeachingStaff(_staffId++);

                    //Reading inputs
                    Console.WriteLine("Enter Name: ");
                    newTeachingStaff.Name = Console.ReadLine();
                    Console.WriteLine("Enter Subject Name: ");
                    newTeachingStaff.SubjectName = Console.ReadLine();

                    //Add to list
                    TeachingStaffs.Add(newTeachingStaff);

                    break;

                case 2:

                    //Create new Administrative Staff
                    AdministrativeStaff newAdministrativeStaff = new AdministrativeStaff(_staffId++);

                    //Reading inputs
                    Console.WriteLine("Enter Name: ");
                    newAdministrativeStaff.Name = Console.ReadLine();
                    Console.WriteLine("Enter Position: ");
                    newAdministrativeStaff.Position = Console.ReadLine();

                    //Add to list
                    AdministrativeStaffs.Add(newAdministrativeStaff);

                    break;

                case 3:

                    //Create new Support Staff
                    SupportStaff newSupportStaff = new SupportStaff(_staffId++);

                    //Reading inputs
                    Console.WriteLine("Enter Name: ");
                    newSupportStaff.Name = Console.ReadLine();
                    Console.WriteLine("Enter role: ");
                    newSupportStaff.Role = Console.ReadLine();

                    //Add to list
                    SupportStaffs.Add(newSupportStaff);

                    break;

                default:
                    Console.WriteLine("!!! Invalid");
                    return;
            }

            Console.WriteLine(staffTypes[staffType - 1] + " Added Successfully.\n");
        }

        public static void UpdateStaff(int id, int staffType)
        {
            switch (staffType)
            {
                case 1:
                    TeachingStaff teachingStaffUpdate = TeachingStaffs.Find(item => item.Id == id);
                    if(teachingStaffUpdate != null)
                    {
                        //Reading inputs for update
                        Console.WriteLine("Update Name: ");
                        teachingStaffUpdate.Name = Console.ReadLine();
                        Console.WriteLine("Update Subject Name: ");
                        teachingStaffUpdate.SubjectName = Console.ReadLine();

                        Console.WriteLine(staffTypes[staffType - 1] + " Updated Successfully\n");

                        return;
                    }
                    break;

                case 2:
                    AdministrativeStaff administrativeStaffUpdate = AdministrativeStaffs.Find(item => item.Id == id);
                    if (administrativeStaffUpdate != null)
                    {
                        //Reading inputs for update
                        Console.WriteLine("Update Name: ");
                        administrativeStaffUpdate.Name = Console.ReadLine();
                        Console.WriteLine("Update Position: ");
                        administrativeStaffUpdate.Position = Console.ReadLine();

                        Console.WriteLine(staffTypes[staffType - 1] + " Updated Successfully\n");

                        return;
                    }
                    break;

                case 3:
                    SupportStaff supportStaffUpdate = SupportStaffs.Find(item => item.Id == id);
                    if (supportStaffUpdate != null)
                    {
                        //Reading inputs for update
                        Console.WriteLine("Update Name: ");
                        supportStaffUpdate.Name = Console.ReadLine();
                        Console.WriteLine("Update role: ");
                        supportStaffUpdate.Role = Console.ReadLine();

                        Console.WriteLine(staffTypes[staffType - 1] + " Updated Successfully\n");

                        return;
                    }
                    break;

                default:
                    Console.WriteLine("!!! Invalid");
                    return;

            }

            Console.WriteLine("Invalid Staff! Cannot Update " + staffTypes[staffType - 1] + "\n");
        }

        public static void DeleteStaff(int id, int staffType)
        {
            bool isDeleted;
            switch (staffType)
            {
                case 1:
                    isDeleted = TeachingStaffs.Remove(TeachingStaffs.Find(item => item.Id == id));
                    break;

                case 2:
                    isDeleted = AdministrativeStaffs.Remove(AdministrativeStaffs.Find(item => item.Id == id));
                    break;

                case 3:
                    isDeleted = SupportStaffs.Remove(SupportStaffs.Find(item => item.Id == id));
                    break;

                default:
                    Console.WriteLine("!!! Invalid");
                    return;

            }
            if (isDeleted)
            {
                Console.WriteLine(staffTypes[staffType - 1] + " Deleted Successfully\n");
            }
            else
            {
                Console.WriteLine("Invalid Staff! Cannot Delete " + staffTypes[staffType - 1] + "\n");
            }
        }

        public static void ViewSpecificStaff(int id, int staffType)
        {
            switch (staffType)
            {
                case 1:
                    Console.WriteLine(TeachingStaff.HeadLinePrintable);
                    Console.WriteLine(line);
                    TeachingStaff t = (TeachingStaffs.Find(item => item.Id == id));
                    if(t != null)
                    {
                        Console.WriteLine(t.GetPrintable());
                    }
                    else
                    {
                        Console.WriteLine("Invalid " + staffTypes[staffType - 1] + "!!\n");
                    }
                    
                    break;

                case 2:
                    Console.WriteLine(AdministrativeStaff.HeadLinePrintable);
                    Console.WriteLine(line);
                    AdministrativeStaff a = AdministrativeStaffs.Find(item => item.Id == id);
                    if(a != null)
                    {
                        Console.WriteLine(a.GetPrintable());
                    }
                    else
                    {
                        Console.WriteLine("Invalid " + staffTypes[staffType - 1] + "!!\n");
                    }
                    break;

                case 3:
                    Console.WriteLine(SupportStaff.HeadLinePrintable);
                    Console.WriteLine(line);
                    SupportStaff s = (SupportStaffs.Find(item => item.Id == id));
                    if(s != null)
                    {
                        Console.WriteLine(s.GetPrintable());
                    }
                    else
                    {
                        Console.WriteLine("Invalid " + staffTypes[staffType - 1] + "!!\n");
                    }
                    
                    break;

                default:
                    Console.WriteLine("!!! Invalid");
                    return;
            }
            Console.WriteLine(line+"\n");
        }

        public static void ViewAll(int staffType)
        {
            Console.WriteLine("Printing " + staffTypes[staffType - 1] + "...");
            switch (staffType)
            {
                case 1:
                    Console.WriteLine(TeachingStaff.HeadLinePrintable);
                    Console.WriteLine(line);
                    foreach (TeachingStaff t in TeachingStaffs)
                    {
                        Console.WriteLine(t.GetPrintable());
                    }
                    break;

                case 2:
                    Console.WriteLine(AdministrativeStaff.HeadLinePrintable);
                    Console.WriteLine(line);
                    foreach (AdministrativeStaff t in AdministrativeStaffs)
                    {
                        Console.WriteLine(t.GetPrintable());
                    }
                    break;

                case 3:
                    Console.WriteLine(SupportStaff.HeadLinePrintable);
                    Console.WriteLine(line);
                    foreach (SupportStaff t in SupportStaffs)
                    {
                        Console.WriteLine(t.GetPrintable());
                    }
                    break;
            }
            Console.WriteLine(line+"\n");
        }

        static void Main(string[] args)
        {
            //Initialising Lists
            TeachingStaffs = new List<TeachingStaff>();
            AdministrativeStaffs = new List<AdministrativeStaff>();
            SupportStaffs = new List<SupportStaff>();


            bool isDone = false;
            while(!isDone)
            {
                int chooseStaff;

                //Show Staff Selection Menu
                Console.WriteLine(selectStaffMenu);
                chooseStaff = Convert.ToInt32(Console.ReadLine());
                switch (chooseStaff)
                {
                    case 1:
                    case 2:
                    case 3:
                        Console.WriteLine("\n-> " + staffTypes[chooseStaff-1] + " Selected\n");
                        break;
                    case 4:
                        Console.WriteLine("\nExiting...\n");
                        isDone = true;
                        continue;
                    default:
                        Console.WriteLine("\n!!! Invalid staff. Please try again.\n");
                        continue;
                }

                bool isMenuDone = false;

                while (!isMenuDone)
                {
                    int menu;

                    //Show Options Menu
                    Console.WriteLine(String.Format( optionsMenu, staffTypes[chooseStaff-1]));
                    menu = Convert.ToInt32(Console.ReadLine());
                    switch (menu)
                    {
                        case 1:
                            Console.WriteLine("\n-> Add " + staffTypes[chooseStaff - 1] + "\n");
                            AddStaff(chooseStaff);
                            break;
                        case 2:
                            Console.WriteLine("\n-> Update details of a " + staffTypes[chooseStaff - 1]);
                            Console.WriteLine("Enter Staff Id: ");
                            int updateStaffId = Convert.ToInt32(Console.ReadLine());
                            UpdateStaff(updateStaffId, chooseStaff);
                            break;
                        case 3:
                            Console.WriteLine("\n-> Delete a " + staffTypes[chooseStaff - 1] + "");
                            Console.WriteLine("Enter Staff Id: ");
                            int DeleteStaffId = Convert.ToInt32(Console.ReadLine());
                            DeleteStaff(DeleteStaffId, chooseStaff);
                            break;
                        case 4:
                            Console.WriteLine("\n-> View one specific " + staffTypes[chooseStaff - 1]);
                            Console.WriteLine("Enter Staff Id: ");
                            int ViewStaffId = Convert.ToInt32(Console.ReadLine());
                            ViewSpecificStaff(ViewStaffId, chooseStaff);
                            break;
                        case 5:
                            ViewAll(chooseStaff);
                            break;
                        case 6:
                            Console.WriteLine("\n<- back...\n");
                            isMenuDone = true;
                            continue;
                        default:
                            Console.WriteLine("\n!!! Invalid menu. Please try again.\n");
                            continue;
                    }
                }
            }
        }
    }
}
