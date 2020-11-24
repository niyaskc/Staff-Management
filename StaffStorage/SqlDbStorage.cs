using StaffModelsLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace StaffStorage
{
    public class SqlDbStorage : IRepository
    {
        private readonly string _sqlConnection = "Data Source=LAPTOP-TN8BAH1N;Initial Catalog=StaffManageDatabase;Integrated Security=SSPI;";

        public bool AddStaff(Staff staff)
        {
            using (SqlConnection connection = new SqlConnection(_sqlConnection))
            {
                try
                {
                    connection.Open();
                    SqlCommand sql_cmnd = new SqlCommand("AddStaff", connection);
                    sql_cmnd.CommandType = CommandType.StoredProcedure;

                    //Adding Common Fields
                    sql_cmnd.Parameters.AddWithValue("@Name", staff.Name);
                    sql_cmnd.Parameters.AddWithValue("@staffType", (int)staff.StaffType);

                    //Adding specific Fields
                    switch (staff.StaffType)
                    {
                        case StaffType.teachingStaff:
                            sql_cmnd.Parameters.AddWithValue("@subjectName", ((TeachingStaff)staff).SubjectName);
                            break;
                        case StaffType.administrativeStaff:
                            sql_cmnd.Parameters.AddWithValue("@position", ((AdministrativeStaff)staff).Position);
                            break;
                        case StaffType.supportStaff:
                            sql_cmnd.Parameters.AddWithValue("@role", ((SupportStaff)staff).Role);
                            break;
                    }

                    if (sql_cmnd.ExecuteNonQuery() > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch
                {
                    return false;
                }
            }
        }

        public bool DeleteStaff(int id)
        {
            using (SqlConnection connection = new SqlConnection(_sqlConnection))
            {
                connection.Open();
                SqlCommand sql_cmnd = new SqlCommand("DeleteStaff", connection);
                sql_cmnd.CommandType = CommandType.StoredProcedure;
                sql_cmnd.Parameters.AddWithValue("@StaffId", id);

                if (sql_cmnd.ExecuteNonQuery() > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool UpdateStaff(int id, Staff staff)
        {
            using (SqlConnection connection = new SqlConnection(_sqlConnection))
            {
                try
                {
                    connection.Open();
                    SqlCommand sql_cmnd = new SqlCommand("UpdateStaff", connection);
                    sql_cmnd.CommandType = CommandType.StoredProcedure;

                    //Adding Common Fields
                    sql_cmnd.Parameters.AddWithValue("@staffId", id);
                    sql_cmnd.Parameters.AddWithValue("@Name", staff.Name);
                    sql_cmnd.Parameters.AddWithValue("@staffType", (int)staff.StaffType);

                    //Adding specific Fields
                    switch (staff.StaffType)
                    {
                        case StaffType.teachingStaff:
                            sql_cmnd.Parameters.AddWithValue("@subjectName", ((TeachingStaff)staff).SubjectName);
                            break;
                        case StaffType.administrativeStaff:
                            sql_cmnd.Parameters.AddWithValue("@position", ((AdministrativeStaff)staff).Position);
                            break;
                        case StaffType.supportStaff:
                            sql_cmnd.Parameters.AddWithValue("@role", ((SupportStaff)staff).Role);
                            break;
                    }

                    if (sql_cmnd.ExecuteNonQuery() > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch
                {
                    return false;
                }
                
            }
        }

        public List<Staff> ViewAllStaff()
        {
            List<Staff> staffs = new List<Staff>();

            using (SqlConnection connection = new SqlConnection(_sqlConnection))
            {
                connection.Open();
                SqlCommand sql_cmnd = new SqlCommand("ViewAll", connection);
                sql_cmnd.CommandType = CommandType.StoredProcedure;
                using (SqlDataReader oReader = sql_cmnd.ExecuteReader())
                {
                    do
                    {
                        while (oReader.Read())
                        {
                            Staff staff = _GetStaffFromData(oReader);
                            if (staff != null) staffs.Add(staff);
                        }
                    } while (oReader.NextResult());

                }
                connection.Close();
            }
            return staffs;

        }

        public Staff ViewStaff(int id)
        {
            Staff staff = null;

            using (SqlConnection connection = new SqlConnection(_sqlConnection))
            {
                connection.Open();
                SqlCommand sql_cmnd = new SqlCommand("ViewStaff", connection);
                sql_cmnd.CommandType = CommandType.StoredProcedure;
                sql_cmnd.Parameters.AddWithValue("@StaffId", id);
                using (SqlDataReader oReader = sql_cmnd.ExecuteReader())
                {
                    if (oReader.Read())
                    {
                        staff = _GetStaffFromData(oReader);
                    }
                }
                connection.Close();
            }

            return staff;
        }

        public bool AddStaffBulk(List<Staff> staffs)
        {
            using (SqlConnection connection = new SqlConnection(_sqlConnection))
            {
                connection.Open();
                SqlCommand sql_cmnd = new SqlCommand("AddStaffBulk", connection);
                sql_cmnd.CommandType = CommandType.StoredProcedure;
                sql_cmnd.Parameters.AddWithValue("@staffTable", _GetTableFromList(staffs));
                try
                {
                    if (sql_cmnd.ExecuteNonQuery() > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch
                {
                    return false;
                }
            }
        }

        public bool UpdateStaffBulk(List<Staff> staffs)
        {
            using (SqlConnection connection = new SqlConnection(_sqlConnection))
            {
                connection.Open();
                SqlCommand sql_cmnd = new SqlCommand("UpdateStaffBulk", connection);
                sql_cmnd.CommandType = CommandType.StoredProcedure;
                sql_cmnd.Parameters.AddWithValue("@staffTable", _GetTableFromList(staffs));
                try
                {
                    if (sql_cmnd.ExecuteNonQuery() > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch
                {
                    return false;
                }
            }
        }

        private Staff _GetStaffFromData(SqlDataReader dataReader)
        {
            Staff staff = null;

            //Creating Specific Staff and reading specific Fields
            switch ((int)dataReader["StaffTypeID"])
            {
                case (int)StaffType.teachingStaff:
                    staff = new TeachingStaff();
                    ((TeachingStaff)staff).SubjectName = dataReader["SubjectName"].ToString().Trim();
                    break;
                case (int)StaffType.administrativeStaff:
                    staff = new AdministrativeStaff();
                    ((AdministrativeStaff)staff).Position = dataReader["Position"].ToString().Trim();
                    break;
                case (int)StaffType.supportStaff:
                    staff = new SupportStaff();
                    ((SupportStaff)staff).Role = dataReader["Role"].ToString().Trim();
                    break;
            }

            //Common Fields
            if(staff != null)
            {
                staff.Id = (int)dataReader["StaffID"];
                staff.Name = dataReader["Name"].ToString().Trim();
            }

            return staff;
        }

        private DataTable _GetTableFromList(List<Staff> staffs)
        {
            //Creating staff Table
            DataTable staffTable = new DataTable("StaffTableType");
            staffTable.Columns.Add("StaffID", typeof(int));
            staffTable.Columns.Add("Name", typeof(string));
            staffTable.Columns.Add("StaffTypeID", typeof(int));
            staffTable.Columns.Add("SubjectName", typeof(string));
            staffTable.Columns.Add("Position", typeof(string));
            staffTable.Columns.Add("Role", typeof(string));

            foreach (Staff staff in staffs)
            {

                //Adding common Fields
                DataRow newStaffRow = staffTable.NewRow();
                newStaffRow["StaffID"] = staff.Id;
                newStaffRow["Name"] = staff.Name;
                newStaffRow["StaffTypeID"] = (int)staff.StaffType;


                //Adding specific Fields
                switch (staff.StaffType)
                {
                    case StaffType.teachingStaff:
                        newStaffRow["SubjectName"] = ((TeachingStaff)staff).SubjectName;
                        break;
                    case StaffType.administrativeStaff:
                        newStaffRow["Position"] = ((AdministrativeStaff)staff).Position;
                        break;
                    case StaffType.supportStaff:
                        newStaffRow["Role"] = ((SupportStaff)staff).Role;
                        break;
                }

                staffTable.Rows.Add(newStaffRow);

            }

            return staffTable;
        }


    }
}
