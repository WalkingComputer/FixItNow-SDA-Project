using FixItNow.Application.Services;
using FixItNow.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace FixItNow.Application.Patterns
{
    /// <summary>
    /// STRATEGY PATTERN (Behavioral)
    /// Defines a family of payment algorithms and makes them interchangeable
    /// Allows payment method to be selected at runtime
    /// </summary>
    public interface IPaymentStrategy
    {
        string PaymentMethodName { get; }
        Task<PaymentResult> ProcessPaymentAsync(decimal amount, int userId, int invoiceId);
        Task<bool> ValidatePaymentAsync(decimal amount, int userId);
    }

    public class PaymentResult
    {
        public bool Success { get; set; }
        public string TransactionReference { get; set; }
        public string Message { get; set; }
        public DateTime ProcessedAt { get; set; }
    }

    /// <summary>
    /// Concrete Strategy: Wallet Payment
    /// </summary>
    public class WalletPaymentStrategy : IPaymentStrategy
    {
        private readonly IWalletService _walletService;

        public WalletPaymentStrategy(IWalletService walletService)
        {
            _walletService = walletService;
        }

        public string PaymentMethodName => "Digital Wallet";

        public async Task<bool> ValidatePaymentAsync(decimal amount, int userId)
        {
            var balance = await _walletService.GetBalanceAsync(userId);
            return balance >= amount;
        }

        public async Task<PaymentResult> ProcessPaymentAsync(decimal amount, int userId, int invoiceId)
        {
            Console.WriteLine($"💳 Strategy Pattern: Processing payment via {PaymentMethodName}");

            try
            {
                // Check balance
                var isValid = await ValidatePaymentAsync(amount, userId);
                if (!isValid)
                {
                    return new PaymentResult
                    {
                        Success = false,
                        Message = "Insufficient wallet balance",
                        ProcessedAt = DateTime.Now
                    };
                }

                // Deduct from wallet
                await _walletService.DeductMoneyAsync(userId, amount, $"Payment for Invoice #{invoiceId}");

                return new PaymentResult
                {
                    Success = true,
                    TransactionReference = $"WALLET-{DateTime.Now.Ticks}",
                    Message = "Payment successful via wallet",
                    ProcessedAt = DateTime.Now
                };
            }
            catch (Exception ex)
            {
                return new PaymentResult
                {
                    Success = false,
                    Message = $"Payment failed: {ex.Message}",
                    ProcessedAt = DateTime.Now
                };
            }
        }
    }

    /// <summary>
    /// Concrete Strategy: Cash Payment
    /// </summary>
    public class CashPaymentStrategy : IPaymentStrategy
    {
        public string PaymentMethodName => "Cash";

        public async Task<bool> ValidatePaymentAsync(decimal amount, int userId)
        {
            // Cash payment always valid (assumed user has cash)
            return await Task.FromResult(true);
        }

        public async Task<PaymentResult> ProcessPaymentAsync(decimal amount, int userId, int invoiceId)
        {
            Console.WriteLine($"💵 Strategy Pattern: Processing payment via {PaymentMethodName}");

            await Task.Delay(100); // Simulate processing

            return new PaymentResult
            {
                Success = true,
                TransactionReference = $"CASH-{DateTime.Now.Ticks}",
                Message = "Cash payment recorded",
                ProcessedAt = DateTime.Now
            };
        }
    }

    /// <summary>
    /// Concrete Strategy: Online Transfer Payment
    /// </summary>
    public class OnlineTransferPaymentStrategy : IPaymentStrategy
    {
        public string PaymentMethodName => "Online Bank Transfer";

        public async Task<bool> ValidatePaymentAsync(decimal amount, int userId)
        {
            // Assume online transfer is always valid
            return await Task.FromResult(true);
        }

        public async Task<PaymentResult> ProcessPaymentAsync(decimal amount, int userId, int invoiceId)
        {
            Console.WriteLine($"🏦 Strategy Pattern: Processing payment via {PaymentMethodName}");

            await Task.Delay(200); // Simulate bank processing

            return new PaymentResult
            {
                Success = true,
                TransactionReference = $"BANK-{DateTime.Now.Ticks}",
                Message = "Online transfer successful",
                ProcessedAt = DateTime.Now
            };
        }
    }

    /// <summary>
    /// Context class that uses the payment strategy
    /// </summary>
    public class PaymentProcessor
    {
        private IPaymentStrategy _paymentStrategy;

        public void SetPaymentStrategy(IPaymentStrategy strategy)
        {
            _paymentStrategy = strategy;
            Console.WriteLine($"🔄 Payment strategy set to: {strategy.PaymentMethodName}");
        }

        public async Task<PaymentResult> ProcessPaymentAsync(decimal amount, int userId, int invoiceId)
        {
            if (_paymentStrategy == null)
            {
                throw new InvalidOperationException("Payment strategy not set");
            }

            Console.WriteLine($"\n💰 Processing payment of PKR {amount:N2}");
            return await _paymentStrategy.ProcessPaymentAsync(amount, userId, invoiceId);
        }
    }
}
