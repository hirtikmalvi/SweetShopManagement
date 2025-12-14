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

            sweetsRepo.Setup((r) => r.CreateSweet(It.Any<Sweet>())).ReturnsAsync(response);

            // Act
            var result = await sweetsService.CreateSweet(request);

            // Assert
            Assert.True(request.Success);
            Assert.Equal(200, request.StatusCode);
            Assert.NotNull(request.Data);
            Assert.Equal("Sweet created successfully.", request.Message);
        }
    }
}
