using FixItNow.Domain.Entities;
using FixItNow.Domain.Interfaces;
using FixItNow.Infrastructure.Data;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FixItNow.Infrastructure.Repositories
{
    public class ChatMessageRepository : IChatMessageRepository
    {
        private readonly IMongoCollection<ChatMessage> _messages;

        public ChatMessageRepository(MongoDbContext context)
        {
            _messages = context.ChatMessages;
        }

        public async Task<List<ChatMessage>> GetByTicketIdAsync(int ticketId)
        {
            var filter = Builders<ChatMessage>.Filter.Eq(m => m.TicketId, ticketId);
            var sort = Builders<ChatMessage>.Sort.Ascending(m => m.SentAt);
            return await _messages.Find(filter).Sort(sort).ToListAsync();
        }

        public async Task<List<ChatMessage>> GetConversationAsync(int userId1, int userId2, int ticketId)
        {
            var filter = Builders<ChatMessage>.Filter.And(
                Builders<ChatMessage>.Filter.Eq(m => m.TicketId, ticketId),
                Builders<ChatMessage>.Filter.Or(
                    Builders<ChatMessage>.Filter.And(
                        Builders<ChatMessage>.Filter.Eq(m => m.SenderId, userId1),
                        Builders<ChatMessage>.Filter.Eq(m => m.ReceiverId, userId2)
                    ),
                    Builders<ChatMessage>.Filter.And(
                        Builders<ChatMessage>.Filter.Eq(m => m.SenderId, userId2),
                        Builders<ChatMessage>.Filter.Eq(m => m.ReceiverId, userId1)
                    )
                )
            );
            var sort = Builders<ChatMessage>.Sort.Ascending(m => m.SentAt);
            return await _messages.Find(filter).Sort(sort).ToListAsync();
        }

        public async Task AddAsync(ChatMessage message)
        {
            await _messages.InsertOneAsync(message);
        }

        public async Task MarkAsReadAsync(int messageId)
        {
            var filter = Builders<ChatMessage>.Filter.Eq(m => m.MessageId, messageId);
            var update = Builders<ChatMessage>.Update.Set(m => m.IsRead, true);
            await _messages.UpdateOneAsync(filter, update);
        }

        public async Task<int> GetNextMessageIdAsync()
        {
            var all = await GetByTicketIdAsync(0);
            var allMessages = await _messages.Find(_ => true).ToListAsync();
            return allMessages.Any() ? allMessages.Max(m => m.MessageId) + 1 : 1;
        }
    }

    public class TechnicianAvailabilityRepository : ITechnicianAvailabilityRepository
    {
        private readonly IMongoCollection<TechnicianAvailability> _availabilities;

        public TechnicianAvailabilityRepository(MongoDbContext context)
        {
            _availabilities = context.TechnicianAvailabilities;
        }

        public async Task<TechnicianAvailability> GetByTechnicianIdAsync(int technicianId)
        {
            var filter = Builders<TechnicianAvailability>.Filter.Eq(a => a.TechnicianId, technicianId);
            return await _availabilities.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<List<TechnicianAvailability>> GetAllAsync()
        {
            return await _availabilities.Find(_ => true).ToListAsync();
        }

        public async Task AddAsync(TechnicianAvailability availability)
        {
            await _availabilities.InsertOneAsync(availability);
        }

        public async Task UpdateAsync(TechnicianAvailability availability)
        {
            var filter = Builders<TechnicianAvailability>.Filter.Eq(a => a.AvailabilityId, availability.AvailabilityId);
            await _availabilities.ReplaceOneAsync(filter, availability);
        }

        public async Task<int> GetNextAvailabilityIdAsync()
        {
            var all = await GetAllAsync();
            return all.Any() ? all.Max(a => a.AvailabilityId) + 1 : 1;
        }
    }

    public class TechnicianEarningsRepository : ITechnicianEarningsRepository
    {
        private readonly IMongoCollection<TechnicianEarnings> _earnings;

        public TechnicianEarningsRepository(MongoDbContext context)
        {
            _earnings = context.TechnicianEarnings;
        }

        public async Task<List<TechnicianEarnings>> GetByTechnicianIdAsync(int technicianId)
        {
            var filter = Builders<TechnicianEarnings>.Filter.Eq(e => e.TechnicianId, technicianId);
            return await _earnings.Find(filter).ToListAsync();
        }

        public async Task<List<TechnicianEarnings>> GetUnpaidAsync()
        {
            var filter = Builders<TechnicianEarnings>.Filter.Eq(e => e.IsPaid, false);
            return await _earnings.Find(filter).ToListAsync();
        }

        public async Task<List<TechnicianEarnings>> GetAllAsync()
        {
            return await _earnings.Find(_ => true).ToListAsync();
        }

        public async Task AddAsync(TechnicianEarnings earnings)
        {
            await _earnings.InsertOneAsync(earnings);
        }

        public async Task UpdateAsync(TechnicianEarnings earnings)
        {
            var filter = Builders<TechnicianEarnings>.Filter.Eq(e => e.EarningId, earnings.EarningId);
            await _earnings.ReplaceOneAsync(filter, earnings);
        }

        public async Task<int> GetNextEarningIdAsync()
        {
            var all = await GetAllAsync();
            return all.Any() ? all.Max(e => e.EarningId) + 1 : 1;
        }
    }

    public class SystemSettingsRepository : ISystemSettingsRepository
    {
        private readonly IMongoCollection<SystemSettings> _settings;

        public SystemSettingsRepository(MongoDbContext context)
        {
            _settings = context.SystemSettings;
        }

        public async Task<SystemSettings> GetByKeyAsync(string key)
        {
            var filter = Builders<SystemSettings>.Filter.Eq(s => s.SettingKey, key);
            return await _settings.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<List<SystemSettings>> GetAllAsync()
        {
            return await _settings.Find(_ => true).ToListAsync();
        }

        public async Task AddAsync(SystemSettings setting)
        {
            await _settings.InsertOneAsync(setting);
        }

        public async Task UpdateAsync(SystemSettings setting)
        {
            var filter = Builders<SystemSettings>.Filter.Eq(s => s.SettingId, setting.SettingId);
            await _settings.ReplaceOneAsync(filter, setting);
        }

        public async Task<int> GetNextSettingIdAsync()
        {
            var all = await GetAllAsync();
            return all.Any() ? all.Max(s => s.SettingId) + 1 : 1;
        }
    }
}
