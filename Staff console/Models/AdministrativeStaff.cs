using System;
using System.Collections.Generic;
using System.Text;

namespace Staff_console.Models
{
    class AdministrativeStaff : Staff
    {
        #region Class Member Variables
        public String Position { get; set; }

        public static readonly String HeadLinePrintable = "Id\t\tName\t\tPosition\t\tType";
        #endregion

        #region Validation Methods

        //Validate Position
        private bool ValidatePosition(String position)
        {
            if (position?.Length > 2)
            {
                return true;
            }
            Console.WriteLine("\n!!! Invalid Position. Minimum 3 letters required. \n");
            return false;
        }
        #endregion

        #region Overriding Methods
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
            this.Position = ReadAndValidateInput<String>(this.Position, "Enter Position: ", s => String.IsNullOrEmpty(s), ValidatePosition);
        }

        public override string GetHeadLinePrintable()
        {
            return HeadLinePrintable;
        }
        #endregion

        public AdministrativeStaff(int id) : base(id, StaffType.administrativeStaff)
        {

        }
    }
}
