using Staff_console.Helpers;
using Staff_console.Models;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace Staff_console
{
    class Program
    {
        public static readonly String optionsMenuText = "\nSelect menu \n  1) Add a Staff\n  2) Update details of a Staff\n  3) Delete a Staff\n  4) View one specific Staff\n  5) View all Staff\n  6) Exit";

        static void Main(string[] args)
        {
            //Initialising Staff List
            List<Staff> lstStaff = new List<Staff>();
            bool isMenuDone = false;

            //print school Name from config file
            String schoolName = ConfigurationManager.AppSettings.Get("School Name");
            schoolName = $"****** Welcome to {schoolName} ******";
            Console.WriteLine(new String('*', schoolName.Length));
            Console.WriteLine(schoolName);
            Console.WriteLine( new String( '*', schoolName.Length));

            do
            {
                int menuSelected;

                //Show Options Menu
                Console.WriteLine(optionsMenuText);
                menuSelected = Convert.ToInt32(Console.ReadLine());
                switch (menuSelected)
                {
                    case 1:
                        Console.WriteLine("\n->Add  Staff:-\n");
                        Staff newStaff = ConsoleHelper.ReadAndCreateStaffFromType();
                        lstStaff.Add(newStaff.Add());
                        Console.WriteLine("\nStaff Added.\n");
                        break;
                    case 2:
                        Console.WriteLine("\n-> Update details of a Staff");
                        int updateId = ConsoleHelper.ReadStaffId();
                        Staff getStaff = lstStaff.Find(s => s.Id == updateId);
                        if (getStaff == null)
                        {
                            Console.WriteLine("!!!! Staff Not Found\n");
                            continue;
                        }
                        getStaff.Update();
                        break;
                    case 3:
                        Console.WriteLine("\n-> Delete a Staff");
                        int deleteId = ConsoleHelper.ReadStaffId();
                        bool isDeleted = lstStaff.Remove(lstStaff.Find(s => s.Id == deleteId));
                        if (isDeleted)
                        {
                            Console.WriteLine("Staff Deleted Successfully\n");
                        }
                        else
                        {
                            Console.WriteLine("!!! Staff Not Found or Deleted.\n");
                            continue;
                        }
                        break;
                    case 4:
                        Console.WriteLine("\n-> View a Staff");
                        int staffId = ConsoleHelper.ReadStaffId();
                        Staff staffRead = lstStaff.Find(s => s.Id == staffId);
                        if (staffRead != null)
                        {
                            staffRead.View();
                        }
                        else
                        {
                            Console.WriteLine("!!! Staff Not Found.\n");
                            continue;
                        }
                        break;
                    case 5:
                        Staff.ViewAll(lstStaff);
                        break;
                    case 6:
                        Console.WriteLine("\n<- Exit...\n");
                        isMenuDone = true;
                        continue;
                    default:
                        Console.WriteLine("\n!!! Invalid menu. Please try again.\n");
                        continue;
                }
            } while (!isMenuDone);

        }
    }
}
