using FixItNow.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FixItNow.Domain.Interfaces
{
    public interface IPaymentRepository
    {
        Task<Payment> GetByIdAsync(int paymentId);
        Task<Payment> GetByTicketIdAsync(int ticketId);
        Task<List<Payment>> GetByResidentIdAsync(int residentId);
        Task<List<Payment>> GetAllAsync();
        Task AddAsync(Payment payment);
        Task UpdateAsync(Payment payment);
    }

    public interface IInvoiceRepository
    {
        Task<Invoice> GetByIdAsync(int invoiceId);
        Task<Invoice> GetByTicketIdAsync(int ticketId);
        Task<List<Invoice>> GetByResidentIdAsync(int residentId);
        Task<List<Invoice>> GetUnpaidAsync();
        Task<List<Invoice>> GetAllAsync();
        Task AddAsync(Invoice invoice);
        Task UpdateAsync(Invoice invoice);
    }

    public interface IWalletRepository
    {
        Task<Wallet> GetByIdAsync(int walletId);
        Task<Wallet> GetByUserIdAsync(int userId);
        Task<List<Wallet>> GetAllAsync();
        Task AddAsync(Wallet wallet);
        Task UpdateAsync(Wallet wallet);
    }

    public interface ITransactionRepository
    {
        Task<Transaction> GetByIdAsync(int transactionId);
        Task<List<Transaction>> GetByWalletIdAsync(int walletId);
        Task<List<Transaction>> GetAllAsync();
        Task AddAsync(Transaction transaction);
    }

    public interface IServiceChargeRepository
    {
        Task<ServiceCharge> GetByIdAsync(int chargeId);
        Task<ServiceCharge> GetByCategoryAndPriorityAsync(int categoryId, int priorityId);
        Task<List<ServiceCharge>> GetAllAsync();
        Task AddAsync(ServiceCharge charge);
    }

    public interface IPaymentMethodRepository
    {
        Task<PaymentMethod> GetByIdAsync(int methodId);
        Task<List<PaymentMethod>> GetAllAsync();
        Task AddAsync(PaymentMethod method);
    }

    public interface IPaymentStatusRepository
    {
        Task<PaymentStatus> GetByIdAsync(int statusId);
        Task<List<PaymentStatus>> GetAllAsync();
        Task AddAsync(PaymentStatus status);
    }
}
