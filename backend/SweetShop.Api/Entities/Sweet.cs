using System.ComponentModel.DataAnnotations;

namespace SweetShop.Api.Entities
{
    public class Sweet
    {
        [Key]
        public int SweetId { get; set; }
        [Required]
        [StringLength(200, MinimumLength = 1)]
        public string Name { get; set; }
        [Required]
        [StringLength(200, MinimumLength = 1)]
        public string Category { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int QuantityInStock { get; set; }
    }
}
