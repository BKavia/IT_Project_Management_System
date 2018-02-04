using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IT_Project_Management_System.Models
{
    public class Document
    {
        public virtual int DocumentID { get; set; }
        public virtual string DocumentName { get; set; }
        public virtual string DocumentDescription { get; set; }
        public virtual DateTime DocumentDate { get; set; }
        public virtual string DocumentURL { get; set; }
        public virtual int TaskID { get; set; }
    }
}