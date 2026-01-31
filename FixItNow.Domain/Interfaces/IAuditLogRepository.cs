using System.Collections.Generic;
using System.Threading.Tasks;
using FixItNow.Domain.Entities;

namespace FixItNow.Domain.Interfaces
{
    public interface IAuditLogRepository
    {
        Task AddAsync(AuditLog auditLog);
        Task<List<AuditLog>> FindByTicketAsync(int? ticketId);
    }
}