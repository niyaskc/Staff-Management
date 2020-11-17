using StaffModelsLibrary;
using StaffStorage;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace StaffConsoleApp
{
    class Program
    {
        public static readonly String optionsMenuText = "\nSelect menu \n  1) Add a Staff\n  2) Update details of a Staff\n  3) Delete a Staff\n  4) View one specific Staff\n  5) View all Staff\n  6) Exit";

        static void Main(string[] args)
        {
            //Initialising Staff Storage (Repo)
            String repoClassName = ConfigurationManager.AppSettings.Get("Repo Class Name");
            var repoType = Type.GetType(repoClassName);
            IRepository staffRepo = Activator.CreateInstance(repoType) as IRepository;

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
                        bool isAdded = staffRepo.AddStaff(newStaff);
                        Console.WriteLine(isAdded ? "\nStaff Added.\n" : "!!! Error. Staff Not Added\n");
                        break;
                    case 2:
                        Console.WriteLine("\n-> Update details of a Staff");
                        int updateId = ConsoleHelper.ReadStaffId();
                        Staff staffToUpdate = staffRepo.ViewStaff(updateId);
                        if (staffToUpdate == null)
                        {
                            Console.WriteLine("!!!! Staff Not Found\n");
                            continue;
                        }
                        ConsoleHelper.ReadOrUpdateStaffDetails(staffToUpdate);
                        bool isupdated = staffRepo.UpdateStaff(updateId, staffToUpdate);
                        Console.WriteLine(isupdated ? "Staff Updated Successfully\n" : "!!! Error. Staff Not Updated\n");
                        break;
                    case 3:
                        Console.WriteLine("\n-> Delete a Staff");
                        int deleteId = ConsoleHelper.ReadStaffId();
                        bool isDeleted = staffRepo.DeleteStaff(deleteId);
                        Console.WriteLine(isDeleted ? "Staff Deleted Successfully\n" : "!!! Staff Not Found or Deleted.\n");
                        break;
                    case 4:
                        Console.WriteLine("\n-> View a Staff");
                        int staffId = ConsoleHelper.ReadStaffId();
                        Staff staffRead = staffRepo.ViewStaff(staffId);
                        if (staffRead != null)
                        {
                            ConsoleHelper.ViewStaff(staffRead);
                        }
                        else
                        {
                            Console.WriteLine("!!! Staff Not Found.\n");
                            continue;
                        }
                        break;
                    case 5:
                        ConsoleHelper.ViewAll(staffRepo.ViewAllStaff());
                        break;
                    case 6:
                        Console.WriteLine("\n<- Exit...\n");
                        return;
                    default:
                        Console.WriteLine("\n!!! Invalid menu. Please try again.\n");
                        continue;
                }
            } while (true);

        }
    }
}
