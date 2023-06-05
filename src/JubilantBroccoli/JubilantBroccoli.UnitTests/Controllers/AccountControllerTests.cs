using AutoMapper;
using JubilantBroccoli.BusinessLogic.Contracts;
using JubilantBroccoli.Controllers;
using JubilantBroccoli.Domain.Dtos.User;
using JubilantBroccoli.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace JubilantBroccoli.UnitTests.Controllers
{
    public class AccountControllerTests
    {
        private readonly AccountController _controller;
        private readonly Mock<UserManager<IdentityUser>> _mockUserManager;
        private readonly Mock<IJwtGenerator> _mockJwtGenerator;
        private readonly Mock<IMapper> _mockMapper;

        public AccountControllerTests()
        {
            _mockUserManager = new Mock<UserManager<IdentityUser>>(
                Mock.Of<IUserStore<IdentityUser>>(),
                null, null, null, null, null, null, null, null);

            _mockJwtGenerator = new Mock<IJwtGenerator>();
            _mockMapper = new Mock<IMapper>();

            _controller = new AccountController(
                _mockUserManager.Object,
                _mockJwtGenerator.Object,
                _mockMapper.Object);
        }

        [Fact]
        public async Task PostUser_InvalidModel_ReturnsBadRequest()
        {
            // Arrange
            var user = new SignInRequest();
            _controller.ModelState.AddModelError("UserName", "The UserName field is required.");

            // Act
            var result = await _controller.PostUser(user);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public async Task PostUser_CreateUserFailed_ReturnsBadRequest()
        {
            // Arrange
            var user = new SignInRequest
            {
                UserName = "testuser",
                Email = "testuser@example.com",
                Address = "Test Address",
                Password = "TestPassword"
            };

            var errors = new List<IdentityError> { new IdentityError { Description = "Error 1" } };
            var identityResult = IdentityResult.Failed(errors.ToArray());

            _mockUserManager.Setup(x => x.CreateAsync(It.IsAny<User>(), user.Password))
                .ReturnsAsync(identityResult);

            // Act
            var result = await _controller.PostUser(user);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public async Task PostUser_ValidModel_ReturnsCreatedResult()
        {
            // Arrange
            var user = new SignInRequest
            {
                UserName = "testuser",
                Email = "testuser@example.com",
                Address = "Test Address",
                Password = "TestPassword"
            };

            var User = new User
            {
                Id = Guid.NewGuid().ToString(),
                UserName = user.UserName,
                Email = user.Email
            };

            _mockUserManager.Setup(x => x.CreateAsync(It.IsAny<User>(), user.Password))
                .ReturnsAsync(IdentityResult.Success);

            _mockMapper.Setup(x => x.Map<UserDto>(It.IsAny<SignInRequest>()))
                .Returns(new UserDto
                {
                    Id = User.Id,
                    UserName = User.UserName,
                    Email = User.Email,
                    Address = user.Address
                });

            // Act
            var result = await _controller.PostUser(user);

            // Assert
            var createdResult = Assert.IsType<CreatedResult>(result.Result);
            var userDto = Assert.IsType<UserDto>(createdResult.Value);
            Assert.Equal(User.Id, userDto.Id);
            Assert.Equal(User.UserName, userDto.UserName);
            Assert.Equal(User.Email, userDto.Email);
            Assert.Equal(user.Address, userDto.Address);
        }

        [Fact]
        public async Task CreateBearerToken_ValidCredentials_ReturnsOkResultWithToken()
        {
            // Arrange
            var request = new SignInRequest
            {
                UserName = "testuser",
                Password = "TestPassword"
            };

            var user = new User { UserName = request.UserName };
            var token = new AuthenticationResponse
            {
                Token = "testtoken"
            };

            _mockUserManager.Setup(x => x.FindByNameAsync(request.UserName))
                .ReturnsAsync(user);

            _mockUserManager.Setup(x => x.CheckPasswordAsync(user, request.Password))
                .ReturnsAsync(true);

            _mockJwtGenerator.Setup(x => x.CreateToken(user))
                .Returns(token);

            // Act
            var result = await _controller.CreateBearerToken(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(token, okResult.Value);
        }

        [Fact]
        public async Task CreateBearerToken_InvalidCredentials_ReturnsBadRequest()
        {
            // Arrange
            var request = new SignInRequest
            {
                UserName = "testuser",
                Password = "InvalidPassword"
            };

            _mockUserManager.Setup(x => x.FindByNameAsync(request.UserName))
                .ReturnsAsync((User)null);

            // Act
            var result = await _controller.CreateBearerToken(request);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }
    }
}
