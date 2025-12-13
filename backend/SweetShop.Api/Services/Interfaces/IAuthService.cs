using SweetShop.Api.DTOs.Login;
using SweetShop.Api.DTOs.Register;
using SweetShop.Api.Shared;

namespace SweetShop.Api.Services.Interfaces
{
    public interface IAuthService
    {
        Task<CustomResult<RegisterUserResponseDTO>> Register(RegisterUserRequestDTO request);
        Task<CustomResult<LoginResponseDTO>> Login(LoginRequestDTO request);
    }
}
