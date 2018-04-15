using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IT_Project_Management_System.Models
{
    //Model used for Project information
    public class Project
    {
        //Primary Key for the Project
        public virtual int ProjectID { get; set; }

        // User identifier for Project Manager
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "ProjectPMRequired")]
        [ForeignKey("User")]
        public int? UserID { get; set; }
        [Display(Name = "ProjectPM", ResourceType = typeof(Resources.Resource))]
        public virtual User User { get; set; }

        // Project key
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "ProjectKeyRequired")]
        [Display(Name = "ProjectKey", ResourceType = typeof(Resources.Resource))]
        [MaxLength(3)]
        public virtual string ProjectKey { get; set; }

        //Name of the project
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "ProjectNameRequired")]
        [Display(Name = "ProjectName", ResourceType = typeof(Resources.Resource))]
        [MaxLength(50)]
        public virtual string ProjectName { get; set; }

        //Description of the project
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "ProjectDescriptionRequired")]
        [Display(Name = "ProjectDescription", ResourceType = typeof(Resources.Resource))]
        [MaxLength(200)]
        public virtual string ProjectDescription { get; set; }
        
        //The date the project started
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "ProjectStartDateRequired")]
        [Display(Name = "ProjectStartDate", ResourceType = typeof(Resources.Resource))]
        [DataType(DataType.Date)]
        public virtual DateTime ProjectStartDate { get; set; }

        //The date the project is to end
        [Display(Name = "ProjectEndDate", ResourceType = typeof(Resources.Resource))]
        [DataType(DataType.Date)]
        public virtual DateTime? ProjectEndDate { get; set; }

        //The status of the project
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "ProjectStatusRequired")]
        [Display(Name = "ProjectStatus", ResourceType = typeof(Resources.Resource))]
        public virtual ProjectStatus ProjectStatus { get; set; }

    }

    //Enumeration for the Project Status
    public enum ProjectStatus
    {
        //Signifies that the project has not started
        [Display(Name = "NotStarted", ResourceType = typeof(Resources.Resource))]
        NotStarted,

        //Signifies that the project is in progress
        [Display(Name = "InProgress", ResourceType = typeof(Resources.Resource))]
        InProgress,

        //Signifies that the project has completed
        [Display(Name = "Completed", ResourceType = typeof(Resources.Resource))]
        Completed
    
    }

   
}