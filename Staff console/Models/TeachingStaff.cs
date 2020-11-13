using System;
using System.Collections.Generic;
using System.Text;

namespace Staff_console.Models
{
    class TeachingStaff : Staff
    {
        #region Class Member Variables
        public String SubjectName { get; set; }

        public static readonly String HeadLinePrintable = "Id\t\tName\t\tSubject Name\t\tType";
        #endregion

        #region Validation Methods

        //Validate Subject Name
        private bool ValidateSubjectName(String subjectName)
        {
            if (subjectName?.Length > 1)
            {
                return true;
            }
            Console.WriteLine("\n!!! Invalid Subject Name. Minimum 2 Letters.\n");
            return false;
        }
        #endregion

        #region Overriding Methods
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
            this.SubjectName = ReadAndValidateInput<String>(this.SubjectName, "Enter Subject Name: ", s => String.IsNullOrEmpty(s), ValidateSubjectName);
        }

        public override string GetHeadLinePrintable()
        {
            return HeadLinePrintable;
        }
        #endregion

        public TeachingStaff(int id) : base(id, StaffType.teachingStaff)
        {

        }
    }
}
