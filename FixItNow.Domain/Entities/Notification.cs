using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace FixItNow.Domain.Entities
{
    public class Notification
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("notificationId")]
        public int NotificationId { get; set; }

        [BsonElement("userId")]
        public int UserId { get; set; }

        [BsonElement("ticketId")]
        public int? TicketId { get; set; }

        [BsonElement("type")]
        public string Type { get; set; }

        [BsonElement("message")]
        public string Message { get; set; }

        [BsonElement("isRead")]
        public bool IsRead { get; set; } = false;

        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; }
    }
}