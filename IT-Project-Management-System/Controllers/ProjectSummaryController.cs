using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using IT_Project_Management_System.Helpers;
using IT_Project_Management_System.Models;

namespace IT_Project_Management_System.Controllers
{
    [Authorize]
    public class ProjectSummaryController : BaseController
    {
        private SystemContext db = new SystemContext();

        //Method used to calculated percentage values for the tasks.
        public int CalculationPercentage(int NoOfValue, int NoOfTasks)
        {
            int percentage = 0;
            try
            {
                percentage = NoOfValue * 100 / NoOfTasks;
            }
            catch { }

            return percentage;
        }

        //Calculate all the Project Summary details needed for the report page
        public ActionResult ProjectSummary(int projectId)
        {

            var tasks = db.Tasks.Where(t => t.ProjectID == projectId).Include(t => t.Project);
            int NoOfTasks = tasks.Count();
            ViewBag.ProjectId = projectId;

            var tasksCompleted = tasks.Where(t => t.TaskStatus == TaskStatus.Completed);
            int NoOfCompletedTasks = tasksCompleted.Count();
            ViewBag.percNoOfCompletedTasks = CalculationPercentage(NoOfCompletedTasks, NoOfTasks);

            var tasksInProgress = tasks.Where(t => t.TaskStatus == TaskStatus.InProgress);
            int NoOfTasksInProgress = tasksInProgress.Count();
            ViewBag.percNoOfTasksInProgress = CalculationPercentage(NoOfTasksInProgress, NoOfTasks);

            var tasksNotStarted = tasks.Where(t => t.TaskStatus == TaskStatus.NotStarted);
            int NoOfTasksNotStarted = tasksNotStarted.Count();
            ViewBag.percNoOfTasksNotStarted = CalculationPercentage(NoOfTasksNotStarted, NoOfTasks);

            Project project = db.Projects.Find(projectId);
            ViewBag.projectName = project.ProjectName;

            User loggedUser = UserHelper.GetUser();
            
            var projectSummaries = db.ProjectSummary.Where(p => p.ProjectID == projectId).Where(u => u.UserID== loggedUser.UserID).Include(p => p.User);
            ViewBag.projectSummaries = projectSummaries;

            ProjectSummary ps = new ProjectSummary
            {
                NoOfTasks = NoOfTasks,
                NoOfTasksInProgress = NoOfTasksInProgress,
                NoOfTasksNotStarted = NoOfTasksNotStarted,
                NoOfCompletedTasks = NoOfCompletedTasks,
                ProjectID = projectId
            };
            return View("ProjectSummary", ps);
        }

        //Used to load the Project Summary Report from the database to view Historic Summaries.
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectSummary projectSummary = db.ProjectSummary.Find(id);
            if (projectSummary == null)
            {
                return HttpNotFound();
            }
            ViewBag.ReportDate = projectSummary.ReportDate;
            ViewBag.ProjectId = projectSummary.ProjectID;
            int NoOfTasks = projectSummary.NoOfTasks;
            int NoOfCompletedTasks = projectSummary.NoOfCompletedTasks;
            ViewBag.percNoOfCompletedTasks = CalculationPercentage(NoOfCompletedTasks, NoOfTasks);

            int NoOfTasksInProgress = projectSummary.NoOfTasksInProgress;
            ViewBag.percNoOfTasksInProgress = CalculationPercentage(NoOfTasksInProgress, NoOfTasks);

            int NoOfTasksNotStarted = projectSummary.NoOfTasksNotStarted;
            ViewBag.percNoOfTasksNotStarted = CalculationPercentage(NoOfTasksNotStarted, NoOfTasks);

            Project project = db.Projects.Find(projectSummary.ProjectID);
            ViewBag.projectName = project.ProjectName;

            User loggedUser = UserHelper.GetUser();
            var projectSummaries = db.ProjectSummary.Where(p => p.ProjectID == projectSummary.ProjectID).Where(u => u.UserID == loggedUser.UserID).Include(p => p.User);
            ViewBag.projectSummaries = projectSummaries;

            ProjectSummary ps = new ProjectSummary
            {
                NoOfTasks = NoOfTasks,
                NoOfTasksInProgress = NoOfTasksInProgress,
                NoOfTasksNotStarted = NoOfTasksNotStarted,
                NoOfCompletedTasks = NoOfCompletedTasks,
                ProjectID = projectSummary.ProjectID
            };
            return View("ProjectSummary", ps);
        }

        //Used by Ajax to save Project Summary into database. Returns a Partial View to update the User Interface.
        [HttpPost]
        public PartialViewResult Create([Bind(Include = "NoOfTasks,NoOfTasksInProgress,NoOfTasksNotStarted,NoOfCompletedTasks,ProjectID,UserID")] ProjectSummary projectSummary)
        {
            User loggedUser = UserHelper.GetUser();
            if (ModelState.IsValid)
            {
                projectSummary.UserID = loggedUser.UserID;
                projectSummary.ReportDate = DateTime.Now;
                db.ProjectSummary.Add(projectSummary);
                db.SaveChanges();
            }

            var projectSummaries = db.ProjectSummary.Where(p => p.ProjectID == projectSummary.ProjectID).Where(u => u.UserID == loggedUser.UserID).Include(p => p.User);
            return PartialView("~/Views/ProjectSummary/_PartialProjectSummaryList.cshtml", projectSummaries);
        }

        //Used by Ajax to delete the ProjectSummary. Returns a Partial View to update the User Interface.
        public PartialViewResult Delete(int id)
        {
            User loggedUser = UserHelper.GetUser();
            ProjectSummary projectSummary = db.ProjectSummary.Find(id);
            if (projectSummary != null)
            {
                db.ProjectSummary.Remove(projectSummary);
                db.SaveChanges();
            }
            var projectSummaries = db.ProjectSummary.Where(p => p.ProjectID == projectSummary.ProjectID).Where(u => u.UserID == loggedUser.UserID).Include(p => p.User);
            return PartialView("~/Views/ProjectSummary/_PartialProjectSummaryList.cshtml", projectSummaries);
        }
        // GET: ProjectSummary
        public ActionResult Index()
        {
            return View();
        }
    }
}
