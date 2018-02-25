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

        [Required(ErrorMessage = "Project is required")]
         public  int ProjectID { get; set; }
        [Display(Name = "Project")]
        public virtual Project Project { get; set; }
       

        [Required(ErrorMessage = "Task Name is required")]
        [Display(Name = "Task Name")]
        [MaxLength(100)]
        public virtual string TaskName { get; set; }

        [Display(Name = "Task Description")]
        [MaxLength(200)]
        public virtual string TaskDescription { get; set; }
            
        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public virtual DateTime? TaskStartDate { get; set; }
               
        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public virtual DateTime? TaskEndDate { get; set; }

        [Required(ErrorMessage = "Task Status is required")]
        [Display(Name = "Task Status")]
        public virtual TaskStatus TaskStatus { get; set; }

        [Display(Name = "User")]
        public int? UserID { get; set; }
        public virtual User User { get; set; }


       
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