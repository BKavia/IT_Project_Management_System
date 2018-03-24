using Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IT_Project_Management_System.Models
{
    public class User
    {
        public virtual int UserID { get; set; }

        [Display(Name = "UserFirstName", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
              ErrorMessageResourceName = "UserFirstNameRequired")]
        [MaxLength(30)]
        public virtual string FirstName { get; set; }

        [Display(Name = "UserLastName", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
             ErrorMessageResourceName = "UserLastNameRequired")]
        [MaxLength(30)]
        public virtual string LastName { get; set; }

        [Display(Name = "UserFullName", ResourceType = typeof(Resources.Resource))]
        public string FullName {
            get
            {
                return this.FirstName + " " + this.LastName;
            }
        }

        [Display(Name = "UserEmailAddress", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
             ErrorMessageResourceName = "UserEmailAddressRequired")]
        [EmailAddress(ErrorMessage = "UserEmailAddressInvalid")]
        [MaxLength(40)]
        public virtual string Email { get; set; }

        [Display(Name = "UserPhoneNumber", ResourceType = typeof(Resources.Resource))]
        [StringLength(14, MinimumLength = 7)]
        public virtual string PhoneNumber { get; set; }

        [Display(Name = "UserUserType", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
             ErrorMessageResourceName = "UserUserTypeRequired")]
         public virtual UserType UserType { get; set; }

        [Display(Name = "UserName", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
             ErrorMessageResourceName = "UserNameRequired")]
        [MaxLength(30)]
        public virtual String UserName { get; set; }

        [Display(Name = "UserPassword", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
            ErrorMessageResourceName = "UserPasswordRequired")]
        [DataType(DataType.Password)]
        [MaxLength(30)]
        public virtual String UserPassword { get; set; }

        [Display(Name = "UserLanguage", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
            ErrorMessageResourceName = "UserLanguageRequired")]
        public virtual Language Language { get; set; }
    }

    public enum UserType
    {
        [Display(Name = "NormalUser", ResourceType = typeof(Resources.Resource))]
        NormalUser,

        [Display(Name = "Administrator", ResourceType = typeof(Resources.Resource))]
        Administrator,

        [Display(Name = "ProjectManager", ResourceType = typeof(Resources.Resource))]
        ProjectManager
    }

    public enum Language 
    {
        [Display(Name = "English", ResourceType = typeof(Resources.Resource))]
        en,

        [Display(Name = "Spanish", ResourceType = typeof(Resources.Resource))]
        es,

        [Display(Name = "French", ResourceType = typeof(Resources.Resource))]
        fr
    }
}