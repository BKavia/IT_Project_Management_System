using System;
using IT_Project_Management_System.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace IT_Project_Management_System.Tests
{
    [TestClass]
    public class TestUser
    {
        [TestMethod]
        public void InstanceOK()
        {
            User user = new User();
            Assert.IsNotNull(user);
        }
        [TestMethod]
        public void Testuseroperties()
        {
            User user = new User();
            int id = 1;
            user.UserID = id;
            Assert.AreEqual(user.UserID, id);

            string firstName = "Bhumi";
            user.FirstName = firstName;
            Assert.AreEqual(user.FirstName, firstName);

            string lastName = "Kavia";
            user.LastName = lastName;
            Assert.AreEqual(user.LastName, lastName);

            string email = "bKavia@pms.co.uk";
            user.Email = email;
            Assert.AreEqual(user.Email, email);

            string phoneNumber = "07834986107";
            user.PhoneNumber = phoneNumber;
            Assert.AreEqual(user.PhoneNumber, phoneNumber);

            string userName = "bhumi";
            user.UserName = userName;
            Assert.AreEqual(user.UserName, userName);

            string userPassword = "bhumi";
            user.UserPassword = userPassword;
            Assert.AreEqual(user.UserPassword, userPassword);


            Language language = new Language();
            user.Language = language;
            Assert.AreEqual(user.Language, language);

            UserType ut = new UserType();
            user.UserType = ut;
            Assert.AreEqual(user.UserType, ut);

            Assert.AreEqual(user.FullName, "Bhumi Kavia");

        }
    }
}
