using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using FixItNow.Domain.Entities;
using FixItNow.Domain.Interfaces;
using FixItNow.Infrastructure.Data;

namespace FixItNow.Infrastructure.Repositories
{
    public class AuditLogRepository : IAuditLogRepository
    {
        private readonly IMongoCollection<AuditLog> _auditLogs;

        public AuditLogRepository(MongoDbContext context)
        {
            _auditLogs = context.AuditLogs;
        }

        public async Task AddAsync(AuditLog auditLog)
        {
            await _auditLogs.InsertOneAsync(auditLog);
        }

        public async Task<List<AuditLog>> FindByTicketAsync(int? ticketId)
        {
            if (ticketId == null)
            {
                return await _auditLogs.Find(_ => true).ToListAsync();
            }

            var filter = Builders<AuditLog>.Filter.Eq(a => a.TicketId, ticketId);
            return await _auditLogs.Find(filter)
                .SortByDescending(a => a.PerformedAt)
                .ToListAsync();
        }
    }
}