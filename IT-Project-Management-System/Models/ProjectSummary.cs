using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IT_Project_Management_System.Models
{
    public class ProjectSummary
    {
        public virtual int ProjectSummaryID { get; set; }

        [Display(Name = "NoOfTasks", ResourceType = typeof(Resources.Resource))]
        public virtual int NoOfTasks { get; set; }

        [Display(Name = "NoOfCompletedTasks", ResourceType = typeof(Resources.Resource))]
        public virtual int NoOfCompletedTasks { get; set; }

        [Display(Name = "NoOfTasksInProgress", ResourceType = typeof(Resources.Resource))]
        public virtual int NoOfTasksInProgress { get; set; }

        [Display(Name = "NoOfTasksNotStarted", ResourceType = typeof(Resources.Resource))]
        public virtual int NoOfTasksNotStarted { get; set; }

        public int ProjectID { get; set; }
        [Display(Name = "ReportProject", ResourceType = typeof(Resources.Resource))]
        public virtual Project Project { get; set; }

        public int UserID { get; set; }
        [Display(Name = "UserID", ResourceType = typeof(Resources.Resource))]
        public virtual User User { get; set; }

        [Display(Name = "ReportDate", ResourceType = typeof(Resources.Resource))]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public virtual DateTime ReportDate { get; set; }
    }
}