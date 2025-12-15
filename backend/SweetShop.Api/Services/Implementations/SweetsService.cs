using SweetShop.Api.DTOs.Sweet;
using SweetShop.Api.Entities;
using SweetShop.Api.Helpers;
using SweetShop.Api.Repositories.Interfaces;
using SweetShop.Api.Services.Interfaces;
using SweetShop.Api.Shared;

namespace SweetShop.Api.Services.Implementations
{
    public class SweetsService : ISweetsService
    {
        private readonly ISweetsRepository sweetsRepo;
        private readonly ICurrentUserContext currentUserContext;
        public SweetsService(ISweetsRepository _sweetsRepo, ICurrentUserContext _currentUserContext) 
        {
            sweetsRepo = _sweetsRepo;
            currentUserContext = _currentUserContext;
        }
        public async Task<CustomResult<Sweet>> CreateSweet(CreateSweetRequestDTO request)
        {
            if (!currentUserContext.IsAdmin) // Not Admin
            {
                return CustomResult<Sweet>.Fail("Sweet creation failed.", 403, ["Only Admins are allowed to create sweets."]);
            }

            var sweet = new Sweet
            {
                Name = request.Name,
                Category = request.Category,
                Price = request.Price,
                QuantityInStock = request.QuantityInStock,
            };

            var createdSweet = await sweetsRepo.CreateSweet(sweet);

            return CustomResult<Sweet>.Ok(createdSweet, "Sweet created successfully.");
        }

        async Task<CustomResult<List<Sweet>>> ISweetsService.GetAllSweets()
        {
            var sweets = await sweetsRepo.GetAllSweets();
            if (sweets.Count == 0)
            {
                return CustomResult<List<Sweet>>.Ok(sweets,"No sweets exist.");
            }
            return CustomResult<List<Sweet>>.Ok(sweets,"Sweets fetched successfully.");
        }

        async Task<CustomResult<List<Sweet>>> ISweetsService.SearchSweets(SweetSearchRequestDTO request)
        {
            var sweets = await sweetsRepo.SearchSweets(request.Name, request.Category, request.MinPrice, request.MaxPrice);
            if (sweets.Count == 0)
            {
                return CustomResult<List<Sweet>>.Ok(sweets, "No Sweets found.");
            }
            return CustomResult<List<Sweet>>.Ok(sweets, "Sweets fetched successfully.");
        }
        public async Task<CustomResult<Sweet>> UpdateSweet(int sweetId, UpdateSweetRequestDTO request)
        {
            if (!currentUserContext.IsAdmin)
            {
                return CustomResult<Sweet>.Fail("Sweet can not be updated.", 403, ["Only Admins are allowed to update sweets."]);
            }

            if (sweetId != request.SweetId)
            {
                return CustomResult<Sweet>.Fail("Sweet can not be updated.", 400, [
                    "SweetId mismatch."
                ]);
            }

            if (!(await sweetsRepo.SweetExist(sweetId)))
            {
                return CustomResult<Sweet>.Fail("Sweet can not be updated.", 404, ["Sweet does not exist."]);
            }

            var sweetToUpdate = new Sweet
            {
                SweetId = request.SweetId,
                Name = request.Name,
                Category = request.Category,
                Price = request.Price,
                QuantityInStock = request.QuantityInStock
            };

            var updated = await sweetsRepo.UpdateSweet(sweetToUpdate);
            return CustomResult<Sweet>.Ok(updated, "Sweet updated successfully.");
        }
    }
}