using IT_Project_Management_System.Helpers;
using IT_Project_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace IT_Project_Management_System.Controllers
{
    public class LoginController : BaseController
    {
        private SystemContext db = new SystemContext();
        // GET: Login
       
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                FormsAuthentication.SignOut();
                Session.Abandon();
                Session["loggedUser"] = null;
            }
            return View();
        }

        [HttpGet]
        public ActionResult LogOff()
        {
            if (User.Identity.IsAuthenticated)
            {
                FormsAuthentication.SignOut();
                Session.Abandon();
                Session["loggedUser"] = null;
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Index([Bind(Include = "UserName,UserPassword")] User user)
        {

            User foundUser = db.Users.SingleOrDefault((u => u.UserName.Equals(user.UserName) && u.UserPassword.Equals(user.UserPassword)));
            
            if (foundUser == null)
            {
                ViewBag.errorMessage = "User was not found";
            }
            else
            {
                SetCulture(foundUser.Language.ToString());
                Session.Add("loggedUser", foundUser);
                FormsAuthentication.SetAuthCookie(user.UserName, false);
           
                // var authTicket = new FormsAuthenticationTicket(1, user.UserName, DateTime.Now, DateTime.Now.AddMinutes(20), false, foundUser.UserType.ToString());
                //string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                //var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                //HttpContext.Response.Cookies.Add(authCookie);

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