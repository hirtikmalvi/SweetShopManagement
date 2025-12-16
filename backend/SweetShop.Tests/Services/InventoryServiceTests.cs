using Moq;
using SweetShop.Api.DTOs.Sweet;
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
    public class InventoryServiceTests
    {
        private readonly Mock<ISweetsRepository> sweetsRepo;
        private readonly IInventoryService inventoryService;
        public InventoryServiceTests()
        {
            sweetsrepo = new Mock<ISweetsRepository>();
            inventoryService = new IInventoryService(sweetsRepo.Object);
        }
        [Fact]
        public async Task UpdateSweet_ShouldFail_WhenRouteIdDoesNotMatchRequestId()
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
            Assert.Equal("SweetId mismatch.", result.Message);
        }
    }
}
