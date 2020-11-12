using System;
using System.Collections.Generic;
using System.Text;

namespace Staff_console.Models
{
    class AdministrativeStaff : Staff
    {
        public String Position { get; set; }

        public static readonly String HeadLinePrintable = "Id\t\tName\t\tPosition\t\tType";

        public AdministrativeStaff(int id): base(id, StaffType.administrativeStaff)
        {

        }


        public override Staff Add()
        {
            Console.WriteLine("\n-> Add a Administrative Staff:-");
            GetConsoleInput();
            return this;
        }

        public override void Update()
        {
            Console.WriteLine($"\n-> Update Administrative Staff with id: {Id} :-");
            GetConsoleInput();
            Console.WriteLine("\n Administrative Staff Updated.\n");
        }

        public override void Delete()
        {
            ClearValues();
        }

        public override void View()
        {
            Console.WriteLine(HeadLinePrintable);
            Console.WriteLine("-----------------------------------------------------------------------------------------");
            Console.WriteLine(GetPrintable());
            Console.WriteLine("-----------------------------------------------------------------------------------------\n");
        }


        public override string GetPrintable()
        {
            return $"{Id}\t\t{Name}\t\t{Position}\t\t{StaffType}";
        }

        public override void GetConsoleInput()
        {
            //Read Common Inputs
            base.GetConsoleInput();
            //Read Specific Inputs
            Console.WriteLine("Enter Position: ");
            Position = Console.ReadLine();
        }

        public override string GetHeadLinePrintable()
        {
            return HeadLinePrintable;
        }

        public override void ClearValues()
        {
            base.ClearValues();
            Position = "";
        }
    }
}
