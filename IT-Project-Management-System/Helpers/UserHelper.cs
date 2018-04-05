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
       
        // Include ONLY cultures you are implementing
        private static readonly List<string> _cultures = new List<string> {
        "en-GB",  // first culture is the DEFAULT
        "es", // Spanish NEUTRAL culture
        "fr"  // Freanch NEUTRAL culture
        
    };


        public static User getUser()
        {
            
            FormsIdentity id = (FormsIdentity)HttpContext.Current.User.Identity;
            string userData = id.Ticket.UserData;
            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            User User1 = serializer.Deserialize<User>(userData);
            return User1;
        }

        public static string getIdentityName()
        {
            return HttpContext.Current.User.Identity.Name;
        }
       
    }
}

