using SweetShop.Api.DTOs.Sweet;
using SweetShop.Api.Entities;
using SweetShop.Api.Shared;

namespace SweetShop.Api.Services.Interfaces
{
    public interface ISweetsService
    {
        Task<CustomResult<Sweet>> CreateSweet(CreateSweetRequestDTO request);
        Task<CustomResult<List<Sweet>>> GetAllSweets();
        Task<CustomResult<List<Sweet>>> SearchSweets(SweetSearchRequestDTO request);
    }
}
