using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TradingApp.Application.DTOs.Order;
using TradingApp.Application.Interfaces.Services;

namespace TradingApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    private Guid GetUserId()
    {
        return Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
    }

    [HttpGet]
    public async Task<IActionResult> GetMyOrders(CancellationToken cancellationToken)
    {
        var orders = await _orderService.GetOrdersAsync(GetUserId(), cancellationToken);
        return Ok(orders);
    }

    [HttpPost("market")]
    public async Task<IActionResult> PlaceMarketOrder([FromBody] PlaceOrderRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _orderService.PlaceMarketOrderAsync(GetUserId(), request, cancellationToken);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }
}
