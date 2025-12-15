using System.ComponentModel.DataAnnotations;

namespace SweetShop.Api.DTOs.Sweet
{
    public class UpdateSweetRequestDTO
    {
        public int SweetId { get; set; }
        [Required]
        [MinLength(1, ErrorMessage = "Minimum Length of Sweet Name must be atleast 1 character.")]
        [MaxLength(200, ErrorMessage = "Maximum Length of Sweet Name should be atmost 200 character.")]
        public string Name { get; set; }
        [Required]
        [MinLength(1, ErrorMessage = "Minimum Length of Category must be atleast 1 character.")]
        [MaxLength(200, ErrorMessage = "Maximum Length of Category should be atmost 200 character.")]
        public string Category { get; set; }
        [Required]
        [Range(0.0, double.MaxValue, ErrorMessage = "Price must be atleast 0.")]
        public decimal Price { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be atleast 1.")]
        public int QuantityInStock { get; set; }
    }
}
