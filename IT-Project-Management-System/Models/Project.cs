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

        [Required(ErrorMessage = "Project Manager is required")]
        [ForeignKey("User")]
        public int? UserID { get; set; }
        [Display(Name = "Project Manager")]
        public virtual User User { get; set; }

        [Required(ErrorMessage = "Key is required")]
        [Display(Name = "Key")]
        [StringLength(3)]
        public virtual string ProjectKey { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [Display(Name = "Name")]
        [StringLength(100)]
        public virtual string ProjectName { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [Display(Name = "Description")]
        [StringLength(200)]
        public virtual string ProjectDescription { get; set; }

        [Required(ErrorMessage = "Start Date is required")]
        [Display(Name = "Start Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public virtual DateTime ProjectStartDate { get; set; }
        

        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public virtual DateTime? ProjectEndDate { get; set; }

        [Required(ErrorMessage = "Project Status is required")]
        [Display(Name = "Project Status")]
        public virtual ProjectStatus ProjectStatus { get; set; }

    }

    public enum ProjectStatus
    {
        [Display(Name = "Not Started")]
        NotStarted,

        [Display(Name = "In Progress")]
        InProgress,

        [Display(Name = "Completed")]
        Completed
    }
}