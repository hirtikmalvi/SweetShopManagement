using System.ComponentModel.DataAnnotations;

namespace SweetShop.Api.DTOs.Register
{
    public class RegisterUserRequestDTO
    {
        [Required]
        [MinLength(1, ErrorMessage = "Minimum Length of Name must be atleast 1 character.")]
        [MaxLength(200, ErrorMessage = "Maximum Length of Name should be atmost 200 character.")]
        public string Name { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Email must be in valid format.")]
        public string Email { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "Minimum Length of Password must be atleast 6 character.")]
        [MaxLength(15, ErrorMessage = "Maximum Length of Password should be atmost 15 character.")]
        public string Password { get; set; }
    }
}
