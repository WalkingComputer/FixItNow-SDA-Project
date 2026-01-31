using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace FixItNow.Domain.Entities
{
    public class AuditLog
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("auditId")]
        public int AuditId { get; set; }

        [BsonElement("ticketId")]
        public int? TicketId { get; set; }

        [BsonElement("entityType")]
        public string EntityType { get; set; }

        [BsonElement("entityId")]
        public int? EntityId { get; set; }

        [BsonElement("action")]
        public string Action { get; set; }

        [BsonElement("details")]
        public string Details { get; set; }

        [BsonElement("performedByUserId")]
        public int PerformedByUserId { get; set; }

        [BsonElement("performedAt")]
        public DateTime PerformedAt { get; set; }
    }
}