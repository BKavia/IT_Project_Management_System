using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace IT_Project_Management_System.Models
{
    public class SystemContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public SystemContext() : base("name=IT_Project_Management_SystemContext")
        {
        }

        public System.Data.Entity.DbSet<IT_Project_Management_System.Models.User> Users { get; set; }

        public System.Data.Entity.DbSet<IT_Project_Management_System.Models.Task> Tasks { get; set; }

        public System.Data.Entity.DbSet<IT_Project_Management_System.Models.Project> Projects { get; set; }

        public System.Data.Entity.DbSet<IT_Project_Management_System.Models.Comment> Comments { get; set; }

        public System.Data.Entity.DbSet<IT_Project_Management_System.Models.Document> Documents { get; set; }

        public System.Data.Entity.DbSet<IT_Project_Management_System.Models.ProjectSummary> ProjectSummary { get; set; }

    }
}
