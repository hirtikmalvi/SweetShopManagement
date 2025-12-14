using Microsoft.AspNetCore.Identity;
using SweetShop.Api.DTOs.Login;
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
            if (await authRepository.EmailExists(request.Email))
            {
                return CustomResult<RegisterUserResponseDTO>.Fail("Email already exists.", 409);
            }

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
        public async Task<CustomResult<LoginResponseDTO>> Login(LoginRequestDTO request)
        {
            var user = await authRepository.GetUserByEmail(request.Email);

            if (user == null)
            {
                return CustomResult<LoginResponseDTO>.Fail("Invalid Credentials.", 401);
            }

            if (new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash, request.Password) == PasswordVerificationResult.Failed)
            {
                return CustomResult<LoginResponseDTO>.Fail("Invalid Credentials.", 401);
            }

            return CustomResult<LoginResponseDTO>.Ok(new LoginResponseDTO
            {
                Token = "this is token."
            }, "User Logged in successfully.");
        }
    }
}
