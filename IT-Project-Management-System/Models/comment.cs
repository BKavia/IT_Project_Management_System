using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IT_Project_Management_System.Models
{
    //Model used for Comments 
    public class Comment
    {
        //Primary Key for Comment
        public virtual int CommentID { get; set; }

        // Task identifier
        public  int TaskID { get; set; }
        [Display(Name = "CommentTask", ResourceType = typeof(Resources.Resource))]
        public virtual Task Task { get; set; }

        //User identifier
        public int UserID { get; set; }
        [Display(Name = "CommentBy", ResourceType = typeof(Resources.Resource))]
        public virtual User User { get; set; }

        //Comment text
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "CommentRequired")]
        [Display(Name = "Comment", ResourceType = typeof(Resources.Resource))]
        [MaxLength(300)]
        public virtual string CommentText { get; set; }

        //Date of which the comment was made
        [Display(Name = "CommentDate", ResourceType = typeof(Resources.Resource))]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public virtual DateTime CommentDate { get; set; }
    }
}