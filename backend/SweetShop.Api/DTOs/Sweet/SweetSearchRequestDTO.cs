namespace SweetShop.Api.DTOs.Sweet
{
    public class SweetSearchRequestDTO
    {
        public string? Name { get; set; }
        public string? Category { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
    }
}
