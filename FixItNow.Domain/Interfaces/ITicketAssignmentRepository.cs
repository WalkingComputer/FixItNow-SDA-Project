using System.Collections.Generic;
using System.Threading.Tasks;
using FixItNow.Domain.Entities;

namespace FixItNow.Domain.Interfaces
{
    public interface ITicketAssignmentRepository
    {
        Task AddAsync(TicketAssignment assignment);
        Task<TicketAssignment> GetActiveByTicketAsync(int ticketId);
        Task<List<TicketAssignment>> GetHistoryByTicketAsync(int ticketId);
    }
}