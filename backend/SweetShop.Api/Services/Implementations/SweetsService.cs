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
            return CustomResult<List<Sweet>>.Ok(sweets,"Sweets fetched successfully.");
        }
    }
}
