using MongoDB.Bson.Serialization.Attributes;
using System;

namespace FixItNow.Domain.Entities
{
    public class ChatMessage
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("messageId")]
        public int MessageId { get; set; }

        [BsonElement("ticketId")]
        public int TicketId { get; set; }

        [BsonElement("senderId")]
        public int SenderId { get; set; }

        [BsonElement("senderName")]
        public string SenderName { get; set; }

        [BsonElement("receiverId")]
        public int ReceiverId { get; set; }

        [BsonElement("messageText")]
        public string MessageText { get; set; }

        [BsonElement("sentAt")]
        public DateTime SentAt { get; set; }

        [BsonElement("isRead")]
        public bool IsRead { get; set; }

        [BsonElement("isSimulated")]
        public bool IsSimulated { get; set; } = true;
    }
}
