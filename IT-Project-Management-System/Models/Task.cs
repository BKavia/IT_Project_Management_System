using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IT_Project_Management_System.Models
{
    public class Task
    {
        public virtual int TaskID { get; set; }
        public virtual int ProjectID { get; set; }
        public virtual string TaskName { get; set; }
        public virtual string TaskDescription { get; set; }
        public virtual System.DateTime TaskStartDate { get; set; }
        public virtual System.DateTime TaskEndDate { get; set; }
        public virtual string TaskStatus { get; set; }
        public virtual int AssignedUserID { get; set; }
    }
}