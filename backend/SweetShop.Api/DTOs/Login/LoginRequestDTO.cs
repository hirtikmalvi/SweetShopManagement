using System.ComponentModel.DataAnnotations;

namespace SweetShop.Api.DTOs.Login
{
    public class LoginRequestDTO
    {
        [Required]
        [EmailAddress(ErrorMessage = "Email must be in valid format.")]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
