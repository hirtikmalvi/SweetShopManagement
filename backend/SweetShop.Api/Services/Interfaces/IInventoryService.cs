using SweetShop.Api.DTOs.Sweet;
using SweetShop.Api.Entities;
using SweetShop.Api.Shared;

namespace SweetShop.Api.Services.Interfaces
{
    public interface IInventoryService
    {
        Task<CustomResult<Sweet>> PurchaseSweet(int sweetId, UpdateSweetStockRequestDTO request);
    }
}
