using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NexIPTV.Core.DTOs;

namespace NexIPTV.API.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class ResellersController : ControllerBase
    {
        private readonly IResellerService _resellerService;

        [HttpPost("create-reseller")]
        public async Task<IActionResult> CreateReseller([FromBody] CreateResellerDto dto)
        {
            var reseller = await _resellerService.CreateResellerAsync(dto);
            return Ok(reseller);
        }

        [HttpPost("transfer-credits")]
        public async Task<IActionResult> TransferCredits([FromBody] CreditTransferDto dto)
        {
            await _resellerService.TransferCreditsAsync(dto);
            return Ok();
        }
    }
}
