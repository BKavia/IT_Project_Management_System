using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IT_Project_Management_System.Models
{
    public class Task
    {
        public virtual int TaskID { get; set; }

        [Required(ErrorMessage = "Project is required")]
        [Display(Name = "Project")]
        public virtual int ProjectID { get; set; }

        [Required(ErrorMessage = "Task Name is required")]
        [Display(Name = "Task Name")]
        [MaxLength(100)]
        public virtual string TaskName { get; set; }

        [Required(ErrorMessage = "Task Description is required")]
        [Display(Name = "Task Description")]
        [MaxLength(200)]
        public virtual string TaskDescription { get; set; }

        [Required(ErrorMessage = " Task Start Date is required")]
        [Display(Name = "Start Date")]
        public virtual DateTime TaskStartDate { get; set; }

       
        [Display(Name = "End Date")]
        public virtual DateTime TaskEndDate { get; set; }

        [Required(ErrorMessage = "Task Status is required")]
        [Display(Name = "Task Status")]
        public virtual TaskStatus TaskStatus { get; set; }

        [Display(Name = "User")]
        public virtual int AssignedUserID { get; set; }
    }

    public enum TaskStatus
    {
        [Display(Name = "Not Started")]
        NotStarted,

        [Display(Name = "In Progress")]
        InProgress,

        [Display(Name = "Completed")]
        Completed
    }
}