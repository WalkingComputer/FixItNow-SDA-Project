using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using FixItNow.Domain.Entities;
using FixItNow.Domain.Interfaces;
using FixItNow.Infrastructure.Data;

namespace FixItNow.Infrastructure.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly IMongoCollection<Ticket> _tickets;

        public TicketRepository(MongoDbContext context)
        {
            _tickets = context.Tickets;
        }

        public async Task<Ticket> GetByIdAsync(int ticketId)
        {
            var filter = Builders<Ticket>.Filter.Eq(t => t.TicketId, ticketId);
            return await _tickets.Find(filter).FirstOrDefaultAsync();
        }

        public async Task AddAsync(Ticket ticket)
        {
            await _tickets.InsertOneAsync(ticket);
        }

        public async Task UpdateAsync(Ticket ticket)
        {
            var filter = Builders<Ticket>.Filter.Eq(t => t.TicketId, ticket.TicketId);
            await _tickets.ReplaceOneAsync(filter, ticket);
        }

        public async Task<List<Ticket>> FindByStatusAsync(int statusId)
        {
            var filter = Builders<Ticket>.Filter.Eq(t => t.StatusId, statusId);
            return await _tickets.Find(filter).ToListAsync();
        }

        public async Task<List<Ticket>> GetAllAsync()
        {
            return await _tickets.Find(_ => true).ToListAsync();
        }
    }
}