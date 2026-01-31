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
        Task<int> CompleteAsync();
    }
}