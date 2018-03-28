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

    public class TasksController : BaseController
    {
        private SystemContext db = new SystemContext();

        public ActionResult ViewProjectTasks(int projectId)
        {
            ViewBag.ShowSearchBox = true;
            var tasks = db.Tasks.Where(t => t.ProjectID == projectId).Include(t => t.Project).Include(t => t.User);
            return View("Index",tasks.ToList());
        }

        // GET: Tasks
        public ActionResult Index(string searchString)
        {
            ViewBag.ShowSearchBox = true;
            var tasks = db.Tasks.Include(t => t.Project).Include(t => t.User);
            IEnumerable<Task> ts = tasks.ToList();

            if (searchString != null)
            {
                String searchStringUpper = searchString.ToUpper();
                ts = ts.Where(s => s.TaskName.Contains(searchStringUpper) ||
                 s.TaskDescription.ToUpper().Contains(searchStringUpper) ||
                 s.TaskKey.ToUpper().Contains(searchStringUpper)
                 );
              }
           
            return View(ts);
        }

        // GET: Tasks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task task = db.Tasks.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }

        // GET: Tasks/Create
        public ActionResult Create()
        {
            ViewBag.ProjectID = new SelectList(db.Projects, "ProjectID", "ProjectName");
            ViewBag.UserID = new SelectList(db.Users.Where(u => u.UserType == UserType.NormalUser), "UserID", "FullName");
            return View();
        }

        // POST: Tasks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TaskID,ProjectID,TaskName,TaskDescription,TaskStartDate,TaskEndDate,TaskStatus,UserID")] Task task)
        {
            if (ModelState.IsValid)
            {
                db.Tasks.Add(task);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProjectID = new SelectList(db.Projects, "ProjectID", "ProjectName", task.ProjectID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "FirstName", task.UserID);
            return View(task);
        }

        // GET: Tasks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task task = db.Tasks.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProjectID = new SelectList(db.Projects, "ProjectID", "ProjectName", task.ProjectID);
            ViewBag.UserID = new SelectList(db.Users.Where(u => u.UserType == UserType.NormalUser), "UserID", "FullName", task.UserID);
            return View(task);
        }

        // POST: Tasks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TaskID,ProjectID,TaskName,TaskDescription,TaskStartDate,TaskEndDate,TaskStatus,UserID")] Task task)
        {
            if (ModelState.IsValid)
            {
                db.Entry(task).State = EntityState.Modified;
                db.SaveChanges();
                string fromDetails = Request.QueryString["from"];
                if (fromDetails != null)
                {
                    return RedirectToAction("Details", new { id = task.TaskID });
                }
                return RedirectToAction("Index");
                 
            }
            ViewBag.ProjectID = new SelectList(db.Projects, "ProjectID", "ProjectName", task.ProjectID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "FirstName", task.UserID);
            return View(task);
        }

        // GET: Tasks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task task = db.Tasks.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }

        // POST: Tasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Task task = db.Tasks.Find(id);
            db.Tasks.Remove(task);
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
