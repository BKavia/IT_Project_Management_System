using System;
using IT_Project_Management_System.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IT_Project_Management_System.Tests
{
    [TestClass]
    public class TestUsersController
    {
        [TestMethod]
        public void InstanceOK()
        {
            UsersController controller = new UsersController();
            Assert.IsNotNull(controller);
        }

        
    }
}

