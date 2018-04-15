using IT_Project_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Security;

namespace IT_Project_Management_System.Helpers
{
    public static class UserHelper
    {
        public static User GetUser()
        {
            FormsIdentity id = (FormsIdentity)HttpContext.Current.User.Identity;
            string userData = id.Ticket.UserData;
            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            User user = serializer.Deserialize<User>(userData);
            return user;
        }

        public static string GetIdentityName()
        {
            return HttpContext.Current.User.Identity.Name;
        }
       
    }
}

