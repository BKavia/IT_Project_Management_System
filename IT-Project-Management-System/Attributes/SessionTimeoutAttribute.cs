using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IT_Project_Management_System.Attributes
{
    public class SessionTimeoutAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext ctx = HttpContext.Current;
            if (HttpContext.Current.Session["loggedUser"] == null)
            {
                filterContext.Result = new RedirectResult("~/Login");
                return;
            }
            base.OnActionExecuting(filterContext);
        }
    }
}