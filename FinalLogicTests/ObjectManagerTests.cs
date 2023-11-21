using FinalDataLayer;
using FinalDataObjects;
using FinalLogic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalLogicTests
{
    [TestClass]
    public class ObjectManagerTests
    {
        [TestMethod]
        public void GetObjectsValidInput()
        {
            //Arrange
            List<ObjectModel> objects = new List<ObjectModel>();
            objects.Add(new ObjectModel());
            objects.Add(new ObjectModel());


            Mock<IObjectAccess> mockObjectAccess = new Mock<IObjectAccess>();

            mockObjectAccess.Setup(t => t.GetObjectList()).Returns(objects);

            ObjectManager mockObjectManager = new ObjectManager(mockObjectAccess.Object);

            //Act
            var result = mockObjectManager.GetObjects();

            //Assert
            Assert.AreEqual(2, result.Count);
        }

        [TestMethod]
        public void GetObjectsEmptyList()
        {
            //Arrange
            List<ObjectModel> objects = new List<ObjectModel>();

            Mock<IObjectAccess> mockObjectAccess = new Mock<IObjectAccess>();



            mockObjectAccess.Setup(t => t.GetObjectList()).Returns(objects);

            ObjectManager mockObjectManager = new ObjectManager(mockObjectAccess.Object);

            //Act
            var result = mockObjectManager.GetObjects();

            //Assert
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void GetObjectByIdValidInput()
        {
            //Arrange
            FullObjectModel fullObjectModel = new FullObjectModel()
            {
                ObjectID = "test"
            };

            Mock<IObjectAccess> mockObjectAccess = new Mock<IObjectAccess>();

            mockObjectAccess.Setup(t => t.GetObjectById(It.IsAny<string>())).Returns(fullObjectModel);

            ObjectManager mockObjectManager = new ObjectManager(mockObjectAccess.Object);

            //Act 
            var result = mockObjectManager.GetObjectById("test");

            //Assert
            Assert.AreEqual(fullObjectModel, result);
        }

        [TestMethod]
        public void GetObjectByIdInvalidInput()
        {
            FullObjectModel fullObjectModel = new FullObjectModel()
            {
                ObjectID = "test"
            };

            FullObjectModel returnModel = new FullObjectModel();

            Mock<IObjectAccess> mockObjectAccess = new Mock<IObjectAccess>();

            mockObjectAccess.Setup(t => t.GetObjectById(It.IsAny<string>())).Returns(returnModel);

            ObjectManager mockObjectManager = new ObjectManager(mockObjectAccess.Object);

            //Act 
            var result = mockObjectManager.GetObjectById("nottest");

            //Assert
            Assert.AreNotEqual(fullObjectModel, returnModel);
        }

        [TestMethod]
        public void EditObjectValidInput()
        {
            FullObjectModel fullObjectModel = new FullObjectModel();


            Mock<IObjectAccess> mockObjectAccess = new Mock<IObjectAccess>();

            mockObjectAccess.Setup(t => t.EditObjectById(It.IsAny<FullObjectModel>(), It.IsAny<string>(), It.IsAny<string>())).Returns(true);

            ObjectManager mockObjectManager = new ObjectManager(mockObjectAccess.Object);

            //Act 
            var result = mockObjectManager.EditObject(fullObjectModel, "testuser", "testaction");

            //Assert
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void EditObjectInvalidInput()
        {
            FullObjectModel fullObjectModel = new FullObjectModel();


            Mock<IObjectAccess> mockObjectAccess = new Mock<IObjectAccess>();

            mockObjectAccess.Setup(t => t.EditObjectById(It.IsAny<FullObjectModel>(), It.IsAny<string>(), It.IsAny<string>())).Returns(false);

            ObjectManager mockObjectManager = new ObjectManager(mockObjectAccess.Object);

            //Act 
            var result = mockObjectManager.EditObject(fullObjectModel, "testuser","testaction");

            //Assert
            Assert.AreEqual(false, result);
        }
        /*
         * 
         *             //Arrange

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
         * 
         * 
         * 
         * 
         * 
         * 
         */


    }
}
