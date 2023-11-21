Test cases with Moq:

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


Take this method for example, to validate the user with correct input.
We instantiate a model testModel with some filler data.
Then we create a Mock of the interface UserAccess, which is where we retrieve our data from in the frontend.
This Mock object mockUserAccess has a method called Setup that allows us to set up a dependency within the method, in this example,
ValidateUser and GetUserRole methods are called in the two mockUserAccess.Setup() calls to specify what those dependencies will return as data 
in the method we currently are testing. This removes all dependency edge cases and side effects, and allows us to only test the functionality of the 
current method.