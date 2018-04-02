using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IT_Project_Management_System.Attributes;
using IT_Project_Management_System.Models;
using PagedList;

namespace IT_Project_Management_System.Controllers
{
    [SessionTimeout]
    public class UsersController : BaseController
    {
        private SystemContext db = new SystemContext();

        // GET: Users
        public ActionResult Index(string sortOrder, string searchString, string currentFilter, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.UserTypeSortParm = sortOrder == "UserType" ? "userType_desc" : "UserType";
            ViewBag.ShowSearchBox = true;
            var users = db.Users.AsQueryable();
            if (searchString != null)
            {
                page = 1;
                users = users.Where(s => s.UserName.Contains(searchString) ||
                 s.FirstName.Contains(searchString) ||
                 s.LastName.Contains(searchString) ||
                  s.Email.Contains(searchString) ||
                  s.UserType.ToString().Contains(searchString) ||
                  s.UserName.Contains(searchString)
                 );
            } else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            switch (sortOrder)
            {
                case "name_desc":
                    users = users.OrderByDescending(s => s.FirstName);
                    break;
                case "UserType":
                    users = users.OrderBy(s => s.UserType);
                    break;
                case "userType_desc":
                    users = users.OrderByDescending(s => s.UserType);
                    break;
                default:
                    users = users.OrderBy(s => s.FirstName);
                    break;
            }
            int pageSize = 8;
            int pageNumber = (page ?? 1);
            return View(users.ToPagedList(pageNumber, pageSize));
         
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserID,FirstName,LastName,Email,PhoneNumber,UserName,UserPassword,UserType,Language")] User user)
        {
            if (db.Users.Any(u => u.UserName == user.UserName))
            {
                ModelState.AddModelError("UserName", @Resources.Resource.UserNameTaken);

            }
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserID,FirstName,LastName,Email,PhoneNumber,UserName,UserPassword,UserType,Language")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        
        // GET: Users/EditProfile/5
        public ActionResult EditProfile()
        {
          
            User loggedUser = (User)Session["loggedUser"];
            return View(loggedUser);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProfile([Bind(Include = "UserID,FirstName,LastName,Email,PhoneNumber,UserName,UserPassword,UserType,Language")] User user)
        {
            if (ModelState.IsValid)
            {
                User loggedUser = (User)Session["loggedUser"];
                user.UserType = loggedUser.UserType;

                db.Entry(user).State = EntityState.Modified;
                SetCulture(user.Language.ToString());
                db.SaveChanges();
                ViewBag.Message = @Resources.Resource.Yourdetailshavebeenupdated;
            }
            return View(user);
        }
    }
}
