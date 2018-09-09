using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataModelLayer;
using BusinessLayer;
using System.Web.Security;

namespace BookShop.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {

            ViewData["Listss"] = new List<String>
            {

                "Create new Employees",
                "Create new Users",
                "Create new Customers",
                "Create new Vendors",
                "Add new Store",
                "Add new Purchase",
                "Add new Sales",
                "Payments of purchases",
                "Payments of sales"
            };
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated) return RedirectToAction("SignOut");
            else return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Users u)
        {
            BusinessLogic.Users_Logic Blgc = new BusinessLogic.Users_Logic();
            bool isLogin = Blgc.UserLogin(u);
            if (isLogin)
            {
                Users usr = Blgc.Get_UserByEmail(u.Email);
                FormsAuthentication.SetAuthCookie(usr.User_ID.ToString(),false);
                return RedirectToAction("ViewMain");
            }
            return View(u);
        }

        [HttpGet]
        [Authorize]
        public ActionResult CreateUser()
        {
            BusinessLogic.Users_Logic Bss = new BusinessLogic.Users_Logic();
            Users usr = Bss.Get_UserBy_ID(Convert.ToInt32(User.Identity.Name));
            if (usr.Permission_ID != 1) return RedirectToAction("ViewMain"); 
            ViewData["User_ID"] = usr.User_ID;
            ViewData["User_FName"] = usr.FirstName;
            return View();
        }

        [HttpPost]
        public ActionResult CreateUser(Users u)
        {
            BusinessLogic.Users_Logic Blgc = new BusinessLogic.Users_Logic();
            Blgc.CreateUser(u);
            return RedirectToAction("ViewMain");
        }

        [HttpGet]
        [Authorize]
        public ActionResult ViewMain()
        {
           
            BusinessLogic.Users_Logic Bss = new BusinessLogic.Users_Logic();
            Users usr = Bss.Get_UserBy_ID(Convert.ToInt32(User.Identity.Name));
            ViewData["User_ID"] = usr.User_ID;
            ViewData["User_FName"] = usr.FirstName;

            return View();
        }

        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
        
    }

}