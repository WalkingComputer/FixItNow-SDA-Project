using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using FixItNow.Domain.Entities;
using FixItNow.Domain.Interfaces;
using FixItNow.Infrastructure.Data;

namespace FixItNow.Infrastructure.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly IMongoCollection<Notification> _notifications;

        public NotificationRepository(MongoDbContext context)
        {
            _notifications = context.Notifications;
        }

        public async Task AddAsync(Notification notification)
        {
            await _notifications.InsertOneAsync(notification);
        }

        public async Task<List<Notification>> FindUnreadByUserAsync(int userId)
        {
            var filter = Builders<Notification>.Filter.And(
                Builders<Notification>.Filter.Eq(n => n.UserId, userId),
                Builders<Notification>.Filter.Eq(n => n.IsRead, false)
            );
            return await _notifications.Find(filter)
                .SortByDescending(n => n.CreatedAt)
                .ToListAsync();
        }

        public async Task MarkReadAsync(int notificationId)
        {
            var filter = Builders<Notification>.Filter.Eq(n => n.NotificationId, notificationId);
            var update = Builders<Notification>.Update.Set(n => n.IsRead, true);
            await _notifications.UpdateOneAsync(filter, update);
        }
    }
}