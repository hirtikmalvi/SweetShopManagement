using Moq;
using Scalar.AspNetCore;
using SweetShop.Api.DTOs.Register;
using SweetShop.Api.Entities;
using SweetShop.Api.Repositories.Implementations;
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

        public AuthServiceTests()
        {
            authRepo = new Mock<IAuthRepository>();
            authService = new AuthService(authRepo.Object);
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
                Name = "Hirtik",
                Email = "hirtik@gmail.com",
                PasswordHash = "xyz12ABcdds",
                IsAdmin = false
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
    }
}
