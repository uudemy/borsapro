using Microsoft.EntityFrameworkCore;
using TradingApp.Application.Interfaces.Repositories;
using TradingApp.Domain.Entities;

namespace TradingApp.Infrastructure.Persistence.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly TradingDbContext _context;

    public OrderRepository(TradingDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Order>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await _context.Orders
            .Include(o => o.Asset)
            .Where(o => o.UserId == userId)
            .OrderByDescending(o => o.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Order order, CancellationToken cancellationToken = default)
    {
        await _context.Orders.AddAsync(order, cancellationToken);
    }
}
