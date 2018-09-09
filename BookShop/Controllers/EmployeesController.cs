using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataModelLayer;
using BusinessLayer;

namespace BookShop.Controllers
{
    public class EmployeesController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {

            return RedirectToAction("GetAllEmps");
            
        }

        [HttpGet]
        [Authorize]
        public ActionResult CreateEmp()
        {
            BusinessLogic.Users_Logic Bss = new BusinessLogic.Users_Logic();
            Users usr = Bss.Get_UserBy_ID(Convert.ToInt32(User.Identity.Name));
            ViewData["User_ID"] = usr.User_ID;
            ViewData["User_FName"] = usr.FirstName;
            return View();
        }

        [HttpPost]
        public ActionResult CreateEmp(Employees emp)
        {
            BusinessLogic.Employees_Logic be = new BusinessLogic.Employees_Logic();
            emp.User_ID = Convert.ToInt32(User.Identity.Name);
            be.CreateNewEmployee(emp);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize]
        public ActionResult GetAllEmps()
        {
            
            BusinessLogic.Employees_Logic Bss = new BusinessLogic.Employees_Logic();
            BusinessLogic.Users_Logic Bss2 = new BusinessLogic.Users_Logic();

            List<Employees> AllEmps = Bss.GetEmployees();
            Users usr = Bss2.Get_UserBy_ID(Convert.ToInt32(User.Identity.Name));

            ViewData["User_ID"] = usr.User_ID;
            ViewData["User_FName"] = usr.FirstName;

            return View(AllEmps);
        }

        [Authorize]
        [HttpGet]
        public ActionResult SearchEmployee()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SearchEmployee(Employees emp)
        {
            String FullName = emp.Emp_FName;
            String[] Fn = FullName.Split(' ');
            BusinessLogic.Employees_Logic Bss = new BusinessLogic.Employees_Logic();
            emp.Emp_FName = Fn[0];
            emp.emp_LName = Fn[1];
            Employees ActuallyEmp = Bss.Get_Employee_By_Name(emp);
            if (ActuallyEmp.Employee_ID == 0) return RedirectToAction("UserNotFound");
            return RedirectToAction("Details", "Employees", ActuallyEmp);
        }

        [Authorize]
        [HttpGet]
        public ActionResult Details(Employees emp)
        {
            return View(emp);
        }

        [Authorize]
        [HttpGet]
        public ActionResult Details_From_Main(int? id)
        {
            BusinessLogic.Employees_Logic Bss = new BusinessLogic.Employees_Logic();
            Employees NwEmp = Bss.Get_Employee_By_ID(id);
            return View(NwEmp);
        }

        [Authorize]
        [HttpGet]
        public ActionResult UserNotFound()
        {
            return View();
        }


        [Authorize]
        [HttpGet]
        public ActionResult UpdateEmployee(int ?id)
        {
            BusinessLogic.Users_Logic Bss = new BusinessLogic.Users_Logic();
            Users usr = Bss.Get_UserBy_ID(Convert.ToInt32(User.Identity.Name));

            BusinessLogic.Employees_Logic Ess = new BusinessLogic.Employees_Logic();
            Employees empp = Ess.Get_Employee_By_ID(id);
            Session["Emp_ID"] = id;
            ViewData["User_ID"] = usr.User_ID;
            ViewData["User_FName"] = usr.FirstName;
            return View(empp);
        }

        [HttpPost]
        public ActionResult UpdateEmployee(Employees emp)
        {
            BusinessLogic.Employees_Logic be = new BusinessLogic.Employees_Logic();
            emp.Employee_ID = Convert.ToInt32(Session["Emp_ID"].ToString());
            be.Update_Employee(emp);
            return RedirectToAction("Index");
        }

        [Authorize]
        [HttpGet]
        public ActionResult DeleteEmployee(int id)
        {
            BusinessLogic.Employees_Logic Ess = new BusinessLogic.Employees_Logic();
            Ess.Delete_Employee(id);
            return RedirectToAction("Index");
        }
    }
}