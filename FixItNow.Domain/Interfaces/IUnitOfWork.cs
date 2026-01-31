using System;
using System.Threading.Tasks;

namespace FixItNow.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        ITicketRepository Tickets { get; }
        ITicketAssignmentRepository TicketAssignments { get; }
        IAuditLogRepository AuditLogs { get; }
        INotificationRepository Notifications { get; }
        
        // ? Payment System
        IPaymentRepository Payments { get; }
        IInvoiceRepository Invoices { get; }
        IWalletRepository Wallets { get; }
        ITransactionRepository Transactions { get; }
        IServiceChargeRepository ServiceCharges { get; }
        IPaymentMethodRepository PaymentMethods { get; }
        IPaymentStatusRepository PaymentStatuses { get; }
        
        // ? Advanced Features
        ICommentRepository Comments { get; }
        IAttachmentRepository Attachments { get; }
        IFeedbackRepository Feedbacks { get; }
        IChatMessageRepository ChatMessages { get; }
        ITechnicianAvailabilityRepository TechnicianAvailabilities { get; }
        ITechnicianEarningsRepository TechnicianEarnings { get; }
        ISystemSettingsRepository SystemSettings { get; }
        
        Task<int> CompleteAsync();
    }
}