using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Moq;
using FinalDataLayer;
using FinalDataObjects;
using FinalLogic;
using System.Collections.Generic;

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

    }
}
