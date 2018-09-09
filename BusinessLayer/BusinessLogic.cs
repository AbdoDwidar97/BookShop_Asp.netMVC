using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModelLayer;

namespace BusinessLayer
{
    public class BusinessLogic
    {

        public struct Users_Logic
        {

            public bool UserLogin(Users usr)
            {
                DataAccess.Users_Access Dss = new DataAccess.Users_Access();
                return Dss.UserLogin(usr.Email, usr.Pwd);

            }

            public void CreateUser(Users usr)
            {
                DataAccess.Users_Access Dss = new DataAccess.Users_Access();
                Dss.CreateNewUser(usr);
            }

            public Users Get_UserByEmail(String eml)
            {
                DataAccess.Users_Access Dss = new DataAccess.Users_Access();
                return Dss.Get_User_ByEmail(eml);
            }

            public Users Get_UserBy_ID(int ID)
            {
                Users usr = new Users();
                DataAccess.Users_Access Dss = new DataAccess.Users_Access();
                List<Users> AllUsrs = Dss.GetAll_Users();
                foreach (Users itr in AllUsrs)
                {
                    if (itr.User_ID == ID)
                    {
                        usr = itr;
                        break;
                    }
                }

                return usr;
            }
        }

        public struct Employees_Logic
        {
            public void CreateNewEmployee(Employees emp)
            {
                DataAccess.Employees_Access Dss = new DataAccess.Employees_Access();
                Dss.CreateNewEmployee(emp);
            }

            public List<Employees> GetEmployees()
            {
                DataAccess.Employees_Access Dss = new DataAccess.Employees_Access();
                return Dss.GetAllEmployees();
            }

            public Employees Get_Employee_By_Name(Employees emp)
            {
                Employees emm = new Employees();
                List<Employees> All = GetEmployees();
                foreach (Employees itr in All)
                {
                    if (itr.Emp_FName == emp.Emp_FName && itr.emp_LName == emp.emp_LName)
                    {
                        emm = itr;
                        break;
                    }
                }
                return emm;
            }

            public Employees Get_Employee_By_ID(int? id)
            {
                Employees emm = new Employees();
                List<Employees> All = GetEmployees();
                foreach (Employees itr in All)
                {
                    if (itr.Employee_ID == id)
                    {
                        emm = itr;
                        break;
                    }
                }
                return emm;
            }

            public void Update_Employee(Employees emp)
            {
                DataAccess.Employees_Access Dss = new DataAccess.Employees_Access();
                Dss.UpdateEmployee(emp);
            }

            public void Delete_Employee(int id)
            {
                DataAccess.Employees_Access Dss = new DataAccess.Employees_Access();
                Dss.DeleteEmployee(id);
            }


        }
    }
}
