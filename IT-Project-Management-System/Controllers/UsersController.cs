﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IT_Project_Management_System.Models;

namespace IT_Project_Management_System.Controllers
{
    
    public class UsersController : BaseController
    {
        private SystemContext db = new SystemContext();

        // GET: Users
        public ActionResult Index(string searchString)
        {
            ViewBag.ShowSearchBox = true;
            var users = db.Users.AsQueryable();
            if (searchString != null)
            {
                users = users.Where(s => s.UserName.Contains(searchString) ||
                 s.FirstName.Contains(searchString) ||
                 s.LastName.Contains(searchString) ||
                  s.Email.Contains(searchString)
                 );
            }
            return View(users.ToList());
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

        [HttpPost]
        public JsonResult DoesUserExist(string Username)
        {
            var userFound = !db.Users.Any(u => u.UserName == Username);
            return Json(userFound,JsonRequestBehavior.AllowGet);
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
                db.Entry(user).State = EntityState.Modified;
                SetCulture(user.Language.ToString());
                db.SaveChanges();
                ViewBag.Message = "Your details have been updated";
            }
            return View(user);
        }
    }
}
