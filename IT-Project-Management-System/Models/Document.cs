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
        [Required]
        [Display(Name = "File")]
        [NotMapped]
        public  HttpPostedFileBase File { get; set; }

        public  int TaskID { get; set; }
        [Display(Name = "Task")]
        public virtual Task Task { get; set; }
    }
}