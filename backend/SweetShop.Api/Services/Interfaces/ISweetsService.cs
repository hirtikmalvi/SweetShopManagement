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
        Task<CustomResult<Sweet>> UpdateSweet(int sweetId, UpdateSweetRequestDTO request);
        Task<CustomResult<bool>> DeleteSweet(int sweetId);
        Task<CustomResult<List<Sweet>>> GetSweetsWithMinimumQty();
    }
}
