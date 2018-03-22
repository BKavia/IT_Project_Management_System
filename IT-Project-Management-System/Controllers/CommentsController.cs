using System;
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
    public class CommentsController : BaseController
    {
        private SystemContext db = new SystemContext();

        // GET: Comments
        public ActionResult Index()
        {
            var comments = db.Comments.Include(c => c.Task).Include(c => c.User);
            return View(comments.ToList());
        }

        // GET: Comments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // GET: Comments/Create
        public ActionResult Create()
        {
            ViewBag.TaskID = new SelectList(db.Tasks, "TaskID", "TaskName");
            ViewBag.UserID = new SelectList(db.Users, "UserID", "FirstName");
            return View();
        }

        // POST: Comments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public PartialViewResult Create([Bind(Include = "TaskID,CommentText")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                User loggedUser = (User)Session["loggedUser"];
                comment.CommentDate = DateTime.Now;
                comment.UserID = loggedUser.UserID;
                db.Comments.Add(comment);
                db.SaveChanges();
            }
            Task task = db.Tasks.Find(comment.TaskID);
            return PartialView("~/Views/Tasks/_PartialCommentList.cshtml", task);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult getPartial(Comment comment)
        {
            return PartialView("~/Views/Tasks/_PartialComment.cshtml", comment);
        }

        // GET: Comments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            ViewBag.TaskID = new SelectList(db.Tasks, "TaskID", "TaskName", comment.TaskID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "FirstName", comment.UserID);
            return View(comment);
        }

        // POST: Comments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CommentID,TaskID,UserID,CommentText,CommentDate")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(comment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TaskID = new SelectList(db.Tasks, "TaskID", "TaskName", comment.TaskID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "FirstName", comment.UserID);
            return View(comment);
        }

        // GET: Comments/Delete/5
        public ActionResult Delete(int? id, int taskID)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment != null)
            {
                db.Comments.Remove(comment);
                db.SaveChanges();
            }
            return RedirectToAction("Details", "Tasks", new { id = taskID });
        }

        // POST: Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Comment comment = db.Comments.Find(id);
            db.Comments.Remove(comment);
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
    }
}
