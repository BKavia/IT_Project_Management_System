using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IT_Project_Management_System.Models
{
    //Model used for Documents(Attachments)
    public class Document
    {
        //Primary Key for Document
        public virtual int DocumentID { get; set; }

        //Field for the file content. Not mapped to database column.
        [Display(Name = "DocumentFile", ResourceType = typeof(Resources.Resource))]
        [NotMapped]
        public  HttpPostedFileBase File { get; set; }

        //Name of the document file.
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "FileNameRequired")]
        public string FileName { get; set; }

        // Task identifier
        public int TaskID { get; set; }
        [Display(Name = "DocumentTask", ResourceType = typeof(Resources.Resource))]
        public virtual Task Task { get; set; }

        //Date the document is uploaded.
        public DateTime UploadDate { get; set; }
    }
}