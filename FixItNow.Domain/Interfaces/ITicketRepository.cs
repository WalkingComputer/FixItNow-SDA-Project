using System.Collections.Generic;
using System.Threading.Tasks;
using FixItNow.Domain.Entities;

namespace FixItNow.Domain.Interfaces
{
    public interface ITicketRepository
    {
        Task<Ticket> GetByIdAsync(int ticketId);
        Task AddAsync(Ticket ticket);
        Task UpdateAsync(Ticket ticket);
        Task<List<Ticket>> FindByStatusAsync(int statusId);
        Task<List<Ticket>> GetAllAsync();
    }
}