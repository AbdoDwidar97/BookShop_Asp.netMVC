using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModelLayer;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using DataModelLayer;

namespace BusinessLayer
{
    public class DataAccess
    {

        public struct Users_Access
        {

            public bool UserLogin(String Email, String Pwd)
            {

                String ConnectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection Conn = new SqlConnection(ConnectionString))
                {

                    SqlCommand Comm = new SqlCommand("Sp_UserLogin", Conn);
                    Comm.CommandType = CommandType.StoredProcedure;
                    Comm.Parameters.AddWithValue("@Email", Email);
                    Comm.Parameters.AddWithValue("@Pwd", Pwd);
                    Conn.Open();

                    SqlDataReader Dr = Comm.ExecuteReader();
                    if (Dr.HasRows) return true;
                    else return false;
                }

            }

            public Users Get_User_ByEmail(String email)
            {
                
                String ConnectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection Conn = new SqlConnection(ConnectionString))
                {

                    SqlCommand Comm = new SqlCommand("Sp_Get_UserByEmail", Conn);
                    Comm.CommandType = CommandType.StoredProcedure;
                    Comm.Parameters.AddWithValue("@Email", email);
                    Conn.Open();

                    SqlDataReader Dr = Comm.ExecuteReader();
                    Dr.Read();
                    Users usr = new Users();

                    usr.User_ID = Convert.ToInt32(Dr["User_ID"].ToString());
                    usr.FirstName = Dr["FirstName"].ToString();
                    usr.LastName = Dr["LastName"].ToString();
                    usr.Phone = Dr["Phone"].ToString();
                    usr.Email = Dr["Email"].ToString();
                    usr.Permission_ID = Convert.ToInt32(Dr["Permission_ID"].ToString());
                    return usr;
                }
                
            }

            public void CreateNewUser(Users usr)
            {
                String ConnectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection Conn = new SqlConnection(ConnectionString))
                {

                    SqlCommand Comm = new SqlCommand("Sp_CreateNewUser", Conn);
                    Comm.CommandType = CommandType.StoredProcedure;
                    Comm.Parameters.AddWithValue("@FirstName", usr.FirstName);
                    Comm.Parameters.AddWithValue("@LastName", usr.LastName);
                    Comm.Parameters.AddWithValue("@Phone", usr.Phone);
                    Comm.Parameters.AddWithValue("@Email", usr.Email);
                    Comm.Parameters.AddWithValue("@Permission_ID", usr.Permission_ID);
                    Comm.Parameters.AddWithValue("@Pwd", usr.Pwd);
                    Conn.Open();
                    Comm.ExecuteNonQuery();
                }
            }

            public List<Users> GetAll_Users()
            {
                List<Users> AllUsers = new List<Users>();
                String ConnectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection Conn = new SqlConnection(ConnectionString))
                {

                    SqlCommand Comm = new SqlCommand("Sp_GetAll_Users", Conn);
                    Comm.CommandType = CommandType.StoredProcedure;
                    Conn.Open();

                    SqlDataReader Dr = Comm.ExecuteReader();
                    while (Dr.Read())
                    {
                        Users usr = new Users();
                        usr.User_ID = Convert.ToInt32(Dr["User_ID"].ToString());
                        usr.FirstName = Dr["FirstName"].ToString();
                        usr.LastName = Dr["LastName"].ToString();
                        usr.Phone = Dr["Phone"].ToString();
                        usr.Email = Dr["Email"].ToString();
                        usr.Permission_ID = Convert.ToInt32(Dr["Permission_ID"].ToString());
                        AllUsers.Add(usr);
                    }
                    
                    return AllUsers;
                }
            }
        }

        public struct Employees_Access
        {
            public void CreateNewEmployee(Employees emp)
            {
                String ConnectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection Conn = new SqlConnection(ConnectionString))
                {

                    SqlCommand Comm = new SqlCommand("Sp_CreateNewEmployee", Conn);
                    Comm.CommandType = CommandType.StoredProcedure;
                    Comm.Parameters.AddWithValue("@Emp_FName", emp.Emp_FName);
                    Comm.Parameters.AddWithValue("@emp_LName", emp.emp_LName);
                    Comm.Parameters.AddWithValue("@Phone", emp.Phone);
                    Comm.Parameters.AddWithValue("@Branch", emp.Branch);
                    Comm.Parameters.AddWithValue("@Job_ID", emp.Job_ID);
                    Comm.Parameters.AddWithValue("@Basic_Salary", emp.Basic_Salary);
                    Comm.Parameters.AddWithValue("@User_ID", emp.User_ID);
                    Conn.Open();
                    Comm.ExecuteNonQuery();
                }
            }

            public List<Employees> GetAllEmployees()
            {
                List<Employees> AllEmps = new List<Employees>();

                String ConnectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection Conn = new SqlConnection(ConnectionString))
                {

                    SqlCommand Comm = new SqlCommand("Sp_GetAll_Employees", Conn);
                    Comm.CommandType = CommandType.StoredProcedure;
                    Conn.Open();
                    SqlDataReader Dr = Comm.ExecuteReader();
                    while (Dr.Read())
                    {
                        Employees NewEmp = new Employees();
                        NewEmp.Employee_ID = Convert.ToInt32(Dr["Employee_ID"].ToString());
                        NewEmp.Emp_FName = Dr["Emp_FName"].ToString();
                        NewEmp.emp_LName = Dr["emp_LName"].ToString();
                        NewEmp.Phone = Dr["Phone"].ToString();
                        NewEmp.Branch = Dr["Branch"].ToString();
                        NewEmp.Basic_Salary = Convert.ToDouble(Dr["Basic_Salary"].ToString());
                        NewEmp.Job_ID = Convert.ToInt32(Dr["Job_ID"].ToString());
                        NewEmp.User_ID = Convert.ToInt32(Dr["User_ID"].ToString());
                        AllEmps.Add(NewEmp);
                    }
                }

                return AllEmps;
            }

            public void UpdateEmployee(Employees emp)
            {
                String ConnectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection Conn = new SqlConnection(ConnectionString))
                {

                    SqlCommand Comm = new SqlCommand("Sp_UpdateEmployee", Conn);
                    Comm.CommandType = CommandType.StoredProcedure;
                    Comm.Parameters.AddWithValue("@Emp_FName", emp.Emp_FName);
                    Comm.Parameters.AddWithValue("@emp_LName", emp.emp_LName);
                    Comm.Parameters.AddWithValue("@Phone", emp.Phone);
                    Comm.Parameters.AddWithValue("@Branch", emp.Branch);
                    Comm.Parameters.AddWithValue("@Job_ID", emp.Job_ID);
                    Comm.Parameters.AddWithValue("@Basic_Salary", emp.Basic_Salary);
                    Comm.Parameters.AddWithValue("@Employee_ID", emp.Employee_ID);
                    Conn.Open();
                    Comm.ExecuteNonQuery();
                }
            }

            public void DeleteEmployee(int id)
            {
                String ConnectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection Conn = new SqlConnection(ConnectionString))
                {

                    SqlCommand Comm = new SqlCommand("Sp_DeleteEmployee", Conn);
                    Comm.CommandType = CommandType.StoredProcedure;
                    Comm.Parameters.AddWithValue("@Employee_ID",id);
                    Conn.Open();
                    Comm.ExecuteNonQuery();
                }
            }


        }
        
    }
}
