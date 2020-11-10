using System;

namespace Staff_console
{
    class Program
    {

        public static readonly String[] staffTypes = { "Teaching Staff", "Administrative Staff", "Support Staff" };
        static StaffList staffList;

        public static Staff GetInput(int staffType)
        {
            string name, location, gender;

            Staff staff;

            //Reading input
            Console.WriteLine("Enter Name: ");
            name = Console.ReadLine();
            Console.WriteLine("Enter Location: ");
            location = Console.ReadLine();
            Console.WriteLine("Enter Gender: ");
            gender = Console.ReadLine();

            switch (staffType)
            {
                case 1:
                    string subjectName, dept;

                    //Reading inputs
                    Console.WriteLine("Enter Subject Name: ");
                    subjectName = Console.ReadLine();
                    Console.WriteLine("Enter Department: ");
                    dept = Console.ReadLine();

                    staff = new TeachingStaff(0, name, location, gender, subjectName, dept);

                    break;

                case 2:
                    string position;

                    //Reading inputs
                    Console.WriteLine("Enter Position: ");
                    position = Console.ReadLine();

                    staff = new AdministrativeStaff(0, name, location, gender, position);

                    break;

                case 3:
                    string role;

                    //Reading inputs
                    Console.WriteLine("Enter role: ");
                    role = Console.ReadLine();

                    staff = new SupportStaff(0, name, location, gender, role);

                    break;

                default:
                    Console.WriteLine("!!! Invalid");
                    return null;
            }

            return staff;
        }

        public static void AddStaff(int staffType)
        {

            Staff staff = GetInput(staffType);

            int id = staffList.AddStaff(staff, true);
            if(id != -1)
            {
                Console.WriteLine(staffTypes[staffType - 1] + " Added Successfully. id: "+id+"\n");
            }
            else
            {
                Console.WriteLine("Error! Cannot add "+staffTypes[staffType - 1] + "\n");
            }
        }

        public static void UpdateStaff(int id, int staffType)
        {
            bool isDeleted;
            switch (staffType)
            {
                case 1:
                    isDeleted = staffList.TeachingStaffs.Remove(staffList.TeachingStaffs.Find(item => item.Id == id));
                    break;

                case 2:
                    isDeleted = staffList.AdministrativeStaffs.Remove(staffList.AdministrativeStaffs.Find(item => item.Id == id));
                    break;

                case 3:
                    isDeleted = staffList.SupportStaffs.Remove(staffList.SupportStaffs.Find(item => item.Id == id));
                    break;

                default:
                    Console.WriteLine("!!! Invalid");
                    return;

            }
            if (!isDeleted)
            {
                Console.WriteLine("Invalid staff! Cannot update " + staffTypes[staffType - 1] + "\n");
                return;
            }
            Staff s = GetInput(staffType);
            s.Id = id;
            int getid = staffList.AddStaff(s, false);
            if (getid != -1)
            {
                Console.WriteLine(staffTypes[staffType - 1] + " Updated Successfully. id: " + id + "\n");
            }
            else
            {
                Console.WriteLine("Error! Cannot update " + staffTypes[staffType - 1] + "\n");
            }
        }

        public static void DeleteStaff(int id, int staffType)
        {
            bool isDeleted;
            switch (staffType)
            {
                case 1:
                    isDeleted = staffList.TeachingStaffs.Remove(staffList.TeachingStaffs.Find(item => item.Id == id));
                    break;

                case 2:
                    isDeleted = staffList.AdministrativeStaffs.Remove(staffList.AdministrativeStaffs.Find(item => item.Id == id));
                    break;

                case 3:
                    isDeleted = staffList.SupportStaffs.Remove(staffList.SupportStaffs.Find(item => item.Id == id));
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
                    Console.WriteLine("Id\t\tName\t\tLocation\t\tGender\t\tSubject Name\t\tDepartment");
                    Console.WriteLine("----------------------------------------------------------------------------------------------");
                    TeachingStaff t = (staffList.TeachingStaffs.Find(item => item.Id == id));
                    if(t != null)
                    {
                        Console.WriteLine(t.Id + "\t\t" + t.Name + "\t\t" + t.Location + "\t\t" + t.Gender + "\t\t" + t.SubjectName + "\t\t" + t.Dept);
                    }
                    else
                    {
                        Console.WriteLine("Invalid " + staffTypes[staffType - 1] + "!!\n");
                    }
                    
                    break;

                case 2:
                    Console.WriteLine("Id\t\tName\t\tLocation\t\tGender\t\tPosition");
                    Console.WriteLine("----------------------------------------------------------------------------------------------");
                    AdministrativeStaff a = staffList.AdministrativeStaffs.Find(item => item.Id == id);
                    if(a != null)
                    {
                        Console.WriteLine(a.Id + "\t\t" + a.Name + "\t\t" + a.Location + "\t\t" + a.Gender + "\t\t" + a.Position);
                    }
                    else
                    {
                        Console.WriteLine("Invalid " + staffTypes[staffType - 1] + "!!\n");
                    }
                    break;

                case 3:
                    Console.WriteLine("Id\t\tName\t\tLocation\t\tGender\t\tRole");
                    Console.WriteLine("----------------------------------------------------------------------------------------------");
                    SupportStaff s = (staffList.SupportStaffs.Find(item => item.Id == id));
                    if(s != null)
                    {
                        Console.WriteLine(s.Id + "\t\t" + s.Name + "\t\t" + s.Location + "\t\t" + s.Gender + "\t\t" + s.Role);
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
            Console.WriteLine("----------------------------------------------------------------------------------------------\n");
        }

        public static void ViewAll(int staffType)
        {
            Console.WriteLine("Printing " + staffTypes[staffType - 1] + "...");
            switch (staffType)
            {
                case 1:
                    Console.WriteLine("Id\t\tName\t\tLocation\t\tGender\t\tSubject Name\t\tDepartment");
                    Console.WriteLine("----------------------------------------------------------------------------------------------");
                    foreach (TeachingStaff t in staffList.TeachingStaffs)
                    {
                        Console.WriteLine(t.Id+"\t\t"+ t.Name+ "\t\t" + t.Location + "\t\t" + t.Gender + "\t\t" + t.SubjectName + "\t\t" + t.Dept);
                    }
                    break;

                case 2:
                    Console.WriteLine("Id\t\tName\t\tLocation\t\tGender\t\tPosition");
                    Console.WriteLine("----------------------------------------------------------------------------------------------");
                    foreach (AdministrativeStaff t in staffList.AdministrativeStaffs)
                    {
                        Console.WriteLine(t.Id + "\t\t" + t.Name + "\t\t" + t.Location + "\t\t" + t.Gender + "\t\t" + t.Position);
                    }
                    break;

                case 3:
                    Console.WriteLine("Id\t\tName\t\tLocation\t\tGender\t\tRole");
                    Console.WriteLine("----------------------------------------------------------------------------------------------");
                    foreach (SupportStaff t in staffList.SupportStaffs)
                    {
                        Console.WriteLine(t.Id + "\t\t" + t.Name + "\t\t" + t.Location + "\t\t" + t.Gender + "\t\t" + t.Role);
                    }
                    break;
            }
            Console.WriteLine("----------------------------------------------------------------------------------------------\n");
        }

        static void Main(string[] args)
        {
            staffList = new StaffList();
            bool isDone = false;

            while(!isDone)
            {
                int choose;
                Console.WriteLine("Select Staff \n  1) "+staffTypes[0]+ "\n  2) " + staffTypes[1] + "\n  3) " + staffTypes[2] + "\n  4) Exit");
                choose = Convert.ToInt32(Console.ReadLine());
                switch (choose)
                {
                    case 1:
                    case 2:
                    case 3:
                        Console.WriteLine("\n-> " + staffTypes[choose-1] + " Selected\n");
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

                    Console.WriteLine("Select menu \n  1) Add a " + staffTypes[choose - 1] + "\n  2) Update details of a " + staffTypes[choose - 1] + "\n  3) Delete a " + staffTypes[choose - 1] + "\n  4) View one specific " + staffTypes[choose - 1] + "\n  5) View all " + staffTypes[choose - 1] + "\n  6) Back");
                    menu = Convert.ToInt32(Console.ReadLine());
                    switch (menu)
                    {
                        case 1:
                            Console.WriteLine("\n-> Add " + staffTypes[choose - 1] + "\n");
                            AddStaff(choose);
                            break;
                        case 2:
                            Console.WriteLine("\n-> Update details of a " + staffTypes[choose - 1]);
                            Console.WriteLine("Enter Staff Id: ");
                            int idu = Convert.ToInt32(Console.ReadLine());
                            UpdateStaff(idu, choose);
                            break;
                        case 3:
                            Console.WriteLine("\n-> Delete a " + staffTypes[choose - 1] + "");
                            Console.WriteLine("Enter Staff Id: ");
                            int id = Convert.ToInt32(Console.ReadLine());
                            DeleteStaff(id, choose);
                            break;
                        case 4:
                            Console.WriteLine("\n-> View one specific " + staffTypes[choose - 1]);
                            Console.WriteLine("Enter Staff Id: ");
                            int idv = Convert.ToInt32(Console.ReadLine());
                            ViewSpecificStaff(idv, choose);
                            break;
                        case 5:
                            ViewAll(choose);
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
