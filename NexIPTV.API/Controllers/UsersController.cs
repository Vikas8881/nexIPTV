using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NexIPTV.API.Entities;
using NexIPTV.API.Interfaces;
using System.Security.Claims;

[Authorize]
[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly UserManager<ApplicationUser> _userManager;

    public UsersController(IUserService userService, UserManager<ApplicationUser> userManager)
    {
        _userService = userService;
        _userManager = userManager;
    }

    [HttpPost("activate/{userId}")]
    public async Task<IActionResult> ActivateUser(string userId)
    {
        var activatorId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        await _userService.ActivateUserAsync(activatorId, userId);
        return Ok(new { Message = "User activated successfully" });
    }

    [HttpGet("trial-users")]
    public async Task<IActionResult> GetTrialUsers()
    {
        var trialUsers = await _userManager.Users
            .Where(u => u.IsTrial)
            .ToListAsync();

        return Ok(trialUsers);
    }
}