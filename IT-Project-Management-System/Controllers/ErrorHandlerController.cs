using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace IT_Project_Management_System.Controllers
{
    public class ErrorHandlerController : Controller
    {
        public ActionResult NotFound()
        {
            return View();
        }
        
        public ActionResult Error()
        {
            return View();
        }
    }
}