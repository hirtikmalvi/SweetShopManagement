using SweetShop.Api.Entities;

namespace SweetShop.Api.Services.Interfaces
{
    public interface ITokenService
    {
        string GenerateJwtToken(User user);
    }
}
