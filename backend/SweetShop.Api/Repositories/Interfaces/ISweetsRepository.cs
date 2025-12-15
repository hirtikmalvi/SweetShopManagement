using SweetShop.Api.DTOs.Sweet;
using SweetShop.Api.Entities;

namespace SweetShop.Api.Repositories.Interfaces
{
    public interface ISweetsRepository
    {
        Task<Sweet?> CreateSweet(Sweet sweet);
        Task<List<Sweet>> GetAllSweets();
        Task<List<Sweet>> SearchSweets(string? name, string? category, decimal? minPrice, decimal? maxPrice);
        Task<bool> SweetExist(int sweetId);
        Task<Sweet> UpdateSweet(Sweet request);
        Task<bool> DeleteSweet(int sweetId);
    }
}
