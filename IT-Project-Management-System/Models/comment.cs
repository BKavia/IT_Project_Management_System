using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IT_Project_Management_System.Models
{
    public class Comment
    {
        public virtual int CommentID { get; set; }
        public virtual string CommentName { get; set; }
        public virtual string CommentDescription { get; set; }
        public virtual DateTime CommentDate { get; set; }
    }
}