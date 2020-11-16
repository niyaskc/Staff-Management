
using System;
using System.Collections.Generic;
using System.Text;

public delegate bool ValidationFunction<T>(T inputForValidation);
public delegate bool IsEmpty<T>(T item);

namespace StaffModelsLibrary
{
    public abstract class Staff
    {
        #region Class Member Variables
        public int Id { get; set; }
        public String Name { get; set; }

        public StaffType StaffType { get; }
        #endregion

        #region Validation Methods

        //Validate Name
        public bool ValidateName(String name)
        {
            if (name?.Length > 3)
            {
                return true;
            }
            return false;
        }
        #endregion

        protected Staff(int id, StaffType staffType)
        {
            Id = id;
            StaffType = staffType;
        }
    }
}
