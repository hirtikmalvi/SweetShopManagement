using Moq;
using SweetShop.Api.DTOs.Sweet;
using SweetShop.Api.Entities;
using SweetShop.Api.Helpers;
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
    public class InventoryServiceTests
    {
        private readonly Mock<ISweetsRepository> sweetsRepo;
        private readonly Mock<ICurrentUserContext> currentUserContext;
        private readonly IInventoryService inventoryService;
        public InventoryServiceTests()
        {
            sweetsRepo = new Mock<ISweetsRepository>();
            currentUserContext = new Mock<ICurrentUserContext>();
            inventoryService = new InventoryService(sweetsRepo.Object, currentUserContext.Object);
        }

        [Fact]
        public async Task PurchaseSweet_ShouldFail_WhenRouteIdDoesNotMatchRequestId()
        {
            // Arrange
            var routeSweetId = 1;

            var request = new UpdateSweetStockRequestDTO
            {
                SweetId = 2,
                QuantityInStock = 120
            };

            // Act
            var result = await inventoryService.PurchaseSweet(routeSweetId, request);

            // Assert
            Assert.False(result.Success);
            Assert.Equal(400, result.StatusCode);
            Assert.Null(result.Data);
            Assert.Equal("Sweet could not be purchased.", result.Message);
        }

        [Fact]
        public async Task PurchaseSweet_ShouldFail_WhenSweetDoesNotExist()
        {
            // Arrange
            var sweetId = 1;

            var request = new UpdateSweetStockRequestDTO
            {
                SweetId = 1,
                QuantityInStock = 99
            };

            sweetsRepo.Setup(r => r.GetSweetById(sweetId))
                         .ReturnsAsync((Sweet)null);

            // Act
            var result = await inventoryService.PurchaseSweet(sweetId, request);

            // Assert
            Assert.False(result.Success);
            Assert.Equal(404, result.StatusCode);
            Assert.Equal("Sweet could not be purchased.", result.Message);
        }

        [Fact]
        public async Task PurchaseSweet_ShouldFail_WhenStockIsNotEnough()
        {
            // Arrange
            var sweetId = 1;

            var request = new UpdateSweetStockRequestDTO
            {
                SweetId = 1,
                QuantityInStock = 45
            };

            var existingSweet = new Sweet
            {
                SweetId = 1,
                Name = "Besan Ladoo",
                Category = "Ladoo",
                Price = 100,
                QuantityInStock = 40,
            };

            sweetsRepo.Setup(r => r.GetSweetById(sweetId))
                     .ReturnsAsync(existingSweet);

            // Act
            var result = await inventoryService.PurchaseSweet(sweetId, request);

            // Assert
            Assert.False(result.Success);
            Assert.Equal(400, result.StatusCode);
            Assert.Null(result.Data);
            Assert.Equal("Sweet could not be purchased.", result.Message);
        }

        [Fact]
        public async Task UpdateSweet_ShouldSucceed_WhenDataIsValid()
        {
            // Arrange
            var sweetId = 1;

            var request = new UpdateSweetStockRequestDTO
            {
                SweetId = 1,
                QuantityInStock = 45
            };

            var existingSweet = new Sweet
            {
                SweetId = 1,
                Name = "Besan Ladoo",
                Category = "Ladoo",
                Price = 100,
                QuantityInStock = 50,
            };

            var sweetToReturnAfterPurchase = new Sweet
            {
                SweetId = 1,
                Name = "Besan Ladoo",
                Category = "Ladoo",
                Price = 100,
                QuantityInStock = 5,
            };

            sweetsRepo.Setup(r => r.GetSweetById(sweetId))
                     .ReturnsAsync(existingSweet);

            sweetsRepo.Setup(r => r.UpdateSweet());

            // Act
            var result = await inventoryService.PurchaseSweet(sweetId, request);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(200, result.StatusCode);
            Assert.NotNull(result.Data);
            Assert.Equal("Sweet purchased successfully.", result.Message);
        }

        [Fact]
        public async Task RestockSweet_ShouldFail_WhenRouteIdDoesNotMatchRequestId()
        {
            // Arrange
            var routeSweetId = 1;
            var request = new UpdateSweetStockRequestDTO
            {
                SweetId = 2,
                QuantityInStock = 10
            };

            // Act
            var result = await inventoryService.RestockSweet(routeSweetId, request);

            // Assert
            Assert.False(result.Success);
            Assert.Equal(400, result.StatusCode);
            Assert.Null(result.Data);
        }
    }
}
