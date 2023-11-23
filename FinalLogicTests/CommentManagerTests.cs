using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using FinalDataObjects;
using FinalLogic;
using RestSharp;
using FinalDataLayer;

namespace FinalLogicTests
{
    [TestClass]
    public class CommentManagerTests
    {
        [TestMethod]
        public void DeactivateCommentValidInput()
        {
            //Arrange
            RestResponse outputs = new RestResponse();
            outputs.Content = "true";


            Mock<ICommentAccess> mockCommentAccess = new Mock<ICommentAccess>();

            mockCommentAccess.Setup(t => t.DeactivateComment(It.IsAny<int>())).Returns(outputs);

            CommentManager mockCommentManager = new CommentManager(mockCommentAccess.Object);

            //Act
            var result = mockCommentManager.DeactivateComment(1);

            //Assert
            Assert.AreEqual(true, result);
            
        }

        [TestMethod]
        public void DeactivateCommentInvalidInput()
        {
            //Arrange
            RestResponse outputs = new RestResponse();
            outputs.Content = "false";


            Mock<ICommentAccess> mockCommentAccess = new Mock<ICommentAccess>();

            mockCommentAccess.Setup(t => t.DeactivateComment(It.IsAny<int>())).Returns(outputs);

            CommentManager mockCommentManager = new CommentManager(mockCommentAccess.Object);

            //Act
            var result = mockCommentManager.DeactivateComment(1);

            //Assert
            Assert.AreEqual(false, result);

        }

        [TestMethod]
        public void EditCommentValidInput()
        {
            //Arrange
            RestResponse outputs = new RestResponse();
            outputs.Content = "true";

            CommentModel model = new CommentModel();
            string inputString = "";

            Mock<ICommentAccess> mockCommentAccess = new Mock<ICommentAccess>();

            mockCommentAccess.Setup(t => t.EditComment(It.IsAny<CommentModel>(), It.IsAny<string>())).Returns(outputs);

            CommentManager mockCommentManager = new CommentManager(mockCommentAccess.Object);

            //Act
            var result = mockCommentManager.EditComment(model, inputString);

            //Assert
            Assert.AreEqual(true, result);

        }

        [TestMethod]
        public void EditCommentInvalidInput()
        {
            //Arrange
            RestResponse outputs = new RestResponse();
            outputs.Content = "false";

            CommentModel model = new CommentModel();
            string inputString = "";

            Mock<ICommentAccess> mockCommentAccess = new Mock<ICommentAccess>();

            mockCommentAccess.Setup(t => t.EditComment(It.IsAny<CommentModel>(), It.IsAny<string>())).Returns(outputs);

            CommentManager mockCommentManager = new CommentManager(mockCommentAccess.Object);

            //Act
            var result = mockCommentManager.EditComment(model, inputString);

            //Assert
            Assert.AreEqual(false, result);

        }

        [TestMethod]
        public void GetCommentsByObjectIdNullReturn()
        {
            //Arrange
            RestResponse outputs = new RestResponse();
            outputs.Content = "[]"; // Null return from the JSONobject looks like this.

            string inputString = "";

            List<CommentModel> expected = new List<CommentModel>();
            
            Mock<ICommentAccess> mockCommentAccess = new Mock<ICommentAccess>();

            mockCommentAccess.Setup(t => t.GetCommentsByObjectId(It.IsAny<string>())).Returns(outputs);

            CommentManager mockCommentManager = new CommentManager(mockCommentAccess.Object);

            //Act
            var result = mockCommentManager.GetCommentsByObjectId(inputString);

            //Assert

            //Cannot directly compare the objects or the assert fails. Instead, we will compare the stringed version of the objects.
            Assert.AreEqual(expected.ToString(), result.ToString());
        }


        [TestMethod]
        public void GetCommentsByObjectIdOneReturn()
        {
            //Arrange
            RestResponse outputs = new RestResponse();
            outputs.Content = "[{\"CommentID\":1,\"UserId\":\"Server\",\"ObjectId\":\"TeSt-102-12.3\",\"Description\":\"string\",\"TimePosted\":\"2023-11-23T16:39:49.787\",\"Active\":true}]";

            string inputString = "";

            List<CommentModel> expected = new List<CommentModel>();
            expected.Add(new CommentModel()
            {
                CommentID = 1,
                UserId = "Server",
                ObjectId = "TeSt-102-12.3",
                Description = "string",
                TimePosted = Convert.ToDateTime("2023-11-23T16:39:49.787"),
                Active = true

            }); 

            Mock<ICommentAccess> mockCommentAccess = new Mock<ICommentAccess>();

            mockCommentAccess.Setup(t => t.GetCommentsByObjectId(It.IsAny<string>())).Returns(outputs);

            CommentManager mockCommentManager = new CommentManager(mockCommentAccess.Object);

            //Act
            var result = mockCommentManager.GetCommentsByObjectId(inputString);

            //Assert

            //Cannot directly compare the objects or the assert fails. Instead, we will compare the stringed version of the objects.
            Assert.AreEqual(expected.ToString(), result.ToString());
        }

        [TestMethod]
        public void GetCommentsByObjectIdMultipleReturn()
        {
            //Arrange
            RestResponse outputs = new RestResponse();
            outputs.Content = "[{\"commentID\":1,\"userId\":\"Server\",\"objectId\":\"TeSt-102-12.3\",\"description\":\"string\",\"timePosted\":\"2023-11-23T16:39:49.787\",\"active\":true},{\"commentID\":2,\"userId\":\"Server\",\"objectId\":\"TeSt-102-12.3\",\"description\":\"string\",\"timePosted\":\"2023-11-23T17:27:17.607\",\"active\":true}]";

            string inputString = "";


            List<CommentModel> expected = new List<CommentModel>();
            expected.Add(new CommentModel()
            {
                CommentID = 1,
                UserId = "Server",
                ObjectId = "TeSt-102-12.3",
                Description = "string",
                TimePosted = Convert.ToDateTime("2023-11-23T16:39:49.787"),
                Active = true

            });
            expected.Add(new CommentModel()
            {
                CommentID = 2,
                UserId = "Server",
                ObjectId = "TeSt-102-12.3",
                Description = "string",
                TimePosted = Convert.ToDateTime("2023-11-23T17:27:17.607"),
                Active = true
            });

            Mock<ICommentAccess> mockCommentAccess = new Mock<ICommentAccess>();

            mockCommentAccess.Setup(t => t.GetCommentsByObjectId(It.IsAny<string>())).Returns(outputs);

            CommentManager mockCommentManager = new CommentManager(mockCommentAccess.Object);

            //Act
            var result = mockCommentManager.GetCommentsByObjectId(inputString);

            //Assert

            //Cannot directly compare the objects or the assert fails. Instead, we will compare the stringed version of the objects.
            Assert.AreEqual(expected.ToString(), result.ToString());
        }

        [TestMethod]
        public void PostCommentValidInput()
        {
            //Arrange
            RestResponse outputs = new RestResponse();
            outputs.Content = "true";

            CommentModel model = new CommentModel();

            Mock<ICommentAccess> mockCommentAccess = new Mock<ICommentAccess>();

            mockCommentAccess.Setup(t => t.PostComment(It.IsAny<CommentModel>())).Returns(outputs);

            CommentManager mockCommentManager = new CommentManager(mockCommentAccess.Object);

            //Act
            var result = mockCommentManager.PostComment(model);

            //Assert
            Assert.AreEqual(true, result);

        }

        [TestMethod]
        public void PostCommentInvalidInput()
        {
            //Arrange
            RestResponse outputs = new RestResponse();
            outputs.Content = "false";

            CommentModel model = new CommentModel();

            Mock<ICommentAccess> mockCommentAccess = new Mock<ICommentAccess>();

            mockCommentAccess.Setup(t => t.PostComment(It.IsAny<CommentModel>())).Returns(outputs);

            CommentManager mockCommentManager = new CommentManager(mockCommentAccess.Object);

            //Act
            var result = mockCommentManager.PostComment(model);

            //Assert
            Assert.AreEqual(false, result);

        }
    }
}