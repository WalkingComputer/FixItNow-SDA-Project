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
        }

        public IUserRepository Users { get; private set; }
        public ITicketRepository Tickets { get; private set; }
        public ITicketAssignmentRepository TicketAssignments { get; private set; }
        public IAuditLogRepository AuditLogs { get; private set; }
        public INotificationRepository Notifications { get; private set; }

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