﻿using IT_Project_Management_System.Helpers;
using System;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace IT_Project_Management_System.Controllers
{
    //Base Controller that most Controllers extend.
    public class BaseController : Controller
    {
        //Checks the cookie for the Culture and set the selection in the current Thread
        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            string cultureName = null;
           
            // Attempt to read the culture cookie from Request
            HttpCookie cultureCookie = Request.Cookies["_culture"];
            if (cultureCookie != null)
            {
                cultureName = cultureCookie.Value;
            }
            
            cultureName = CultureHelper.GetImplementedCulture(cultureName);

            // Modify current thread's cultures            
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(cultureName);
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;

            return base.BeginExecuteCore(callback, state);
        }

        //Mehtod is used to set the Language selected to the session and create a cookie.
        public void SetCulture(string culture)
        {
            culture = CultureHelper.GetImplementedCulture(culture);
            
            HttpCookie cookie = Request.Cookies["_culture"];
            if (cookie != null)
                cookie.Value = culture; 
            else
            {
                cookie = new HttpCookie("_culture")
                {
                    Value = culture,
                    Expires = DateTime.Now.AddYears(1)
                };
            }
            Session.Add("culture", culture);
            Response.Cookies.Add(cookie);
        }
    }
}