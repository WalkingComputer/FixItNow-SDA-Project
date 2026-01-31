using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using FixItNow.Domain.Entities;
using FixItNow.Domain.Interfaces;
using FixItNow.Infrastructure.Data;

namespace FixItNow.Infrastructure.Repositories
{
    public class TicketAssignmentRepository : ITicketAssignmentRepository
    {
        private readonly IMongoCollection<TicketAssignment> _assignments;

        public TicketAssignmentRepository(MongoDbContext context)
        {
            _assignments = context.TicketAssignments;
        }

        public async Task AddAsync(TicketAssignment assignment)
        {
            await _assignments.InsertOneAsync(assignment);
        }

        public async Task<TicketAssignment> GetActiveByTicketAsync(int ticketId)
        {
            var filter = Builders<TicketAssignment>.Filter.And(
                Builders<TicketAssignment>.Filter.Eq(a => a.TicketId, ticketId),
                Builders<TicketAssignment>.Filter.Eq(a => a.IsActive, true)
            );
            return await _assignments.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<List<TicketAssignment>> GetHistoryByTicketAsync(int ticketId)
        {
            var filter = Builders<TicketAssignment>.Filter.Eq(a => a.TicketId, ticketId);
            return await _assignments.Find(filter)
                .SortByDescending(a => a.AssignedAt)
                .ToListAsync();
        }
    }
}