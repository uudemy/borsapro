using TradingApp.Application.Interfaces.Repositories;

namespace TradingApp.Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly TradingDbContext _context;

    public UnitOfWork(TradingDbContext context, IUserRepository users)
    {
        _context = context;
        Users = users;
    }

    public IUserRepository Users { get; }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }
}
