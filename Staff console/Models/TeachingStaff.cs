using System;
using System.Collections.Generic;
using System.Text;

namespace Staff_console.Models
{
    class TeachingStaff : Staff
    {
        public String SubjectName { get; set; }

        public static readonly String HeadLinePrintable = "Id\t\tName\t\tSubject Name\t\tType";

        public TeachingStaff(int id) : base(id, StaffType.teachingStaff)
        {

        }


        public override Staff Add()
        {
            Console.WriteLine("\n-> Add a Teaching Staff:-");
            GetConsoleInput();
            return this;
        }

        public override void Update()
        {
            Console.WriteLine($"\n-> Update Teaching Staff with id: {Id} :-");
            GetConsoleInput();
            Console.WriteLine("\n Teaching Staff Updated.\n");
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
            return $"{Id}\t\t{Name}\t\t{SubjectName}\t\t{StaffType}";
        }

        public override void GetConsoleInput()
        {
            //Read Common Inputs
            base.GetConsoleInput();
            //Read Specific Inputs
            Console.WriteLine("Enter Subject Name: ");
            SubjectName = Console.ReadLine();
        }

        public override string GetHeadLinePrintable()
        {
            return HeadLinePrintable;
        }

        public override void ClearValues()
        {
            base.ClearValues();
            SubjectName = "";
        }
    }
}
