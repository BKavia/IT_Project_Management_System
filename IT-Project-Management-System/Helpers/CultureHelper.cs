using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace IT_Project_Management_System.Helpers
{
    public static class CultureHelper
    {
       
        //Langiages supported
        private static readonly List<string> _cultures = new List<string> {
        "en-GB",  // DEFAULT english language
        "es", // Spanish
        "fr"  // Freanch
        
        };

        /// <summary>
        /// Returns a valid culture name based on "name" parameter. If "name" is not valid, it returns the default culture "en-GB"
        /// </summary>
        /// <param name="name" />Culture's name (e.g. en-GB)</param>
        public static string GetImplementedCulture(string name)
        {
            // make sure it's not null
            if (string.IsNullOrEmpty(name))
                return GetDefaultCulture(); // return Default culture
                                            // make sure it is a valid culture first
            
                                            // if it is implemented, accept it
            if (_cultures.Where(c => c.Equals(name, StringComparison.InvariantCultureIgnoreCase)).Count() > 0)
                return name; // accept it
                             // Find a close match. For example, if you have "en-GB" defined and the user requests "en-GB", 
                             // the function will return closes match that is "en-GB" because at least the language is the same (ie English)  
             return GetDefaultCulture(); // return Default culture as no match found
        }
        /// <summary>
        /// Returns default culture name which is the first name decalared (e.g. en-GB)
        /// </summary>
        /// <returns></returns>
        public static string GetDefaultCulture()
        {
            return _cultures[0]; // return Default culture
        }

        public static string GetCurrentCulture()
        {
            return Thread.CurrentThread.CurrentCulture.Name;
        }
    }
}

