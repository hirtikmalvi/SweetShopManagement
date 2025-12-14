using SweetShop.Api.DTOs.Register;
using SweetShop.Api.Entities;

namespace SweetShop.Api.Repositories.Interfaces
{
    public interface IAuthRepository
    {
        Task<User?> Register(User request);
        Task<User?> GetUserByEmail(string email);
        Task<bool> EmailExists(string email);
    }
}
