using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using IT_Project_Management_System.Helpers;
using IT_Project_Management_System.Models;
using PagedList;

namespace IT_Project_Management_System.Controllers
{
    //Controller for Tasks.
    [Authorize]
    public class TasksController : BaseController
    {
        private SystemContext db = new SystemContext();

         // GET: Gets the Tasks depending on the passed criteria
        public ActionResult Index(int? projectId, string sortOrder, string searchString, string taskstatusList, Boolean? myTasksOnly, string currentFilter, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.TaskKeySortParm = String.IsNullOrEmpty(sortOrder) ? "taskKey_desc" : "";
            ViewBag.TaskStatusSortParm = sortOrder == "TaskStatus" ? "taskStatus_desc" : "TaskStatus";
            ViewBag.ShowSearchBox = true;
            ViewBag.onlyMyTasks = (myTasksOnly == null)?true: Convert.ToBoolean(myTasksOnly);
            ViewBag.CurrentFilter = searchString;

            IQueryable<Task> tasks = db.Tasks.Include(t => t.Project).Include(t => t.User);
            User user = UserHelper.getUser();
            if (taskstatusList != null && taskstatusList != "All") {
                TaskStatus selectedStatus = (TaskStatus)Enum.Parse(typeof(TaskStatus), taskstatusList);
                tasks = tasks.Where(t => t.TaskStatus == selectedStatus);
            }
         
            if (projectId != null)
            {
                tasks = tasks.Where(t => t.ProjectID == projectId);
            }
            if (user.UserType == UserType.ProjectManager)
            {
                tasks = tasks.Where(p => p.Project.UserID == user.UserID);
            }
                if (user.UserType == UserType.TeamMember)
            {
                tasks = tasks.Where(p => p.Project.ProjectStatus != ProjectStatus.Completed);
                if (ViewBag.onlyMyTasks == true)
                {
                    tasks = tasks.Where(u => u.UserID == user.UserID);
                }
            }

       
            IEnumerable<Task> ts = tasks.ToList();
            if (searchString != null)
            {
                page = 1;
                String searchStringUpper = searchString.ToUpper();
                ts = ts.Where(s => s.TaskName.Contains(searchStringUpper) ||
                 s.User.FullName.ToUpper().Contains(searchStringUpper) ||
                 s.TaskKey.ToUpper().Contains(searchStringUpper)
                 );
            }
            else
            {
                searchString = currentFilter;
            }

            switch (sortOrder)
            {
                case "taskKey_desc":
                    ts = ts.OrderByDescending(s => s.TaskKey);
                    break;
                case "TaskStatus":
                    ts = ts.OrderBy(s => s.TaskStatus);
                    break;
                case "taskStatus_desc":
                    ts = ts.OrderByDescending(s => s.TaskStatus);
                    break;
                default:
                    ts = ts.OrderBy(s => s.TaskKey);
                    break;
            }

            int pageSize = 8;
            int pageNumber = (page ?? 1);
            return View(ts.ToPagedList(pageNumber, pageSize));
           
        }

        //Load the Task for Edit
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

        //Load the Objects needed to be displayed on the Create page
        // GET: Tasks/Create
        public ActionResult Create()
        {
            ViewBag.ProjectID = new SelectList(db.Projects, "ProjectID", "ProjectName");
            ViewBag.UserID = new SelectList(db.Users.Where(u => u.UserType == UserType.TeamMember), "UserID", "FullName");
            return View();
        }

        // POST: Tasks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TaskID,ProjectID,TaskName,TaskDescription,TaskStartDate,TaskEndDate,TaskStatus,UserID")] Task task)
        {
            if (ModelState.IsValidField("TaskStartDate") && ModelState.IsValidField("TaskEndDate") && task.TaskStartDate > task.TaskEndDate)
            {
                ModelState.AddModelError("TaskEndDate", @Resources.Resource.Enddateshouldbegraterthanstartdate);
            }

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
            ViewBag.UserID = new SelectList(db.Users.Where(u => u.UserType == UserType.TeamMember), "UserID", "FullName", task.UserID);
            User user = UserHelper.getUser();

            if (task.UserID != user.UserID)
            {
                ViewBag.isTaskStatusDisabled = true;
            }
            else
            {
                ViewBag.isTaskStatusDisabled = false;
            }
            return View(task);
        }

        // POST: Tasks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TaskID,ProjectID,TaskName,TaskDescription,TaskStartDate,TaskEndDate,TaskStatus,UserID")] Task task)
        {
            if (ModelState.IsValidField("TaskStartDate") && ModelState.IsValidField("TaskEndDate") && task.TaskStartDate > task.TaskEndDate)
            {
                ModelState.AddModelError("TaskEndDate", @Resources.Resource.Enddateshouldbegraterthanstartdate);
            }
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
