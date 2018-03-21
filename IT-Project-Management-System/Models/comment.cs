using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IT_Project_Management_System.Models
{
    public class Comment
    {
        public virtual int CommentID { get; set; }

        public  int TaskID { get; set; }
        [Display(Name = "CommentTask", ResourceType = typeof(Resources.Resource))]
        public virtual Task Task { get; set; }

        public int UserID { get; set; }
        [Display(Name = "CommentBy", ResourceType = typeof(Resources.Resource))]
        public virtual User User { get; set; }


        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "CommentRequired")]
        [Display(Name = "Comment", ResourceType = typeof(Resources.Resource))]
        [MaxLength(300)]
        public virtual string CommentText { get; set; }

        [Display(Name = "CommentDate", ResourceType = typeof(Resources.Resource))]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public virtual DateTime CommentDate { get; set; }
    }
}