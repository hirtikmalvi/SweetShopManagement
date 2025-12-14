using Microsoft.IdentityModel.JsonWebTokens;
using Moq;
using Scalar.AspNetCore;
using SweetShop.Api.DTOs.Login;
using SweetShop.Api.DTOs.Register;
using SweetShop.Api.Entities;
using SweetShop.Api.Repositories.Interfaces;
using SweetShop.Api.Services.Implementations;
using SweetShop.Api.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweetShop.Tests.Services
{
    public class AuthServiceTests
    {
        private readonly Mock<IAuthRepository> authRepo;
        private readonly IAuthService authService;
        private readonly Mock<ITokenService> tokenService;

        public AuthServiceTests()
        {
            authRepo = new Mock<IAuthRepository>();
            tokenService = new Mock<ITokenService>();
            authService = new AuthService(authRepo.Object, tokenService.Object);
        }

        [Fact]
        public async Task Register_Success_WhenDataIsValid()
        {
            // Arrange
            var request = new RegisterUserRequestDTO
            {
                Name = "Hirtik",
                Email = "hirtik@gmail.com",
                Password = "Hirtik@999"
            };

            var repoResponse = new User
            {
                UserId = 1,
                Name = "Hitesh",
                Email = "hitesh@gmail.com",
                PasswordHash = "AQAAAAIAAYagAAAAEMTxdtkM8/jeVUDvfoJQhUdUUDMc9l5S1TYg4xGbQFYqKtXVYGngDUWLZABJwA4etA==",
                IsAdmin = true
            };

            authRepo.Setup((r) => r.Register(It.IsAny<User>())).ReturnsAsync(repoResponse);

            // Act
            var result = await authService.Register(request);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(200, result.StatusCode);
            Assert.NotNull(result.Data);
            Assert.Equal("User Registered Successfully.", result.Message);
        }

        [Fact]
        public async Task Register_Fail_WhenEmailAlreadyExists()
        {
            // Arrange
            var request = new RegisterUserRequestDTO
            {
                Name = "Hirtik ",
                Email = "hirtik@gmail.com",
                Password = "Hirtik@999"
            };

            authRepo.Setup((r) => r.EmailExists(It.IsAny<string>())).ReturnsAsync(true);

            // Act
            var result = await authService.Register(request);

            // Assert
            Assert.False(result.Success);
            Assert.Equal(409, result.StatusCode);
            Assert.Null(result.Data);
            Assert.Equal("Email already exists.", result.Message);
        }

        [Fact]
        public async Task Login_Success_WhenValidCredentials()
        {
            // Arrange
            var request = new LoginRequestDTO
            {
                Email = "hitesh@gmail.com",
                Password = "Hitesh@1234"
            };

            var repoResponse = new User
            {
                UserId = 1,
                Name = "Hitesh",
                Email = "hitesh@gmail.com",
                PasswordHash = "AQAAAAIAAYagAAAAEMTxdtkM8/jeVUDvfoJQhUdUUDMc9l5S1TYg4xGbQFYqKtXVYGngDUWLZABJwA4etA==",
                IsAdmin = true
            };

            authRepo.Setup((r) => r.GetUserByEmail(It.IsAny<string>())).ReturnsAsync(repoResponse);
            
            // Act
            var result = await authService.Login(request);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(200, result.StatusCode);
            Assert.NotNull(result.Data);
            Assert.Equal("User Logged in successfully.", result.Message);
        }

        [Fact]
        public async Task Login_Fail_WhenEmailNotExist()
        {
            // Arrange
            var request = new LoginRequestDTO
            {
                Email = "hirtik@gmail.com",
                Password = "Hirtik@999"
            };

            authRepo.Setup((r) => r.GetUserByEmail(It.IsAny<string>())).ReturnsAsync((User)null);

            // Act
            var result = await authService.Login(request);

            // Assert
            Assert.False(result.Success);
            Assert.Equal(401, result.StatusCode);
            Assert.Null(result.Data);
            Assert.Equal("Invalid Credentials.", result.Message);
        }

        [Fact]
        public async Task Login_Fail_WhenPasswordNotMatch()
        {
            // Arrange
            var request = new LoginRequestDTO
            {
                Email = "hirtik@gmail.com",
                Password = "Hirtik@999"
            };

            var repoResponse = new User
            {
                UserId = 1,
                Name = "Hirtik",
                Email = "hirtik@gmail.com",
                PasswordHash = "AQAAAAIAAYagAAAAEMTxdtkM9/jeVUDvfoJQhUdUUDMc9l5S1TYg4xGbQFYqKtXVYGngDUWLZABJwA4etA==",
                IsAdmin = false
            };

            authRepo.Setup((r) => r.GetUserByEmail(It.IsAny<string>())).ReturnsAsync(repoResponse);

            // Act
            var result = await authService.Login(request);

            // Assert
            Assert.False(result.Success);
            Assert.Equal(401, result.StatusCode);
            Assert.Null(result.Data);
            Assert.Equal("Invalid Credentials.", result.Message);
        }

        [Fact]
        public async Task Login_ShouldReturnJwtToken_OnSuccess()
        {
            // Arrange
            var request = new LoginRequestDTO
            {
                Email = "hitesh@gmail.com",
                Password = "Hitesh@1234"
            };

            var repoResponse = new User
            {
                UserId = 1,
                Name = "Hitesh",
                Email = "hitesh@gmail.com",
                PasswordHash = "AQAAAAIAAYagAAAAEMTxdtkM8/jeVUDvfoJQhUdUUDMc9l5S1TYg4xGbQFYqKtXVYGngDUWLZABJwA4etA==",
                IsAdmin = true
            };

            authRepo.Setup((r) => r.GetUserByEmail(It.IsAny<string>())).ReturnsAsync(repoResponse);

            tokenService.Setup((r) => r.GenerateJwtToken(It.IsAny<User>())).Returns("fakeheader.fakepayload.fakesignature");

            // Act
            var result = await authService.Login(request);
            
            // Assert
            Assert.True(result.Success);
            Assert.Equal(200, result.StatusCode);
            Assert.NotNull(result.Data);
            Assert.True(new JsonWebTokenHandler().CanReadToken(result.Data.Token)); // Checks if the string is Jwt Token or not
            Assert.Equal("User Logged in successfully.", result.Message);
        }
    }
}
