using System.ComponentModel.DataAnnotations;

namespace SweetShop.Api.DTOs.Sweet
{
    public class UpdateSweetStockRequestDTO
    {
        [Required]
        public int SweetId { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be atleast 1.")]
        public int QuantityInStock { get; set; }
    }
}
