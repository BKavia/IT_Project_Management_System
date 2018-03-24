using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IT_Project_Management_System.Models
{
    public class Document
    {
        public virtual int DocumentID { get; set; }

     
        [Display(Name = "DocumentFile", ResourceType = typeof(Resources.Resource))]
        [NotMapped]
        public  HttpPostedFileBase File { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "FileNameRequired")]
        public string FileName { get; set; }

      
        public  int TaskID { get; set; }

        [Display(Name = "DocumentTask", ResourceType = typeof(Resources.Resource))]
        public virtual Task Task { get; set; }
    }
}