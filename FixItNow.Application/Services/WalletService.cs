using FixItNow.Domain.Entities;
using FixItNow.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FixItNow.Application.Services
{
    /// <summary>
    /// ?? DUMMY WALLET SERVICE - SIMULATED BALANCE ONLY
    /// No real money transfers
    /// </summary>
    public interface IWalletService
    {
        Task<Wallet> GetWalletAsync(int userId);
        Task<Wallet> CreateWalletAsync(int userId);
        Task<decimal> GetBalanceAsync(int userId);
        Task AddMoneyAsync(int userId, decimal amount, string description);
        Task DeductMoneyAsync(int userId, decimal amount, string description);
        Task<List<Transaction>> GetTransactionHistoryAsync(int userId);
    }

    public class WalletService : IWalletService
    {
        private readonly IUnitOfWork _unitOfWork;

        public WalletService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Wallet> GetWalletAsync(int userId)
        {
            var wallet = await _unitOfWork.Wallets.GetByUserIdAsync(userId);
            if (wallet == null)
            {
                wallet = await CreateWalletAsync(userId);
            }
            return wallet;
        }

        /// <summary>
        /// ? Create dummy wallet with default balance PKR 5000
        /// </summary>
        public async Task<Wallet> CreateWalletAsync(int userId)
        {
            var allWallets = await _unitOfWork.Wallets.GetAllAsync();
            var walletId = allWallets.Any() ? allWallets.Max(w => w.WalletId) + 1 : 1;

            var wallet = new Wallet
            {
                WalletId = walletId,
                UserId = userId,
                Balance = 5000, // Default dummy balance
                LastUpdated = DateTime.Now,
                IsDummy = true
            };

            await _unitOfWork.Wallets.AddAsync(wallet);
            return wallet;
        }

        public async Task<decimal> GetBalanceAsync(int userId)
        {
            var wallet = await GetWalletAsync(userId);
            return wallet.Balance;
        }

        /// <summary>
        /// ? Simulate adding money (dummy transaction)
        /// </summary>
        public async Task AddMoneyAsync(int userId, decimal amount, string description)
        {
            var wallet = await GetWalletAsync(userId);
            wallet.Balance += amount;
            wallet.LastUpdated = DateTime.Now;
            await _unitOfWork.Wallets.UpdateAsync(wallet);

            // Create transaction record (SIMULATED)
            var allTransactions = await _unitOfWork.Transactions.GetAllAsync();
            var transactionId = allTransactions.Any() ? allTransactions.Max(t => t.TransactionId) + 1 : 1;

            var transaction = new Transaction
            {
                TransactionId = transactionId,
                WalletId = wallet.WalletId,
                Amount = amount,
                TransactionType = "Credit",
                Description = description + " (SIMULATED)",
                ReferenceId = $"REF-{transactionId:D5}",
                TransactionDate = DateTime.Now,
                IsSimulated = true
            };

            await _unitOfWork.Transactions.AddAsync(transaction);
        }

        /// <summary>
        /// ? Simulate deducting money (dummy transaction)
        /// </summary>
        public async Task DeductMoneyAsync(int userId, decimal amount, string description)
        {
            var wallet = await GetWalletAsync(userId);

            if (wallet.Balance < amount)
                throw new Exception($"Insufficient balance. Available: PKR {wallet.Balance:N2}, Required: PKR {amount:N2}");

            wallet.Balance -= amount;
            wallet.LastUpdated = DateTime.Now;
            await _unitOfWork.Wallets.UpdateAsync(wallet);

            // Create transaction record (SIMULATED)
            var allTransactions = await _unitOfWork.Transactions.GetAllAsync();
            var transactionId = allTransactions.Any() ? allTransactions.Max(t => t.TransactionId) + 1 : 1;

            var transaction = new Transaction
            {
                TransactionId = transactionId,
                WalletId = wallet.WalletId,
                Amount = amount,
                TransactionType = "Debit",
                Description = description + " (SIMULATED)",
                ReferenceId = $"REF-{transactionId:D5}",
                TransactionDate = DateTime.Now,
                IsSimulated = true
            };

            await _unitOfWork.Transactions.AddAsync(transaction);
        }

        public async Task<List<Transaction>> GetTransactionHistoryAsync(int userId)
        {
            var wallet = await GetWalletAsync(userId);
            return await _unitOfWork.Transactions.GetByWalletIdAsync(wallet.WalletId);
        }
    }
}
