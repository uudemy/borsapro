using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TradingApp.Application.Interfaces.Services;

namespace TradingApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TransactionController : ControllerBase
{
    private readonly ITransactionHistoryService _transactionHistoryService;

    public TransactionController(ITransactionHistoryService transactionHistoryService)
    {
        _transactionHistoryService = transactionHistoryService;
    }

    [HttpGet]
    public async Task<IActionResult> GetMyTransactions(CancellationToken cancellationToken)
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var transactions = await _transactionHistoryService.GetUserTransactionsAsync(userId, cancellationToken);
        return Ok(transactions);
    }
}
