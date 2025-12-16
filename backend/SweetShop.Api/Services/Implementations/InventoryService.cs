using SweetShop.Api.DTOs.Sweet;
using SweetShop.Api.Entities;
using SweetShop.Api.Repositories.Interfaces;
using SweetShop.Api.Services.Interfaces;
using SweetShop.Api.Shared;

namespace SweetShop.Api.Services.Implementations
{
    public class InventoryService : IInventoryService
    {
        private readonly ISweetsRepository sweetsRepo;

        public InventoryService(ISweetsRepository _sweetsRepo)
        {
            sweetsRepo = _sweetsRepo;
        }

        public async Task<CustomResult<Sweet>> PurchaseSweet(int sweetId, UpdateSweetStockRequestDTO request)
        {
            if (sweetId != request.SweetId)
            {
                return CustomResult<Sweet>.Fail(
                    "Sweet could not be purchased.",
                    400,
                    ["SweetId mismatch."]
                );
            }

            var sweet = await sweetsRepo.GetSweetById(sweetId);

            if (sweet == null)
            {
                return CustomResult<Sweet>.Fail("Sweet could not be purchased.", 404, ["Sweet not found"]);
            }

            if (request.QuantityInStock > sweet.QuantityInStock)
            {
                return CustomResult<Sweet>.Fail("Sweet could not be purchased.", 400, ["Sweets are out of stock."]);
            }

            sweet.QuantityInStock -= request.QuantityInStock; 

            await sweetsRepo.UpdateSweet();
            return CustomResult<Sweet>.Ok(sweet, "Sweet purchased successfully.");

        }
    }
}
