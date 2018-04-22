using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using IT_Project_Management_System.Models;
using PagedList;

namespace IT_Project_Management_System.Controllers
{
    //Controller for Projects.
    [Authorize]
    public class ProjectsController : BaseController
    {
        private SystemContext db = new SystemContext();

        // GET: Gets the Project and sort them depending on the passed criteria
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

        //Load the Projects for the Details View
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

        //Loads the Objects needed to be displayed for the Create page
        // GET: Projects/Create
        public ActionResult Create()
        {
            ViewBag.UserID = new SelectList(db.Users.Where(p => p.UserType == UserType.ProjectManager), "UserID", "FullName");
            return View();
        }

        //Method called when the Create Form is submitted. This saves the Project in the database
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

        //Load the Project for the Edit View and prepare the needed information.Gives BadRequest if the id is not specified
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

        //Method called when the Project information edited is to be saved to the database.
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

        //Loads the Project for the Delete Confirmation page.
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

        //Removes the Project from the database once delete has been confirmed.
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

        //Release the database and resources
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
