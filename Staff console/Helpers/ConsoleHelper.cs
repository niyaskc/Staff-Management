using Staff_console.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Staff_console.Helpers
{
    class ConsoleHelper
    {
        private static int _staffId = 1;

        public static Staff ReadAndCreateStaffFromType()
        {
            int staffsType = ReadStaffType();
            switch (staffsType)
            {
                case 1:
                    Console.WriteLine("\n-> Teaching Staff Selected\n");
                    return new TeachingStaff(_staffId++);
                case 2:
                    Console.WriteLine("\n-> Administrative Staff Selected\n");
                    return new AdministrativeStaff(_staffId++);
                case 3:
                    Console.WriteLine("\n-> Support Staff Selected\n");
                    return new SupportStaff(_staffId++);
            }

            return null;
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
    }
}
