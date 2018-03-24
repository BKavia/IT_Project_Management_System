using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace IT_Project_Management_System.Models
{
    public class Task
    {
        public virtual int TaskID { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "TaskProjectRequired")]
        public int ProjectID { get; set; }
        [Display(Name = "TaskProject", ResourceType = typeof(Resources.Resource))]
        public virtual Project Project { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "TaskNameRequired")]
        [Display(Name = "TaskName", ResourceType = typeof(Resources.Resource))]
        [MaxLength(100)]
        public virtual string TaskName { get; set; }


        [Display(Name = "TaskDescription", ResourceType = typeof(Resources.Resource))]
        [MaxLength(200)]
        public virtual string TaskDescription { get; set; }

        [Display(Name = "TaskStartDate", ResourceType = typeof(Resources.Resource))]
        [DataType(DataType.Date)]
        public virtual DateTime? TaskStartDate { get; set; }

        [Display(Name = "TaskEndDate", ResourceType = typeof(Resources.Resource))]
        [DataType(DataType.Date)]
        public virtual DateTime? TaskEndDate { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "TaskStatusRequired")]
        [Display(Name = "TaskStatus", ResourceType = typeof(Resources.Resource))]
        public virtual TaskStatus TaskStatus { get; set; }
        
        public int? UserID { get; set; }

        [Display(Name = "TaskAssignedTo", ResourceType = typeof(Resources.Resource))]
        public virtual User User { get; set; }

        [Display(Name = "TaskKey", ResourceType = typeof(Resources.Resource))]
        public string TaskKey
        {
            get
            {
                return this.Project.ProjectKey + "-" + this.TaskID;
            }
        }

        [Display(Name = "TaskComments", ResourceType = typeof(Resources.Resource))]
        public virtual ICollection<Comment> Comments { get; set; }

        [Display(Name = "TaskAttachments", ResourceType = typeof(Resources.Resource))]
        public virtual ICollection<Document> Documents { get; set; }

    }

    public enum TaskStatus
    {
        [Display(Name = "NotStarted", ResourceType = typeof(Resources.Resource))]
         NotStarted,

        [Display(Name = "InProgress", ResourceType = typeof(Resources.Resource))]
        InProgress,

        [Display(Name = "Completed", ResourceType = typeof(Resources.Resource))]
         Completed
    }
    
}