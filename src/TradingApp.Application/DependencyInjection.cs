using Microsoft.Extensions.DependencyInjection;
using TradingApp.Application.Interfaces.Services;
using TradingApp.Application.Services;

namespace TradingApp.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IWalletService, WalletService>();
        return services;
    }
}
