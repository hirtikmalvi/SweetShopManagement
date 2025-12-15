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

        public SweetsController(ISweetsService _sweetsService)
        {
            sweetsService = _sweetsService;
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
    }
}
