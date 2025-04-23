using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NexIPTV.API.Services;
using NexIPTV.Core.Entities;
using NexIPTV.Core.Services;
using System.Security.Claims;

namespace NexIPTV.API.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserService _userService;

        [HttpPost("create-reseller")]
        public async Task<IActionResult> CreateReseller([FromBody] CreateUserDto dto)
        {
            var user = new ApplicationUser
            {
                UserName = dto.Email,
                Email = dto.Email,
                ParentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier)
            };

            var result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded) return BadRequest(result.Errors);

            await _userManager.AddToRoleAsync(user, "Reseller");
            return Ok(user);
        }

        [HttpPost("transfer-credits")]
        public async Task<IActionResult> TransferCredits([FromBody] CreditTransferDto dto)
        {
            await _userService.TransferCreditsAsync(
                User.FindFirstValue(ClaimTypes.NameIdentifier),
                dto.ReceiverId,
                dto.Amount
            );
            return Ok();
        }
    }
}
