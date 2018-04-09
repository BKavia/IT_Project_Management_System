using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IT_Project_Management_System.Models
{
    //Model used for Project Summart(report) information
    public class ProjectSummary
    {
        //Primary Key for the Project Summary
        public virtual int ProjectSummaryID { get; set; }

        //Total number of tasks in a project
        [Display(Name = "NoOfTasks", ResourceType = typeof(Resources.Resource))]
        public virtual int NoOfTasks { get; set; }

        //Number of tasks completed
        [Display(Name = "NoOfCompletedTasks", ResourceType = typeof(Resources.Resource))]
        public virtual int NoOfCompletedTasks { get; set; }

        //Number of tasks that are in progress
        [Display(Name = "NoOfTasksInProgress", ResourceType = typeof(Resources.Resource))]
        public virtual int NoOfTasksInProgress { get; set; }

        //Number of tasks that have not been started
        [Display(Name = "NoOfTasksNotStarted", ResourceType = typeof(Resources.Resource))]
        public virtual int NoOfTasksNotStarted { get; set; }

        //The identifier of the project
        public int ProjectID { get; set; }
        [Display(Name = "ReportProject", ResourceType = typeof(Resources.Resource))]
        public virtual Project Project { get; set; }

        //The identifier for the User
        public int UserID { get; set; }
        [Display(Name = "UserID", ResourceType = typeof(Resources.Resource))]
        public virtual User User { get; set; }

        //The Date the report was created
        [Display(Name = "ReportDate", ResourceType = typeof(Resources.Resource))]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public virtual DateTime ReportDate { get; set; }
    }
}