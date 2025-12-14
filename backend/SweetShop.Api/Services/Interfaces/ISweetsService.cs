using SweetShop.Api.DTOs.Sweet;
using SweetShop.Api.Entities;
using SweetShop.Api.Shared;

namespace SweetShop.Api.Services.Interfaces
{
    public interface ISweetsService
    {
        Task<CustomResult<Sweet>> CreateSweet(CreateSweetRequestDTO request);
    }
}
