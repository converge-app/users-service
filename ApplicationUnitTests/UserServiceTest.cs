using System;
using Application.Models.Entities;
using Application.Repositories;
using Application.Services;
using Moq;
using Xunit;

namespace ApplicationUnitTests
{
    public class UserServiceTest
    {
        [Fact]
        public void Create_EmailAlreadyTaken_Throws()
        {

            // Arrange
            var mockEmail = "test@test.com";
            var userRepository = new Mock<IUserRepository>();
            userRepository.Setup(m => m.GetByEmail(mockEmail)).Returns(new User() { Email = mockEmail });
            var userService = new UserService(userRepository.Object);

            // Act
            // Assert
            Assert.Throws<Exception>(() => userService.Create(new User() { Email = "test@test.com" }));
        }

        [Fact]
        public void Create_EmailNotTaken_ReturnsUserWithEmail()
        {
            // Arrange
            var userRepository = new Mock<IUserRepository>();
            userRepository.Setup(m => m.GetByEmail("")).Returns((User)null);
            userRepository.Setup(m => m.Create(It.IsAny<User>())).Returns(new User());
            var userService = new UserService(userRepository.Object);

            // Act
            var actual = userService.Create(new User());

            // Assert
            Assert.Equal(new User().Email, actual.Email);
        }

        [Fact]
        public void Create_EmailNotTaken_ReturnsUserWithFirstName()
        {
            // Arrange
            var userRepository = new Mock<IUserRepository>();
            userRepository.Setup(m => m.GetByEmail("")).Returns((User)null);
            userRepository.Setup(m => m.Create(It.IsAny<User>())).Returns(new User());
            var userService = new UserService(userRepository.Object);

            // Act
            var actual = userService.Create(new User());

            // Assert
            Assert.Equal(new User().FirstName, actual.FirstName);
        }

        [Fact]
        public void Create_EmailNotTaken_ReturnsUserWithLastname()
        {
            // Arrange
            var userRepository = new Mock<IUserRepository>();
            userRepository.Setup(m => m.GetByEmail("")).Returns((User)null);
            userRepository.Setup(m => m.Create(It.IsAny<User>())).Returns(new User());
            var userService = new UserService(userRepository.Object);

            // Act
            var actual = userService.Create(new User());

            // Assert
            Assert.Equal(new User().LastName, actual.LastName);
        }

        [Fact]
        public void Update_UserById_ThrowsArgumentNullException()
        {
            // Arrange
            var userRepository = new Mock<IUserRepository>();
            userRepository.Setup(m => m.GetById(It.IsAny<string>())).Returns((User)null);
            var userService = new UserService(userRepository.Object);
            // Act
            // Assert
            Assert.Throws<ArgumentNullException>(() => userService.Update(new User()));
        }


        [Fact]
        public void Update_GotUserById_EmailWasTaken_ThrowsException()
        {
            // Arrange
            var mockEmail = "test@test.com";
            var userRepository = new Mock<IUserRepository>();
            userRepository.Setup(m => m.GetById(It.IsAny<string>())).Returns(new User() { Email = mockEmail });
            userRepository.Setup(m => m.GetByEmail(It.IsAny<string>())).Returns(new User() { Email = mockEmail });
            var userService = new UserService(userRepository.Object);
            // Act
            // Assert
            Assert.Throws<Exception>(() => userService.Update(new User() { Email = "test1@test.com" }));
        }

        [Fact]
        public void Update_GotUserById_EmailWasTaken_ReturnsUserWithEmail()
        {
            // Arrange
            var mockEmail = "test@test.com";
            var userRepository = new Mock<IUserRepository>();
            userRepository.Setup(m => m.GetById(It.IsAny<string>())).Returns(new User() { Email = mockEmail });
            userRepository.Setup(m => m.Update(It.IsAny<string>(), It.IsAny<User>()));

            var userService = new UserService(userRepository.Object);
            // Act
            userService.Update(new User() { Email = mockEmail });

            // Assert'

        }

        [Fact]
        public void Update_GotUserById_EmailWasTaken_ReturnsUserWithFirstName()
        {
            // Arrange
            var mockEmail = "test@test.com";
            var mockFirstName = "Test";
            var userRepository = new Mock<IUserRepository>();
            userRepository.Setup(m => m.GetById(It.IsAny<string>())).Returns(new User() { Email = mockEmail });
            userRepository.Setup(m => m.Update(It.IsAny<string>(), It.IsAny<User>()));

            var userService = new UserService(userRepository.Object);
            // Act
            userService.Update(new User() { Email = mockEmail, FirstName = mockFirstName });

            // Assert'

        }

        [Fact]
        public void Update_GotUserById_EmailWasTaken_ReturnsUserWithLastName()
        {
            // Arrange
            var mockEmail = "test@test.com";
            var mockLastName = "Test";
            var userRepository = new Mock<IUserRepository>();
            userRepository.Setup(m => m.GetById(It.IsAny<string>())).Returns(new User() { Email = mockEmail });
            userRepository.Setup(m => m.Update(It.IsAny<string>(), It.IsAny<User>()));

            var userService = new UserService(userRepository.Object);
            // Act
            userService.Update(new User() { Email = mockEmail, LastName = mockLastName });

            // Assert'

        }
    }
}