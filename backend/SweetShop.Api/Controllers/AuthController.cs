using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SweetShop.Api.DTOs.Login;
using SweetShop.Api.DTOs.Register;
using SweetShop.Api.Services.Interfaces;
using SweetShop.Api.Shared;
using System.Threading.Tasks;

namespace SweetShop.Api.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController (IAuthService authService) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserRequestDTO request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Where(ms => ms.Value.Errors.Count > 0).SelectMany(kvp => kvp.Value.Errors.Select(err => err.ErrorMessage)).ToList();
                return Ok(CustomResult<RegisterUserResponseDTO>.Fail("Registration Failed.", 400, errors));
            }
            var result = await authService.Register(request);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Where(ms => ms.Value.Errors.Count > 0).SelectMany(kvp => kvp.Value.Errors.Select(err => err.ErrorMessage)).ToList();
                return Ok(CustomResult<LoginResponseDTO>.Fail("Login Failed.", 400, errors));
            }

            var result = await authService.Login(request);

            if (!result.Success && result.StatusCode == 401)
            {
                return Unauthorized(result);
            }
            return Ok(result);
        }
    }
}
