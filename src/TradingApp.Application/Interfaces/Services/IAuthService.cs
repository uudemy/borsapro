using System.Security.Cryptography;
using System.Text;

namespace TradingApp.Application.Interfaces.Services;

public interface IAuthService
{
    Task<string> RegisterAsync(DTOs.Auth.RegisterRequest request, CancellationToken cancellationToken = default);
    Task<DTOs.Auth.AuthResponse> LoginAsync(DTOs.Auth.LoginRequest request, CancellationToken cancellationToken = default);
}
