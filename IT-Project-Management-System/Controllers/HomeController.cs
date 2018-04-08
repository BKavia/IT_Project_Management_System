﻿using IT_Project_Management_System.Models;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace IT_Project_Management_System.Controllers
{
    public class HomeController : BaseController
    {
        private SystemContext db = new SystemContext();
        // GET: Login
       
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "UserName,UserPassword")] User user)
        {

            if (ModelState.IsValidField("UserName") && ModelState.IsValidField("UserPassword"))
            {
               // bool found = Membership.ValidateUser(user.UserName, user.UserPassword);
                User foundUser = db.Users.SingleOrDefault((u => u.UserName.Equals(user.UserName) && u.UserPassword.Equals(user.UserPassword)));
            
                if (foundUser == null)
                {
                    ViewBag.errorMessage =  "Username was not found";
                }
                else
                {
                    SetCulture(foundUser.Language.ToString());

                    var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                    string userdata = serializer.Serialize(foundUser);
                    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
    1,                                     // ticket version
    foundUser.FullName,                              // authenticated username
    DateTime.Now,                          // issueDate
    DateTime.Now.AddMinutes(30),           // expiryDate
    false,                          // true to persist across browser sessions
    userdata,                              // can be used to store additional user data
    FormsAuthentication.FormsCookiePath);  // the path for the cookie

                    // Encrypt the ticket using the machine key
                    string encryptedTicket = FormsAuthentication.Encrypt(ticket);

                    // Add the cookie to the request to save it
                    HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                    cookie.HttpOnly = true;
                    Response.Cookies.Add(cookie);

                    if (foundUser.UserType == UserType.Administrator || foundUser.UserType == UserType.ProjectManager)
                    {
                        return RedirectToAction("Index", "Projects");
                    }
                    else {
                        return RedirectToAction("Index", "Tasks");

                    }

                }
            }
            
            return View(user);
        }
     

    }
}