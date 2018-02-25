using IT_Project_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IT_Project_Management_System.Controllers
{
    public class LoginController : Controller
    {
        private SystemContext db = new SystemContext();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "UserName,UserPassword")] User user)
        {
            User foundUser = db.Users.SingleOrDefault((u => u.UserName.Equals(user.UserName) && u.UserPassword.Equals(user.UserPassword)));
            
            if (foundUser == null)
            {
                ViewBag.errorMessage = "User was not found";
            }
            else
            {
                if (foundUser.UserType == UserType.Administrator || foundUser.UserType == UserType.ProjectManager)
                {
                    return RedirectToAction("Index", "Projects");
                }
                else {
                    return RedirectToAction("Index", "Tasks");

                }

            }
           
            return View(user);
        }

    }
}