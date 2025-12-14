using Moq;
using SweetShop.Api.DTOs.Sweet;
using SweetShop.Api.Entities;
using SweetShop.Api.Helpers;
using SweetShop.Api.Repositories.Interfaces;
using SweetShop.Api.Services.Implementations;
using SweetShop.Api.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweetShop.Tests.Services
{
    public class SweetsServiceTests
    {
        private readonly Mock<ISweetsRepository> sweetRepo;
        private readonly ISweetsService sweetsService;
        private readonly Mock<ICurrentUserContext> currentUserContext;
        public SweetsServiceTests()
        {
            sweetRepo = new Mock<ISweetsRepository>();
            currentUserContext = new Mock<ICurrentUserContext>();
            sweetsService = new SweetsService(sweetRepo.Object, currentUserContext.Object);
        }

        [Fact]
        public async Task CreateSweet_Success_WhenDataIsValidAndUserIsAdmin()
        {
            // Arrange
            var request = new CreateSweetRequestDTO
            {
                Name = "Besan Laddoos",
                Category = "Laddoos",
                Price = 110,
                QuantityInStock = 5
            };

            var response = new Sweet
            {
                SweetId = 1,
                Name = "Besan Laddoos",
                Category = "Laddoos",
                Price = 110,
                QuantityInStock = 5
            };

            sweetRepo.Setup((r) => r.CreateSweet(It.IsAny<Sweet>())).ReturnsAsync(response);
            currentUserContext.Setup((c) => c.IsAdmin).Returns(true);

            // Act
            var result = await sweetsService.CreateSweet(request);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(200, result.StatusCode);
            Assert.NotNull(result.Data);
            Assert.Equal("Sweet created successfully.", result.Message);
        }

        [Fact]
        public async Task CreateSweet_Failure_WhenDataIsValidAndUserIsNotAdmin()
        {
            // Arrange
            var request = new CreateSweetRequestDTO
            {
                Name = "Besan Laddoos",
                Category = "Laddoos",
                Price = 110,
                QuantityInStock = 5
            };

            var response = new Sweet
            {
                SweetId = 1,
                Name = "Besan Laddoos",
                Category = "Laddoos",
                Price = 110,
                QuantityInStock = 5
            };

            sweetRepo.Setup((r) => r.CreateSweet(It.IsAny<Sweet>())).ReturnsAsync(response);
            currentUserContext.Setup((c) => c.IsAdmin).Returns(false);

            // Act
            var result = await sweetsService.CreateSweet(request);

            // Assert
            Assert.False(result.Success);
            Assert.Equal(403, result.StatusCode);
            Assert.Null(result.Data);
            Assert.Equal("Sweet creation failed.", result.Message);
        }

        [Fact]
        public async Task GetAllSweets_ShouldReturnList_WhenSweetsExist()
        {
            // Arrange
            var sweets = new List<Sweet>
            {
                new Sweet { SweetId = 1, Name = "Besan Ladoo", Category = "Ladoos", Price = 100 },
                new Sweet { SweetId = 2, Name = "Kaju Barfi", Category = "Barfi", Price = 150 }
            };

            sweetRepo.Setup(r => r.GetAllSweets())
                     .ReturnsAsync(sweets);

            // Act
            var result = await sweetsService.GetAllSweets();

            // Assert
            Assert.True(result.Success);
            Assert.Equal(200, result.StatusCode);
            Assert.NotNull(result.Data);
            Assert.Equal(2, result.Data.Count);
        }
    }
}
