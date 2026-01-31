using MongoDB.Bson.Serialization.Attributes;

namespace FixItNow.Domain.Entities
{
    public class PaymentStatus
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("paymentStatusId")]
        public int PaymentStatusId { get; set; }

        [BsonElement("statusName")]
        public string StatusName { get; set; } // Pending, Paid, Failed, Refunded
    }
}
