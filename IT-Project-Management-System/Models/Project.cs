using PagedList;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IT_Project_Management_System.Models
{
    public class Project
    {
        public virtual int ProjectID { get; set; }

        
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "ProjectPMRequired")]
        [ForeignKey("User")]
        public int? UserID { get; set; }
        [Display(Name = "ProjectPM", ResourceType = typeof(Resources.Resource))]
        public virtual User User { get; set; }


        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "ProjectKeyRequired")]
        [Display(Name = "ProjectKey", ResourceType = typeof(Resources.Resource))]
        [MaxLength(3)]
        public virtual string ProjectKey { get; set; }

      
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "ProjectNameRequired")]
        [Display(Name = "ProjectName", ResourceType = typeof(Resources.Resource))]
        [StringLength(100)]
        public virtual string ProjectName { get; set; }


        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "ProjectDescriptionRequired")]
        [Display(Name = "ProjectDescription", ResourceType = typeof(Resources.Resource))]
        [StringLength(200)]
        public virtual string ProjectDescription { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "ProjectStartDateRequired")]
        [Display(Name = "ProjectStartDate", ResourceType = typeof(Resources.Resource))]
        [DataType(DataType.Date)]
        public virtual DateTime ProjectStartDate { get; set; }


        [Display(Name = "ProjectEndDate", ResourceType = typeof(Resources.Resource))]
        [DataType(DataType.Date)]
        public virtual DateTime? ProjectEndDate { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "ProjectStatusRequired")]
        [Display(Name = "ProjectStatus", ResourceType = typeof(Resources.Resource))]
        public virtual ProjectStatus ProjectStatus { get; set; }

    }

    public enum ProjectStatus
    {
        [Display(Name = "NotStarted", ResourceType = typeof(Resources.Resource))]
        NotStarted,

        [Display(Name = "InProgress", ResourceType = typeof(Resources.Resource))]
        InProgress,

        [Display(Name = "Completed", ResourceType = typeof(Resources.Resource))]
        Completed

 

       
    
    }
}