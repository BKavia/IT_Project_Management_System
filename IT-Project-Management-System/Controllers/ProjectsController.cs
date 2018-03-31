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
    public class ProjectsController : BaseController
    {
        private SystemContext db = new SystemContext();

        // GET: Projects
        public ActionResult Index(string sortOrder, string searchString, string currentFilter, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            ViewBag.ShowSearchBox = true;
            var projects = db.Projects.Include(p => p.User); 
            if (searchString != null)
            {
                page = 1;
                projects = projects.Where(p => p.ProjectName.Contains(searchString) ||
                 p.ProjectDescription.Contains(searchString) ||
                 p.ProjectKey.Contains(searchString)
                 ).Include(p => p.User);
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            switch (sortOrder)
            {
                case "name_desc":
                    projects = projects.OrderByDescending(p => p.ProjectName);
                    break;
                case "Date":
                    projects = projects.OrderBy(p => p.ProjectStartDate);
                    break;
                case "date_desc":
                    projects = projects.OrderByDescending(p => p.ProjectStartDate );
                    break;
                default:
                    projects = projects.OrderBy(p => p.ProjectName);
                    break;
            }

            
            int pageSize = 8;
            int pageNumber = (page ?? 1);
            return View(projects.ToPagedList(pageNumber, pageSize));

        }
        public int CalculationPercentage(int NoOfValue, int NoOfTasks )
        {
            int percentage = 0;
            try
            {
                percentage = NoOfValue * 100 / NoOfTasks;
            }
            catch { }

            return percentage;
        }
        public ActionResult ProjectSummary(int projectId)
        {
            
            var tasks = db.Tasks.Where(t => t.ProjectID == projectId).Include(t => t.Project);
            int NoOfTasks = tasks.Count();
            ViewBag.NoOfTasks = NoOfTasks;


            var tasksCompleted = tasks.Where(t => t.TaskStatus == TaskStatus.Completed);
            int NoOfCompletedTasks = tasksCompleted.Count();
            ViewBag.NoOfCompletedTasks = NoOfCompletedTasks;
            ViewBag.percNoOfCompletedTasks = CalculationPercentage(NoOfCompletedTasks, NoOfTasks);


            var tasksInProgress = tasks.Where(t => t.TaskStatus == TaskStatus.InProgress);
            int NoOfTasksInProgress = tasksInProgress.Count();
            ViewBag.NoOfTasksInProgress = NoOfTasksInProgress;
            ViewBag.percNoOfTasksInProgress = CalculationPercentage(NoOfTasksInProgress, NoOfTasks);


            var tasksNotStarted = tasks.Where(t => t.TaskStatus == TaskStatus.NotStarted);
            int NoOfTasksNotStarted = tasksNotStarted.Count();
            ViewBag.NoOfTasksNotStarted = NoOfTasksNotStarted;
            ViewBag.percNoOfTasksNotStarted = CalculationPercentage(NoOfTasksNotStarted, NoOfTasks);

            Project project = db.Projects.Find(projectId);
            ViewBag.projectName = project.ProjectName;

            return View("ProjectSummary");
        }
        // GET: Projects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // GET: Projects/Create
        public ActionResult Create()
        {
            ViewBag.UserID = new SelectList(db.Users.Where(p => p.UserType == UserType.ProjectManager), "UserID", "FullName");
            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProjectID,UserID,ProjectKey,ProjectName,ProjectDescription,ProjectStartDate,ProjectEndDate,ProjectStatus")] Project project)
        {
            if(ModelState.IsValidField("ProjectStartDate") && ModelState.IsValidField("ProjectEndDate") && project.ProjectStartDate > project.ProjectEndDate)
            {
                ModelState.AddModelError("ProjectEndDate", @Resources.Resource.Enddateshouldbegraterthanstartdate);
            }
            if (ModelState.IsValid)
            {
                project.ProjectKey = project.ProjectKey.ToUpper();
                db.Projects.Add(project);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserID = new SelectList(db.Users.Where(p => p.UserType == UserType.ProjectManager), "UserID", "FullName", project.UserID);
            return View(project);
        }

        // GET: Projects/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserID = new SelectList(db.Users.Where(p => p.UserType == UserType.ProjectManager), "UserID", "FullName", project.UserID);
            return View(project);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProjectID,UserID,ProjectKey,ProjectName,ProjectDescription,ProjectStartDate,ProjectEndDate,ProjectStatus")] Project project)
        {
            if (ModelState.IsValidField("ProjectStartDate") && ModelState.IsValidField("ProjectEndDate") && project.ProjectStartDate > project.ProjectEndDate)
            {
                ModelState.AddModelError("ProjectEndDate", @Resources.Resource.Enddateshouldbegraterthanstartdate);
            }
            if (ModelState.IsValid)
            {
                project.ProjectKey = project.ProjectKey.ToUpper();
                db.Entry(project).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserID = new SelectList(db.Users.Where(p => p.UserType == UserType.ProjectManager), "UserID", "FullName", project.UserID);
            return View(project);
        }

        // GET: Projects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Project project = db.Projects.Find(id);
            db.Projects.Remove(project);
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
