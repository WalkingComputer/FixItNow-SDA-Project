using MongoDB.Bson.Serialization.Attributes;
using System;

namespace FixItNow.Domain.Entities
{
    public class TechnicianEarnings
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("earningId")]
        public int EarningId { get; set; }

        [BsonElement("technicianId")]
        public int TechnicianId { get; set; }

        [BsonElement("ticketId")]
        public int TicketId { get; set; }

        [BsonElement("amount")]
        public decimal Amount { get; set; }

        [BsonElement("earnedAt")]
        public DateTime EarnedAt { get; set; }

        [BsonElement("isPaid")]
        public bool IsPaid { get; set; }

        [BsonElement("paidAt")]
        public DateTime? PaidAt { get; set; }

        [BsonElement("isDummy")]
        public bool IsDummy { get; set; } = true;
    }
}
