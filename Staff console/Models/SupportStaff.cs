using System;
using System.Collections.Generic;
using System.Text;

namespace Staff_console.Models
{
    class SupportStaff : Staff
    {
        public String Role { get; set; }

        public static readonly String HeadLinePrintable = "Id\t\tName\t\tRole\t\tType";

        public SupportStaff(int id) : base(id, StaffType.supportStaff)
        {
            
        }

        


        public override Staff Add()
        {
            Console.WriteLine("\n-> Add a Support Staff:-");
            GetConsoleInput();
            return this;
        }

        public override void Update()
        {
            Console.WriteLine($"\n-> Update Support Staff with id: {Id} :-");
            GetConsoleInput();
            Console.WriteLine("\n Support Staff Updated.\n");
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
            return $"{Id}\t\t{Name}\t\t{Role}\t\t{StaffType}";
        }

        public override void GetConsoleInput()
        {
            //Read Common Inputs
            base.GetConsoleInput();
            //Read Specific Inputs
            Console.WriteLine("Enter role: ");
            Role = Console.ReadLine();
        }

        public override string GetHeadLinePrintable()
        {
            return HeadLinePrintable;
        }

        public override void ClearValues()
        {
            base.ClearValues();
            Role = "";
        }
    }
}
