using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TradingApp.Application.DTOs.Wallet;
using TradingApp.Application.Interfaces.Services;

namespace TradingApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class WalletController : ControllerBase
{
    private readonly IWalletService _walletService;

    public WalletController(IWalletService walletService)
    {
        _walletService = walletService;
    }

    private Guid GetUserId()
    {
        var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (Guid.TryParse(userIdString, out var userId))
        {
            return userId;
        }
        throw new UnauthorizedAccessException("User context is invalid. Please log in again.");
    }

    [HttpGet]
    public async Task<IActionResult> GetWallet(CancellationToken cancellationToken)
    {
        var wallet = await _walletService.GetWalletAsync(GetUserId(), cancellationToken);
        return Ok(wallet);
    }

    [HttpPost("deposit")]
    public async Task<IActionResult> Deposit([FromBody] DepositRequest request, CancellationToken cancellationToken)
    {
        var wallet = await _walletService.DepositAsync(GetUserId(), request.Amount, cancellationToken);
        return Ok(wallet);
    }

    [HttpPost("withdraw")]
    public async Task<IActionResult> Withdraw([FromBody] WithdrawRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var wallet = await _walletService.WithdrawAsync(GetUserId(), request.Amount, cancellationToken);
            return Ok(wallet);
        }
        catch (Exception ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }
}
