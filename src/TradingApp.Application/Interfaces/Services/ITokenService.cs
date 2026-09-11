using TradingApp.Domain.Entities;

namespace TradingApp.Application.Interfaces.Services;

public interface ITokenService
{
    string GenerateToken(User user);
}
