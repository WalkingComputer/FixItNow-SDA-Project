using FixItNow.Domain.Entities;
using FixItNow.Domain.Interfaces;
using FixItNow.Infrastructure.Data;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FixItNow.Infrastructure.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly IMongoCollection<Payment> _payments;

        public PaymentRepository(MongoDbContext context)
        {
            _payments = context.Payments;
        }

        public async Task<Payment> GetByIdAsync(int paymentId)
        {
            var filter = Builders<Payment>.Filter.Eq(p => p.PaymentId, paymentId);
            return await _payments.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<Payment> GetByTicketIdAsync(int ticketId)
        {
            var filter = Builders<Payment>.Filter.Eq(p => p.TicketId, ticketId);
            return await _payments.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<List<Payment>> GetByResidentIdAsync(int residentId)
        {
            var filter = Builders<Payment>.Filter.Eq(p => p.ResidentId, residentId);
            return await _payments.Find(filter).ToListAsync();
        }

        public async Task<List<Payment>> GetAllAsync()
        {
            return await _payments.Find(_ => true).ToListAsync();
        }

        public async Task AddAsync(Payment payment)
        {
            await _payments.InsertOneAsync(payment);
        }

        public async Task UpdateAsync(Payment payment)
        {
            var filter = Builders<Payment>.Filter.Eq(p => p.PaymentId, payment.PaymentId);
            await _payments.ReplaceOneAsync(filter, payment);
        }
    }

    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly IMongoCollection<Invoice> _invoices;

        public InvoiceRepository(MongoDbContext context)
        {
            _invoices = context.Invoices;
        }

        public async Task<Invoice> GetByIdAsync(int invoiceId)
        {
            var filter = Builders<Invoice>.Filter.Eq(i => i.InvoiceId, invoiceId);
            return await _invoices.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<Invoice> GetByTicketIdAsync(int ticketId)
        {
            var filter = Builders<Invoice>.Filter.Eq(i => i.TicketId, ticketId);
            return await _invoices.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<List<Invoice>> GetByResidentIdAsync(int residentId)
        {
            var filter = Builders<Invoice>.Filter.Eq(i => i.ResidentId, residentId);
            return await _invoices.Find(filter).ToListAsync();
        }

        public async Task<List<Invoice>> GetUnpaidAsync()
        {
            var filter = Builders<Invoice>.Filter.Eq(i => i.IsPaid, false);
            return await _invoices.Find(filter).ToListAsync();
        }

        public async Task<List<Invoice>> GetAllAsync()
        {
            return await _invoices.Find(_ => true).ToListAsync();
        }

        public async Task AddAsync(Invoice invoice)
        {
            await _invoices.InsertOneAsync(invoice);
        }

        public async Task UpdateAsync(Invoice invoice)
        {
            var filter = Builders<Invoice>.Filter.Eq(i => i.InvoiceId, invoice.InvoiceId);
            await _invoices.ReplaceOneAsync(filter, invoice);
        }
    }

    public class WalletRepository : IWalletRepository
    {
        private readonly IMongoCollection<Wallet> _wallets;

        public WalletRepository(MongoDbContext context)
        {
            _wallets = context.Wallets;
        }

        public async Task<Wallet> GetByIdAsync(int walletId)
        {
            var filter = Builders<Wallet>.Filter.Eq(w => w.WalletId, walletId);
            return await _wallets.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<Wallet> GetByUserIdAsync(int userId)
        {
            var filter = Builders<Wallet>.Filter.Eq(w => w.UserId, userId);
            return await _wallets.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<List<Wallet>> GetAllAsync()
        {
            return await _wallets.Find(_ => true).ToListAsync();
        }

        public async Task AddAsync(Wallet wallet)
        {
            await _wallets.InsertOneAsync(wallet);
        }

        public async Task UpdateAsync(Wallet wallet)
        {
            var filter = Builders<Wallet>.Filter.Eq(w => w.WalletId, wallet.WalletId);
            await _wallets.ReplaceOneAsync(filter, wallet);
        }
    }

    public class TransactionRepository : ITransactionRepository
    {
        private readonly IMongoCollection<Transaction> _transactions;

        public TransactionRepository(MongoDbContext context)
        {
            _transactions = context.Transactions;
        }

        public async Task<Transaction> GetByIdAsync(int transactionId)
        {
            var filter = Builders<Transaction>.Filter.Eq(t => t.TransactionId, transactionId);
            return await _transactions.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<List<Transaction>> GetByWalletIdAsync(int walletId)
        {
            var filter = Builders<Transaction>.Filter.Eq(t => t.WalletId, walletId);
            return await _transactions.Find(filter).ToListAsync();
        }

        public async Task<List<Transaction>> GetAllAsync()
        {
            return await _transactions.Find(_ => true).ToListAsync();
        }

        public async Task AddAsync(Transaction transaction)
        {
            await _transactions.InsertOneAsync(transaction);
        }
    }

    public class ServiceChargeRepository : IServiceChargeRepository
    {
        private readonly IMongoCollection<ServiceCharge> _charges;

        public ServiceChargeRepository(MongoDbContext context)
        {
            _charges = context.ServiceCharges;
        }

        public async Task<ServiceCharge> GetByIdAsync(int chargeId)
        {
            var filter = Builders<ServiceCharge>.Filter.Eq(c => c.ServiceChargeId, chargeId);
            return await _charges.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<ServiceCharge> GetByCategoryAndPriorityAsync(int categoryId, int priorityId)
        {
            var filter = Builders<ServiceCharge>.Filter.And(
                Builders<ServiceCharge>.Filter.Eq(c => c.CategoryId, categoryId),
                Builders<ServiceCharge>.Filter.Eq(c => c.PriorityId, priorityId)
            );
            return await _charges.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<List<ServiceCharge>> GetAllAsync()
        {
            return await _charges.Find(_ => true).ToListAsync();
        }

        public async Task AddAsync(ServiceCharge charge)
        {
            await _charges.InsertOneAsync(charge);
        }
    }

    public class PaymentMethodRepository : IPaymentMethodRepository
    {
        private readonly IMongoCollection<PaymentMethod> _methods;

        public PaymentMethodRepository(MongoDbContext context)
        {
            _methods = context.PaymentMethods;
        }

        public async Task<PaymentMethod> GetByIdAsync(int methodId)
        {
            var filter = Builders<PaymentMethod>.Filter.Eq(m => m.PaymentMethodId, methodId);
            return await _methods.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<List<PaymentMethod>> GetAllAsync()
        {
            return await _methods.Find(_ => true).ToListAsync();
        }

        public async Task AddAsync(PaymentMethod method)
        {
            await _methods.InsertOneAsync(method);
        }
    }

    public class PaymentStatusRepository : IPaymentStatusRepository
    {
        private readonly IMongoCollection<PaymentStatus> _statuses;

        public PaymentStatusRepository(MongoDbContext context)
        {
            _statuses = context.PaymentStatuses;
        }

        public async Task<PaymentStatus> GetByIdAsync(int statusId)
        {
            var filter = Builders<PaymentStatus>.Filter.Eq(s => s.PaymentStatusId, statusId);
            return await _statuses.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<List<PaymentStatus>> GetAllAsync()
        {
            return await _statuses.Find(_ => true).ToListAsync();
        }

        public async Task AddAsync(PaymentStatus status)
        {
            await _statuses.InsertOneAsync(status);
        }
    }
}
