using TradingApp.Application.DTOs.Wallet;
using TradingApp.Application.Interfaces.Repositories;
using TradingApp.Application.Interfaces.Services;
using TradingApp.Domain.Entities;

namespace TradingApp.Application.Services;

public class WalletService : IWalletService
{
    private readonly IUnitOfWork _unitOfWork;

    public WalletService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<WalletResponse> GetWalletAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var wallet = await _unitOfWork.Wallets.GetByUserIdAsync(userId, cancellationToken);
        if (wallet == null)
        {
            wallet = new Wallet { UserId = userId, AvailableBalance = 0, LockedBalance = 0, Currency = "USD" };
            await _unitOfWork.Wallets.AddAsync(wallet, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        return new WalletResponse { Id = wallet.Id, UserId = wallet.UserId, Balance = wallet.AvailableBalance, Currency = wallet.Currency };
    }

    public async Task<WalletResponse> DepositAsync(Guid userId, decimal amount, CancellationToken cancellationToken = default)
    {
        var wallet = await _unitOfWork.Wallets.GetByUserIdAsync(userId, cancellationToken);
        if (wallet == null)
        {
            wallet = new Wallet { UserId = userId, AvailableBalance = 0, LockedBalance = 0, Currency = "USD" };
            await _unitOfWork.Wallets.AddAsync(wallet, cancellationToken);
        }

        wallet.AvailableBalance += amount;
        
        if (wallet.Id != Guid.Empty)
        {
            _unitOfWork.Wallets.Update(wallet);
        }
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new WalletResponse { Id = wallet.Id, UserId = wallet.UserId, Balance = wallet.AvailableBalance, Currency = wallet.Currency };
    }

    public async Task<WalletResponse> WithdrawAsync(Guid userId, decimal amount, CancellationToken cancellationToken = default)
    {
        var wallet = await _unitOfWork.Wallets.GetByUserIdAsync(userId, cancellationToken);
        if (wallet == null || wallet.AvailableBalance < amount)
        {
            throw new Exception("Insufficient funds. You cannot withdraw more than your current balance.");
        }

        wallet.AvailableBalance -= amount;
        _unitOfWork.Wallets.Update(wallet);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new WalletResponse { Id = wallet.Id, UserId = wallet.UserId, Balance = wallet.AvailableBalance, Currency = wallet.Currency };
    }
}
