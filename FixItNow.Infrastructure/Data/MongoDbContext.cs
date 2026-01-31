using MongoDB.Driver;
using FixItNow.Domain.Entities;

namespace FixItNow.Infrastructure.Data
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(MongoDbSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            _database = client.GetDatabase(settings.DatabaseName);
        }

        public IMongoCollection<User> Users => _database.GetCollection<User>("Users");
        public IMongoCollection<Role> Roles => _database.GetCollection<Role>("Roles");
        public IMongoCollection<Ticket> Tickets => _database.GetCollection<Ticket>("Tickets");
        public IMongoCollection<TicketAssignment> TicketAssignments => _database.GetCollection<TicketAssignment>("TicketAssignments");
        public IMongoCollection<Category> Categories => _database.GetCollection<Category>("Categories");
        public IMongoCollection<Priority> Priorities => _database.GetCollection<Priority>("Priorities");
        public IMongoCollection<TicketStatus> TicketStatuses => _database.GetCollection<TicketStatus>("TicketStatuses");
        public IMongoCollection<Location> Locations => _database.GetCollection<Location>("Locations");
        public IMongoCollection<Comment> Comments => _database.GetCollection<Comment>("Comments");
        public IMongoCollection<Attachment> Attachments => _database.GetCollection<Attachment>("Attachments");
        public IMongoCollection<Notification> Notifications => _database.GetCollection<Notification>("Notifications");
        public IMongoCollection<Feedback> Feedbacks => _database.GetCollection<Feedback>("Feedbacks");
        public IMongoCollection<AuditLog> AuditLogs => _database.GetCollection<AuditLog>("AuditLogs");
    }
}