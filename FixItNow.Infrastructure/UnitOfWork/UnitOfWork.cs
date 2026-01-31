using System.Threading.Tasks;
using FixItNow.Domain.Interfaces;
using FixItNow.Infrastructure.Data;
using FixItNow.Infrastructure.Repositories;

namespace FixItNow.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MongoDbContext _context;

        public UnitOfWork(MongoDbContext context)
        {
            _context = context;
            Users = new UserRepository(context);
            Tickets = new TicketRepository(context);
            TicketAssignments = new TicketAssignmentRepository(context);
            AuditLogs = new AuditLogRepository(context);
            Notifications = new NotificationRepository(context);
            
            // Payment System
            Payments = new PaymentRepository(context);
            Invoices = new InvoiceRepository(context);
            Wallets = new WalletRepository(context);
            Transactions = new TransactionRepository(context);
            ServiceCharges = new ServiceChargeRepository(context);
            PaymentMethods = new PaymentMethodRepository(context);
            PaymentStatuses = new PaymentStatusRepository(context);
            
            // Advanced Features
            Comments = new CommentRepository(context);
            Attachments = new AttachmentRepository(context);
            Feedbacks = new FeedbackRepository(context);
            ChatMessages = new ChatMessageRepository(context);
            TechnicianAvailabilities = new TechnicianAvailabilityRepository(context);
            TechnicianEarnings = new TechnicianEarningsRepository(context);
            SystemSettings = new SystemSettingsRepository(context);
        }

        public IUserRepository Users { get; private set; }
        public ITicketRepository Tickets { get; private set; }
        public ITicketAssignmentRepository TicketAssignments { get; private set; }
        public IAuditLogRepository AuditLogs { get; private set; }
        public INotificationRepository Notifications { get; private set; }
        
        // Payment
        public IPaymentRepository Payments { get; private set; }
        public IInvoiceRepository Invoices { get; private set; }
        public IWalletRepository Wallets { get; private set; }
        public ITransactionRepository Transactions { get; private set; }
        public IServiceChargeRepository ServiceCharges { get; private set; }
        public IPaymentMethodRepository PaymentMethods { get; private set; }
        public IPaymentStatusRepository PaymentStatuses { get; private set; }
        
        // Advanced Features
        public ICommentRepository Comments { get; private set; }
        public IAttachmentRepository Attachments { get; private set; }
        public IFeedbackRepository Feedbacks { get; private set; }
        public IChatMessageRepository ChatMessages { get; private set; }
        public ITechnicianAvailabilityRepository TechnicianAvailabilities { get; private set; }
        public ITechnicianEarningsRepository TechnicianEarnings { get; private set; }
        public ISystemSettingsRepository SystemSettings { get; private set; }

        public async Task<int> CompleteAsync()
        {
            return await Task.FromResult(1);
        }

        public void Dispose()
        {
            // MongoDB driver handles connections automatically
        }
    }
}