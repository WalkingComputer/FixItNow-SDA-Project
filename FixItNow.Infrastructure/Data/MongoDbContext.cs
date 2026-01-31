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
        
        // ? NEW - Payment System Collections
        public IMongoCollection<Payment> Payments => _database.GetCollection<Payment>("Payments");
        public IMongoCollection<Invoice> Invoices => _database.GetCollection<Invoice>("Invoices");
        public IMongoCollection<Wallet> Wallets => _database.GetCollection<Wallet>("Wallets");
        public IMongoCollection<Transaction> Transactions => _database.GetCollection<Transaction>("Transactions");
        public IMongoCollection<ServiceCharge> ServiceCharges => _database.GetCollection<ServiceCharge>("ServiceCharges");
        public IMongoCollection<PaymentMethod> PaymentMethods => _database.GetCollection<PaymentMethod>("PaymentMethods");
        public IMongoCollection<PaymentStatus> PaymentStatuses => _database.GetCollection<PaymentStatus>("PaymentStatuses");
        
        // ? NEW Collections
        public IMongoCollection<ChatMessage> ChatMessages => _database.GetCollection<ChatMessage>("ChatMessages");
        public IMongoCollection<TechnicianAvailability> TechnicianAvailabilities => _database.GetCollection<TechnicianAvailability>("TechnicianAvailabilities");
        public IMongoCollection<TechnicianEarnings> TechnicianEarnings => _database.GetCollection<TechnicianEarnings>("TechnicianEarnings");
        public IMongoCollection<SystemSettings> SystemSettings => _database.GetCollection<SystemSettings>("SystemSettings");
    }
}