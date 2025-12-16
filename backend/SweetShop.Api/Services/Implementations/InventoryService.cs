using SweetShop.Api.DTOs.Sweet;
using SweetShop.Api.Entities;
using SweetShop.Api.Helpers;
using SweetShop.Api.Repositories.Interfaces;
using SweetShop.Api.Services.Interfaces;
using SweetShop.Api.Shared;

namespace SweetShop.Api.Services.Implementations
{
    public class InventoryService : IInventoryService
    {
        private readonly ISweetsRepository sweetsRepo;
        private readonly ICurrentUserContext currentUserContext;

        public InventoryService(ISweetsRepository _sweetsRepo, ICurrentUserContext _currentUserContext)
        {
            sweetsRepo = _sweetsRepo;
            currentUserContext = _currentUserContext;
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

            if (sweet.QuantityInStock == 0 || request.QuantityInStock > sweet.QuantityInStock)
            {
                return CustomResult<Sweet>.Fail("Sweet could not be purchased.", 400, ["Sweets are out of stock."]);
            }

            sweet.QuantityInStock -= request.QuantityInStock; 

            await sweetsRepo.UpdateSweet();
            return CustomResult<Sweet>.Ok(sweet, "Sweet purchased successfully.");

        }
        public async Task<CustomResult<Sweet>> RestockSweet(int sweetId, UpdateSweetStockRequestDTO request)
        {
            if (sweetId != request.SweetId)
            {
                return CustomResult<Sweet>.Fail(
                    "Sweet could not be restocked.",
                    400,
                    ["SweetId mismatch."]
                );
            }
            if (!currentUserContext.IsAdmin)
            {
                return CustomResult<Sweet>.Fail(
                    "Sweet could not be restocked.",
                    403,
                    ["Only admins are allowed to restock sweets."]
                );
            }

            if (request.QuantityInStock <= 0)
            {
                return CustomResult<Sweet>.Fail(
                    "Sweet could not be restocked.",
                    400,
                    ["Quantity must be greater than zero."]
                );
            }

            var sweet = await sweetsRepo.GetSweetById(sweetId);

            if (sweet == null)
            {
                return CustomResult<Sweet>.Fail(
                    "Sweet could not be restocked.",
                    404,
                    ["Sweet not found."]
                );
            }

            sweet.QuantityInStock += request.QuantityInStock;

            await sweetsRepo.UpdateSweet();

            return CustomResult<Sweet>.Ok(sweet, "Sweet restocked successfully.");
        }
    }
}
