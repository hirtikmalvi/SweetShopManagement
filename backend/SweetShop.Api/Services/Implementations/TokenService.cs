using Microsoft.IdentityModel.Tokens;
using SweetShop.Api.Entities;
using SweetShop.Api.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SweetShop.Api.Services.Implementations
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration configuration;

        public TokenService(IConfiguration _configuration)
        {
            configuration = _configuration;
        }

        public string GenerateJwtToken(User user)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.IsAdmin ? "Admin": "User"),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes
                (configuration.GetValue<string>("JwtSettings:Key")!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: configuration.GetValue<string>("JwtSettings:Issuer")!,
                audience: configuration.GetValue<string>("JwtSettings:Audience")!,
                claims: claims,
                expires: DateTime.Now.AddMinutes(int.Parse(configuration.GetValue<string>("JwtSettings:ExpiryMinutes")!)),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
