using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IT_Project_Management_System.Models;

namespace IT_Project_Management_System.Controllers
{
    public class DocumentsController : BaseController
    {
        private SystemContext db = new SystemContext();

        // GET: Documents
        public ActionResult Index()
        {
            return View(db.Documents.ToList());
        }

        // GET: Documents/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Document document = db.Documents.Find(id);
            if (document == null)
            {
                return HttpNotFound();
            }
            return View(document);
        }

        [HttpPost]
        public ActionResult UploadFile(HttpPostedFileBase file, int TaskID)
        {
            try
            {
      
                if (file != null && file.ContentLength > 0)
                {
                    string _FileName = Path.GetFileName(file.FileName);
                    string _path = Path.Combine(Server.MapPath("~/UploadedFiles"), _FileName);
                    file.SaveAs(_path);

                    Document document = new Document();
                    document.TaskID = TaskID;
                    document.FileName = _FileName;
                    document.UploadDate = DateTime.Now;
                    db.Documents.Add(document);
                    db.SaveChanges();
                    ViewBag.Message = "File Uploaded Successfully!!";

                }
                else
                {
                    ViewBag.Message = "File upload failed!!";
                }
               
            }
            catch (Exception ex)
            {
                ViewBag.Message = "File upload failed!!";
               
            }
            
            Task task = db.Tasks.Find(TaskID);
            return PartialView("~/Views/Documents/_PartialAttachmentList.cshtml", task);
        }
        // GET: Documents/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Documents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DocumentID,TaskID")] Document document)
        {
            if (ModelState.IsValid)
            {
                db.Documents.Add(document);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(document);
        }

        // GET: Documents/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Document document = db.Documents.Find(id);
            if (document == null)
            {
                return HttpNotFound();
            }
            return View(document);
        }

        // POST: Documents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DocumentID,TaskID")] Document document)
        {
            if (ModelState.IsValid)
            {
                db.Entry(document).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(document);
        }

        // GET: Documents/Delete/5
        public ActionResult Delete(int? id, int taskID)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Document document = db.Documents.Find(id);
            if (document != null)
            {
                db.Documents.Remove(document);
                db.SaveChanges();
            }
            Task task = db.Tasks.Find(taskID);
            return PartialView("~/Views/Documents/_PartialAttachmentList.cshtml", task);
        
        }

        // POST: Documents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Document document = db.Documents.Find(id);
            db.Documents.Remove(document);
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
