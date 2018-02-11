using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IT_Project_Management_System.Models
{
    public class User
    {
        public virtual int UserID { get; set; }

        [Required(ErrorMessage = "FirstName is required")]
        [Display(Name = "First Name")]
        [MaxLength(30)]
        public virtual string FirstName { get; set; }

        [Required(ErrorMessage = "LastName is required")]
        [Display(Name = "Last Name")]
        [MaxLength(30)]
        public virtual string LastName { get; set; }

        [Display(Name = "Email address")]
        [Required(ErrorMessage = "The Email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public virtual string Email { get; set; }


        [Display(Name = "Phone Number")]
        [StringLength(14, MinimumLength = 7)]
        public virtual string PhoneNumber { get; set; }

        [Display(Name = "User type")]
        [Required(ErrorMessage = "The User type is required")]
        public virtual UserType UserType { get; set; }

        [Display(Name = "Language")]
        [Required(ErrorMessage = "The Language is required")]
        public virtual Language Language { get; set; }
    }

    public enum UserType
    {
        [Display(Name = "Administrator")]
        Administrator,

        [Display(Name = "Project Manager")]
        ProjectManager,

        [Display(Name = "Normal User")]
        NormalUser
    }

    public enum Language
    {
        [Display(Name = "English")]
        en,

        [Display(Name = "Spanish")]
        es,

        [Display(Name = "French")]
        fr
    }
}