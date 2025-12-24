using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SweetShop.Api.DTOs.Register;
using SweetShop.Api.DTOs.Sweet;
using SweetShop.Api.Entities;
using SweetShop.Api.Services.Implementations;
using SweetShop.Api.Services.Interfaces;
using SweetShop.Api.Shared;

namespace SweetShop.Api.Controllers
{
    [Route("api/sweets")]
    [ApiController]
    public class SweetsController : ControllerBase
    {
        private readonly ISweetsService sweetsService;
        private readonly IInventoryService inventoryService;

        public SweetsController(ISweetsService _sweetsService, IInventoryService _inventoryService)
        {
            sweetsService = _sweetsService;
            inventoryService = _inventoryService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateSweet([FromBody] CreateSweetRequestDTO request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Where(ms => ms.Value.Errors.Count > 0).SelectMany(kvp => kvp.Value.Errors.Select(err => err.ErrorMessage)).ToList();
                return Ok(CustomResult<Sweet>.Fail("Sweet Creation Failed.", 400, errors));
            }
            var result = await sweetsService.CreateSweet(request);
            return Ok(result);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllSweets()
        {
            var result = await sweetsService.GetAllSweets();
            return Ok(result);
        }

        [HttpGet("sweets-with-minimum-qty")]
        [Authorize]
        public async Task<IActionResult> GetSweetsWithMinimumQty()
        {
            var result = await sweetsService.GetSweetsWithMinimumQty();
            return Ok(result);
        }

        [HttpGet("search")]
        [Authorize]
        public async Task<IActionResult> SearchSweets([FromBody] SweetSearchRequestDTO request)
        {
            var result = await sweetsService.SearchSweets(request);
            return Ok(result);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateSweet([FromRoute] int id, [FromBody] UpdateSweetRequestDTO request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Where(ms => ms.Value.Errors.Count > 0).SelectMany(kvp => kvp.Value.Errors.Select(err => err.ErrorMessage)).ToList();
                return Ok(CustomResult<Sweet>.Fail("Sweet can not be updated.", 400, errors));
            }
            var result = await sweetsService.UpdateSweet(id, request);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteSweet([FromRoute] int id)
        {
            var result = await sweetsService.DeleteSweet(id);
            return Ok(result);
        }

        [HttpPost("{id}/purchase")]
        [Authorize]
        public async Task<IActionResult> PurchaseSweet([FromRoute] int id, [FromBody] UpdateSweetStockRequestDTO request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Where(ms => ms.Value.Errors.Count > 0).SelectMany(kvp => kvp.Value.Errors.Select(err => err.ErrorMessage)).ToList();
                return Ok(CustomResult<Sweet>.Fail("Sweet can not be purchased.", 400, errors));
            }
            var result = await inventoryService.PurchaseSweet(id, request);
            return Ok(result);
        }

        [HttpPost("{id}/restock")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RestockSweet([FromRoute] int id, [FromBody] UpdateSweetStockRequestDTO request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Where(ms => ms.Value.Errors.Count > 0).SelectMany(kvp => kvp.Value.Errors.Select(err => err.ErrorMessage)).ToList();
                return Ok(CustomResult<Sweet>.Fail("Sweet can not be restocked.", 400, errors));
            }
            var result = await inventoryService.RestockSweet(id, request);
            return Ok(result);
        }
    }
}
