using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace IT_Project_Management_System.Models
{
    //Model used for Task
    public class Task
    {
        //Primary Key for the Task
        public virtual int TaskID { get; set; }
        
        //The identified for the project the task belongs to.
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "TaskProjectRequired")]
        public int ProjectID { get; set; }
        [Display(Name = "TaskProject", ResourceType = typeof(Resources.Resource))]
        public virtual Project Project { get; set; }

        //The task name
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "TaskNameRequired")]
        [Display(Name = "TaskName", ResourceType = typeof(Resources.Resource))]
        [MaxLength(100)]
        public virtual string TaskName { get; set; }

        //The description
        [Display(Name = "TaskDescription", ResourceType = typeof(Resources.Resource))]
        [MaxLength(200)]
        public virtual string TaskDescription { get; set; }

        //The Date the task was started
        [Display(Name = "TaskStartDate", ResourceType = typeof(Resources.Resource))]
        [DataType(DataType.Date)]
        public virtual DateTime? TaskStartDate { get; set; }

        //The Date the task ended
        [Display(Name = "TaskEndDate", ResourceType = typeof(Resources.Resource))]
        [DataType(DataType.Date)]
        public virtual DateTime? TaskEndDate { get; set; }

        //The status of the task(NotStarted,InProgress or Completed)
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "TaskStatusRequired")]
        [Display(Name = "TaskStatus", ResourceType = typeof(Resources.Resource))]
        public virtual TaskStatus TaskStatus { get; set; }
        
        //The identifier for the user the taskk is assigned to.
        public int? UserID { get; set; }
        [Display(Name = "TaskAssignedTo", ResourceType = typeof(Resources.Resource))]
        public virtual User User { get; set; }

        //The unique task key. Joins the project key with the task identifier.
        [Display(Name = "TaskKey", ResourceType = typeof(Resources.Resource))]
        public string TaskKey
        {
            get
            {
                return this.Project.ProjectKey + "-" + this.TaskID;
            }
        }

        //A Collection to Comments
        [Display(Name = "TaskComments", ResourceType = typeof(Resources.Resource))]
        public virtual ICollection<Comment> Comments { get; set; }

        //A Collection of documents uploaded against the task
        [Display(Name = "TaskAttachments", ResourceType = typeof(Resources.Resource))]
        public virtual ICollection<Document> Documents { get; set; }

    }

    public enum TaskStatus
    {
        //Signifies that the task has not started
        [Display(Name = "Not Started", ResourceType = typeof(Resources.Resource))]
         NotStarted,

        //Signifies that the task is in progress
        [Display(Name = "In Progress", ResourceType = typeof(Resources.Resource))]
        InProgress,

        //Signifies that the task has completed
        [Display(Name = "Completed", ResourceType = typeof(Resources.Resource))]
        Completed

    }
    
}