using System;
using IT_Project_Management_System.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IT_taskoject_Management_System.Tests
{
    [TestClass]
    public class TestTask
    {
        [TestMethod]
        public void InstanceOK()
        {
            Task task = new Task();
            Assert.IsNotNull(task);
        }
        [TestMethod]
        public void Testtaskoperties()
        {
            Task task = new Task();
            int id = 1;
            task.TaskID = id;
            Assert.AreEqual(task.TaskID, id);

            int projectID = 2;
            task.ProjectID = projectID;
            Assert.AreEqual(task.ProjectID, projectID);

            Project project = new Project();
            task.Project = project;
            Assert.AreEqual(task.Project, project);

            string taskName = "Update database table";
            task.TaskName = taskName;
            Assert.AreEqual(task.TaskName, taskName);

            string taskDescription = "All table needs to be updated";
            task.TaskDescription = taskDescription;
            Assert.AreEqual(task.TaskDescription, taskDescription);

            DateTime taskStartDate = DateTime.Now;
            task.TaskStartDate = taskStartDate;
            Assert.AreEqual(task.TaskStartDate, taskStartDate);

            DateTime taskEndDate = DateTime.Now;
            task.TaskEndDate = taskEndDate;
            Assert.AreEqual(task.TaskEndDate, taskEndDate);

            TaskStatus ts = new TaskStatus();
            task.TaskStatus = ts;
            Assert.AreEqual(task.TaskStatus, ts);

            int userID = 2;
            task.UserID = userID;
            Assert.AreEqual(task.UserID, userID);

            User user = new User();
            task.User = user;
            Assert.AreEqual(task.User, user);

            Project pr = new Project();
            pr.ProjectKey = "ABC";
            task.Project = pr;
            task.TaskID = 1;
            Assert.AreEqual(task.TaskKey, "ABC-1");

        }
    }
}
