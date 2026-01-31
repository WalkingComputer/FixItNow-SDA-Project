using MongoDB.Bson.Serialization.Attributes;

namespace FixItNow.Domain.Entities
{
    public class PaymentMethod
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("paymentMethodId")]
        public int PaymentMethodId { get; set; }

        [BsonElement("methodName")]
        public string MethodName { get; set; } // Mock: Cash, Card, JazzCash, EasyPaisa, Wallet

        [BsonElement("isActive")]
        public bool IsActive { get; set; }

        [BsonElement("isMock")]
        public bool IsMock { get; set; } = true; // SIMULATED ONLY

        [BsonElement("description")]
        public string Description { get; set; }
    }
}
