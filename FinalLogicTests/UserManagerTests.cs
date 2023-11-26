using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Moq;
using FinalDataLayer;
using FinalDataObjects;
using FinalLogic;
using System.Collections.Generic;
using RestSharp;

namespace FinalLogicTests
{
    [TestClass]
    public class UserManagerTests
    {


        [TestMethod]
        public void ValidateUserCorrectInput()
        {
            //Arrange

            UserModel testModel = new UserModel()
            {
                UserId = "test",
                PasswordHash = "test",
                Roles = new List<string>()
            };
            Mock<IUserAccess> mockUserAccess = new Mock<IUserAccess>();

            mockUserAccess.Setup(t => t.ValidateUser(It.IsAny<UserModel>())).Returns(true);
            mockUserAccess.Setup(t => t.GetUserRole(It.IsAny<UserModel>())).Returns(new List<string>());

            UserManager mockUserManager = new UserManager(mockUserAccess.Object);

            //Act
            var result = mockUserManager.LoginUser(testModel);

            //Assert
            Assert.AreEqual(result, testModel);

        }

        [TestMethod]
        public void ValidateUserIncorrectInput()
        {

            //Arrange
            UserModel testModel = new UserModel()
            {
                UserId = "test",
                PasswordHash = "test",
                Roles = new List<string>()
            };
            Mock<IUserAccess> mockUserAccess = new Mock<IUserAccess>();

            mockUserAccess.Setup(t => t.ValidateUser(It.IsAny<UserModel>())).Returns(false);

            UserManager mockUserManager = new UserManager(mockUserAccess.Object);

            //Act
            var result = mockUserManager.LoginUser(testModel);

            //Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void CreateUserValidInput()
        {
            //Arrange
            UserModel testModel = new UserModel()
            {
                UserId = "test",
                PasswordHash = "test",
                Roles = new List<string>()
            };
            Mock<IUserAccess> mockUserAccess = new Mock<IUserAccess>();

            mockUserAccess.Setup(t => t.CreateUser(It.IsAny<UserModel>())).Returns(true);

            UserManager mockUserManager = new UserManager(mockUserAccess.Object);

            //Act
            var result = mockUserManager.CreateUser(testModel);

            //Assert
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void CreateUserError()
        {

            //Arrange
            UserModel testModel = new UserModel()
            {
                UserId = "test",
                PasswordHash = "test",
                Roles = new List<string>()
            };
            Mock<IUserAccess> mockUserAccess = new Mock<IUserAccess>();

            mockUserAccess.Setup(t => t.CreateUser(It.IsAny<UserModel>())).Returns(false);

            UserManager mockUserManager = new UserManager(mockUserAccess.Object);

            //Act
            var result = mockUserManager.CreateUser(testModel);

            //Assert
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void GetUsersNullReturn()
        {
            //Arrange
            RestResponse outputs = new RestResponse();
            outputs.Content = "[\"Server\"]"; 

            List<string> expected = new List<string>();
            expected.Add("Server");

            Mock<IUserAccess> mockUserAccess = new Mock<IUserAccess>();


            mockUserAccess.Setup(t => t.GetUsers()).Returns(outputs);

            UserManager mockUserManager = new UserManager(mockUserAccess.Object);

            //Act
            var result = mockUserManager.GetUsers();

            //Assert

            //Cannot directly compare the objects or the assert fails. Instead, we will compare the stringed version of the objects.
            Assert.AreEqual(expected.ToString(), result.ToString());

        }

        [TestMethod]
        public void GetUsersOneReturn()
        {

            //Arrange
            RestResponse outputs = new RestResponse();
            outputs.Content = "[\"Server\",\"test\"]"; 

            List<string> expected = new List<string>();
            expected.Add("Server");
            expected.Add("test");

            Mock<IUserAccess> mockUserAccess = new Mock<IUserAccess>();


            mockUserAccess.Setup(t => t.GetUsers()).Returns(outputs);

            UserManager mockUserManager = new UserManager(mockUserAccess.Object);

            //Act
            var result = mockUserManager.GetUsers();

            //Assert

            //Cannot directly compare the objects or the assert fails. Instead, we will compare the stringed version of the objects.
            Assert.AreEqual(expected.ToString(), result.ToString());
        }

        [TestMethod]
        public void GetUsersMultipleReturn()
        {
            //Arrange
            RestResponse outputs = new RestResponse();
            outputs.Content = "[]"; // Null return from the JSONobject looks like this.

            List<string> expected = new List<string>();

            Mock<IUserAccess> mockUserAccess = new Mock<IUserAccess>();


            mockUserAccess.Setup(t => t.GetUsers()).Returns(outputs);

            UserManager mockUserManager = new UserManager(mockUserAccess.Object);

            //Act
            var result = mockUserManager.GetUsers();

            //Assert

            //Cannot directly compare the objects or the assert fails. Instead, we will compare the stringed version of the objects.
            Assert.AreEqual(expected.ToString(), result.ToString());
        }

        [TestMethod]
        public void AddUserRoleValidInput()
        {
            //Arrange
            bool outputs = true;

            Mock<IUserAccess> mockUserAccess = new Mock<IUserAccess>();


            mockUserAccess.Setup(t => t.AddUserRole(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(outputs);

            UserManager mockUserManager = new UserManager(mockUserAccess.Object);
            //Act
            var result = mockUserManager.AddRole("", "", "");

            //Assert
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void AddUserRoleInvalidInput()
        {
            //Arrange
            bool outputs = false;

            Mock<IUserAccess> mockUserAccess = new Mock<IUserAccess>();


            mockUserAccess.Setup(t => t.AddUserRole(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(outputs);

            UserManager mockUserManager = new UserManager(mockUserAccess.Object);
            //Act
            var result = mockUserManager.AddRole("", "", "");

            //Assert
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void RemoveUserRoleValidInput()
        {
            //Arrange
            bool outputs = true;

            Mock<IUserAccess> mockUserAccess = new Mock<IUserAccess>();


            mockUserAccess.Setup(t => t.RemoveUserRole(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(outputs);

            UserManager mockUserManager = new UserManager(mockUserAccess.Object);
            //Act
            var result = mockUserManager.RemoveRole("", "", "");

            //Assert
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void RemoveUserRoleInvalidInput()
        {
            //Arrange
            bool outputs = false;

            Mock<IUserAccess> mockUserAccess = new Mock<IUserAccess>();


            mockUserAccess.Setup(t => t.RemoveUserRole(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(outputs);

            UserManager mockUserManager = new UserManager(mockUserAccess.Object);
            //Act
            var result = mockUserManager.RemoveRole("", "", "");

            //Assert
            Assert.AreEqual(false, result);
        }
    }
}
