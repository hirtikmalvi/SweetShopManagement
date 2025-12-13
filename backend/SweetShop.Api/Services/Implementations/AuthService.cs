using Microsoft.AspNetCore.Identity;
using SweetShop.Api.DTOs.Register;
using SweetShop.Api.Entities;
using SweetShop.Api.Repositories.Implementations;
using SweetShop.Api.Services.Interfaces;
using SweetShop.Api.Shared;

namespace SweetShop.Api.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository authRepository;
        public AuthService(IAuthRepository _authRepository)
        {
            authRepository = _authRepository;
        }
        public async Task<CustomResult<RegisterUserResponseDTO>> Register(RegisterUserRequestDTO request)
        {
            var userToRegister = new User();
            userToRegister.Name = request.Name;
            userToRegister.Email = request.Email;
            userToRegister.PasswordHash = new PasswordHasher<User>().HashPassword(userToRegister, request.Password);

            var user = await authRepository.Register(userToRegister);

            var registerUserResponse = new RegisterUserResponseDTO
            {
                UserId = user.UserId,
                Name = user.Name,
                Email = user.Email,
            };

            return CustomResult<RegisterUserResponseDTO>.Ok(registerUserResponse, "User Registered Successfully.");
        }
    }
}
