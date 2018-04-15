using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using IT_Project_Management_System.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IT_Project_Management_System.Tests
{
    [TestClass]
    public class TestProject
    {
        [TestMethod]
        public void InstanceOK()
        {
            Project pr = new Project();
            Assert.IsNotNull(pr);
        }

        [TestMethod]
        public void TestProperties()
        {
            Project pr = new Project();
            int id = 1;
            pr.ProjectID = id;
            Assert.AreEqual(pr.ProjectID, id);

            int userID = 2;
            pr.UserID = userID;
            Assert.AreEqual(pr.UserID, userID);

            User user = new User();
            pr.User = user;
            Assert.AreEqual(pr.User, user);

            string projectKey = "JUP";
            pr.ProjectKey = projectKey;
            Assert.AreEqual(pr.ProjectKey, projectKey);

            string projectName = "Jupiter";
            pr.ProjectName = projectName;
            Assert.AreEqual(pr.ProjectName, projectName);

            string projectDescription = "Jupiter";
            pr.ProjectDescription = projectDescription;
            Assert.AreEqual(pr.ProjectDescription, projectDescription);

            DateTime projectStartDate = DateTime.Now;
            pr.ProjectStartDate = projectStartDate;
            Assert.AreEqual(pr.ProjectStartDate, projectStartDate);

            DateTime projectEndDate = DateTime.Now;
            pr.ProjectEndDate = projectEndDate;
            Assert.AreEqual(pr.ProjectEndDate, projectEndDate);

            ProjectStatus ps = new ProjectStatus();
            pr.ProjectStatus = ps;
            Assert.AreEqual(pr.ProjectStatus, ps);
                        
        }
        
    }
}

