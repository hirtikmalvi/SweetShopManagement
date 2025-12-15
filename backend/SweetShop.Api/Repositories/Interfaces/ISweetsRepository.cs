using SweetShop.Api.Entities;

namespace SweetShop.Api.Repositories.Interfaces
{
    public interface ISweetsRepository
    {
        Task<Sweet?> CreateSweet(Sweet sweet);
        Task<List<Sweet>> GetAllSweets();
    }
}
