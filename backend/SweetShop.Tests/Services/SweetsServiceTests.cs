using Azure;
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

        [Fact]
        public async Task GetAllSweets_ReturnsEmptyList_WhenNoSweetsExist()
        {
            // Arrange
            sweetRepo.Setup(r => r.GetAllSweets())
                     .ReturnsAsync(new List<Sweet>());

            // Act
            var result = await sweetsService.GetAllSweets();

            // Assert
            Assert.True(result.Success);
            Assert.Equal(200, result.StatusCode);
            Assert.NotNull(result.Data);
            Assert.Equal(0, result.Data.Count);
        }

        [Fact]
        public async Task SearchSweets_ShouldReturnResults_WhenNameMatches()
        {
            // Arrange
            var request = new SweetSearchRequestDTO
            {
                Name = "Ladoo"
            };

            var repoResult = new List<Sweet>
            {
                new Sweet { SweetId = 1, Name = "Besan Ladoo", Category = "Ladoos", Price = 100 }
            };

            sweetRepo.Setup(r =>
                r.SearchSweets("Ladoo", null, null, null))
                .ReturnsAsync(repoResult);

            // Act
            var result = await sweetsService.SearchSweets(request);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(200, result.StatusCode);
            Assert.Single(result.Data);
        }

        [Fact]
        public async Task SearchSweets_ShouldReturnResults_WhenCategoryMatches()
        {
            // Arrange
            var request = new SweetSearchRequestDTO
            {
                Category = "Ladoos"
            };

            var repoResult = new List<Sweet>
            {
                new Sweet { SweetId = 1, Name = "Besan Ladoo", Category = "Ladoos", Price = 100 }
            };

            sweetRepo.Setup(r =>
                r.SearchSweets(null, "Ladoos", null, null))
                .ReturnsAsync(repoResult);

            // Act
            var result = await sweetsService.SearchSweets(request);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(200, result.StatusCode);
            Assert.Single(result.Data);
        }
        
        [Fact]
        public async Task SearchSweets_ShouldReturnResults_WhenPriceInRange()
        {
            // Arrange
            var request = new SweetSearchRequestDTO
            {
                MinPrice = 50,
                MaxPrice = 150
            };

            var repoResult = new List<Sweet>
            {
                new Sweet { SweetId = 1, Name = "Besan Ladoo", Category = "Ladoos", Price = 100 }
            };

            sweetRepo.Setup(r =>
                r.SearchSweets(null, null, 50, 150))
                .ReturnsAsync(repoResult);

            // Act
            var result = await sweetsService.SearchSweets(request);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(200, result.StatusCode);
            Assert.Single(result.Data);
        }

        [Fact]
        public async Task SearchSweets_ShouldReturnEmptyList_WhenNoMatch()
        {
            // Arrange
            var request = new SweetSearchRequestDTO
            {
                Name = "Xyz",
                Category = "Abc",
                MinPrice = 784,
                MaxPrice = 999
            };

            var repoResult = new List<Sweet>();

            sweetRepo.Setup(r =>
                r.SearchSweets("Xyz", "Abc", 784, 999))
                .ReturnsAsync(repoResult);

            // Act
            var result = await sweetsService.SearchSweets(request);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal(0, result.Data.Count);
            Assert.Equal("No Sweets found.", result.Message);
        }

        [Fact]
        public async Task UpdateSweet_ShouldSucceed_WhenSweetExists()
        {
            // Arrange
            var request = new UpdateSweetRequestDTO
            {
                SweetId = 1,
                Name = "Xyz",
                Category = "Abc",
                Price = 784,
            };

            var repoResult = new Sweet
            {
                SweetId = 1,
                Name = "Besan Ladoo",
                Category = "Ladoos",
                Price = 100,
                QuantityInStock = 50
            };

            sweetRepo.Setup((r) => r.GetSweetById(It.IsAny<int>())).ReturnsAsync(repoResult);
            currentUserContext.Setup((c) => c.IsAdmin).Returns(true);
            sweetRepo.Setup((r) => r.UpdateSweet());

            // Act
            var result = await sweetsService.UpdateSweet(1, request);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(200, result.StatusCode);
            Assert.NotNull(result.Data);
            Assert.Equal("Sweet updated successfully.", result.Message);
        }

        [Fact]
        public async Task UpdateSweet_ShouldFail_WhenSweetDoesNotExist()
        {
            // Arrange
            var request = new UpdateSweetRequestDTO
            {
                SweetId = 1,
                Name = "Xyz",
                Category = "Abc",
                Price = 784,
            };

            sweetRepo.Setup((r) => r.SweetExist(It.IsAny<int>())).ReturnsAsync(false);
            currentUserContext.Setup((c) => c.IsAdmin).Returns(true);

            // Act
            var result = await sweetsService.UpdateSweet(1, request);

            // Assert
            Assert.False(result.Success);
            Assert.Equal(404, result.StatusCode);
            Assert.Null(result.Data);
            Assert.Equal("Sweet can not be updated.", result.Message);
        }

        [Fact]
        public async Task UpdateSweet_ShouldFail_WhenUserIsNotAdmin()
        {
            // Arrange
            var request = new UpdateSweetRequestDTO
            {
                SweetId = 1,
                Name = "Xyz",
                Category = "Abc",
                Price = 784,
            };

            currentUserContext.Setup((c) => c.IsAdmin).Returns(false);

            // Act
            var result = await sweetsService.UpdateSweet(1, request);

            // Assert
            Assert.False(result.Success);
            Assert.Equal(403, result.StatusCode);
            Assert.Null(result.Data);
            Assert.Equal("Sweet can not be updated.", result.Message);
        }

        [Fact]
        public async Task DeleteSweet_ShouldSucceed_WhenSweetExistsAndUserIsAdmin()
        {
            // Arrange
            sweetRepo.Setup((r) => r.SweetExist(It.IsAny<int>())).ReturnsAsync(true);
            currentUserContext.Setup((c) => c.IsAdmin).Returns(true);
            sweetRepo.Setup((r) => r.DeleteSweet(It.IsAny<int>())).ReturnsAsync(true);

            // Act
            var result = await sweetsService.DeleteSweet(1);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(200, result.StatusCode);
            Assert.NotNull(result.Data);
            Assert.Equal("Sweet deleted successfully.", result.Message);
        }

        [Fact]
        public async Task DeleteSweet_ShouldFail_WhenSweetDoesNotExist()
        {
            // Arrange
            sweetRepo.Setup((r) => r.SweetExist(It.IsAny<int>())).ReturnsAsync(false);
            currentUserContext.Setup((c) => c.IsAdmin).Returns(true);

            // Act
            var result = await sweetsService.DeleteSweet(1);

            // Assert
            Assert.False(result.Success);
            Assert.Equal(404, result.StatusCode);
            Assert.NotNull(result.Data);
            Assert.Equal("Sweet can not be deleted.", result.Message);
        }

        [Fact]
        public async Task DeleteSweet_ShouldFail_WhenUserIsNotAdmin()
        {
            // Arrange
            currentUserContext.Setup((c) => c.IsAdmin).Returns(false);

            // Act
            var result = await sweetsService.DeleteSweet(1);

            // Assert
            Assert.False(result.Data);
            Assert.False(result.Success);
            Assert.Equal(403, result.StatusCode);
            Assert.Equal("Sweet can not be deleted.", result.Message);
        }
    }
}
