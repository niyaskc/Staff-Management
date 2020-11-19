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
                connection.Open();
                SqlCommand sql_cmnd = new SqlCommand("AddStaff", connection);
                sql_cmnd.CommandType = CommandType.StoredProcedure;

                //Adding Common Fields
                sql_cmnd.Parameters.AddWithValue("@Name", staff.Name);
                sql_cmnd.Parameters.AddWithValue("@staffType", (int)staff.StaffType);

                //Adding specific Fields
                switch ((int)staff.StaffType)
                {
                    case (int)StaffType.teachingStaff:
                        sql_cmnd.Parameters.AddWithValue("@subjectName", ((TeachingStaff)staff).SubjectName);
                        break;
                    case (int)StaffType.administrativeStaff:
                        sql_cmnd.Parameters.AddWithValue("@position", ((AdministrativeStaff)staff).Position);
                        break;
                    case (int)StaffType.supportStaff:
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
                connection.Open();
                SqlCommand sql_cmnd = new SqlCommand("UpdateStaff", connection);
                sql_cmnd.CommandType = CommandType.StoredProcedure;

                //Adding Common Fields
                sql_cmnd.Parameters.AddWithValue("@staffId", id);
                sql_cmnd.Parameters.AddWithValue("@Name", staff.Name);
                sql_cmnd.Parameters.AddWithValue("@staffType", (int)staff.StaffType);
                
                //Adding specific Fields
                switch ((int)staff.StaffType)
                {
                    case (int)StaffType.teachingStaff:
                        sql_cmnd.Parameters.AddWithValue("@subjectName", ((TeachingStaff)staff).SubjectName);
                        break;
                    case (int)StaffType.administrativeStaff:
                        sql_cmnd.Parameters.AddWithValue("@position", ((AdministrativeStaff)staff).Position);
                        break;
                    case (int)StaffType.supportStaff:
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
                            Staff staff = GetStaffFromData(oReader);
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
                        staff = GetStaffFromData(oReader);
                    }
                }
                connection.Close();
            }

            return staff;
        }


        public Staff GetStaffFromData(SqlDataReader dataReader)
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

        public bool AddStaffBulk(List<Staff> staffs)
        {
            Tuple<DataTable, DataTable, DataTable, DataTable> tables = GetTableFromList(staffs, true);
            using (SqlConnection connection = new SqlConnection(_sqlConnection))
            {
                connection.Open();
                SqlCommand sql_cmnd = new SqlCommand("AddStaffBulk", connection);
                sql_cmnd.CommandType = CommandType.StoredProcedure;
                sql_cmnd.Parameters.AddWithValue("@staff", tables.Item1);
                sql_cmnd.Parameters.AddWithValue("@teachingStaff", tables.Item2);
                sql_cmnd.Parameters.AddWithValue("@administrativeStaff", tables.Item3);
                sql_cmnd.Parameters.AddWithValue("@supportStaff", tables.Item4);
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

        public bool UpdateStaffBulk(List<Staff> staffs)
        {
            Tuple<DataTable, DataTable, DataTable, DataTable> tables = GetTableFromList(staffs, false);
            using (SqlConnection connection = new SqlConnection(_sqlConnection))
            {
                connection.Open();
                SqlCommand sql_cmnd = new SqlCommand("UpdateStaffBulk", connection);
                sql_cmnd.CommandType = CommandType.StoredProcedure;
                sql_cmnd.Parameters.AddWithValue("@staff", tables.Item1);
                sql_cmnd.Parameters.AddWithValue("@teachingStaff", tables.Item2);
                sql_cmnd.Parameters.AddWithValue("@administrativeStaff", tables.Item3);
                sql_cmnd.Parameters.AddWithValue("@supportStaff", tables.Item4);
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

        public Tuple<DataTable, DataTable, DataTable, DataTable> GetTableFromList(List<Staff> staffs, bool makeIdUnique)
        {
            //Creating staff Table
            DataTable staffTable = new DataTable("StaffType");
            staffTable.Columns.Add("StaffID", typeof(int));
            staffTable.Columns.Add("Name", typeof(string));
            staffTable.Columns.Add("StaffTypeID", typeof(int));

            DataTable teachingStaffTable = new DataTable("TeachingStaffType");
            teachingStaffTable.Columns.Add("StaffID", typeof(int));
            teachingStaffTable.Columns.Add("SubjectName", typeof(string));
            

            DataTable administrativeStaffTable = new DataTable("AdministrativeStaffType");
            administrativeStaffTable.Columns.Add("StaffID", typeof(int));
            administrativeStaffTable.Columns.Add("Position", typeof(string));

            DataTable supportStaffTable = new DataTable("SupportStaffType");
            supportStaffTable.Columns.Add("StaffID", typeof(int));
            supportStaffTable.Columns.Add("Role", typeof(string));

            int uniqueStaffId = 1;
            foreach (Staff staff in staffs)
            {

                //Adding common Fields
                DataRow newStaffRow = staffTable.NewRow();
                newStaffRow["StaffID"] = makeIdUnique ? uniqueStaffId : staff.Id;
                newStaffRow["Name"] = staff.Name;
                newStaffRow["StaffTypeID"] = (int)staff.StaffType;
                staffTable.Rows.Add(newStaffRow);

                //Adding specific Fields
                switch ((int)staff.StaffType)
                {
                    case (int)StaffType.teachingStaff:
                        DataRow newTeachingStaffRow = teachingStaffTable.NewRow();
                        newTeachingStaffRow["StaffID"] = makeIdUnique ? uniqueStaffId : staff.Id;
                        newTeachingStaffRow["SubjectName"] = ((TeachingStaff)staff).SubjectName;
                        teachingStaffTable.Rows.Add(newTeachingStaffRow);
                        break;
                    case (int)StaffType.administrativeStaff:
                        DataRow newAdministrativeStaffRow = administrativeStaffTable.NewRow();
                        newAdministrativeStaffRow["StaffID"] = makeIdUnique ? uniqueStaffId : staff.Id;
                        newAdministrativeStaffRow["Position"] = ((AdministrativeStaff)staff).Position;
                        administrativeStaffTable.Rows.Add(newAdministrativeStaffRow);
                        break;
                    case (int)StaffType.supportStaff:
                        DataRow newSupportStaffRow = supportStaffTable.NewRow();
                        newSupportStaffRow["StaffID"] = makeIdUnique ? uniqueStaffId : staff.Id;
                        newSupportStaffRow["Role"] = ((SupportStaff)staff).Role;
                        supportStaffTable.Rows.Add(newSupportStaffRow);
                        break;
                }

                uniqueStaffId++;

            }

            return new Tuple<DataTable, DataTable, DataTable, DataTable>(staffTable, teachingStaffTable, administrativeStaffTable, supportStaffTable);
        }

        
    }
}
