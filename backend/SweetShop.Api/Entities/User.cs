using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SweetShop.Api.Entities
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        [StringLength(200, MinimumLength = 1)]
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        [DefaultValue(false)]
        public bool IsAdmin { get; set; } = false;
    }
}
