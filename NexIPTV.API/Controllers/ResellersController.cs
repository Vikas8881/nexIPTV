using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NexIPTV.API.DTOs;
using NexIPTV.API.Interfaces;

[Authorize(Roles = "Admin")]
[Route("api/[controller]")]
[ApiController]
public class ResellersController : ControllerBase
{
    private readonly IResellerService _resellerService;

    public ResellersController(IResellerService resellerService)
    {
        _resellerService = resellerService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateReseller([FromBody] CreateResellerDto dto)
    {
        await _resellerService.CreateResellerAsync(dto);
        return Ok();
    }

    [HttpPost("transfer-credits")]
    public async Task<IActionResult> TransferCredits([FromBody] CreditTransferDto dto)
    {
        await _resellerService.TransferCreditsAsync(dto);
        return Ok();
    }
}