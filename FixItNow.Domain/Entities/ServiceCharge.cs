using MongoDB.Bson.Serialization.Attributes;

namespace FixItNow.Domain.Entities
{
    public class ServiceCharge
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("serviceChargeId")]
        public int ServiceChargeId { get; set; }

        [BsonElement("categoryId")]
        public int CategoryId { get; set; }

        [BsonElement("priorityId")]
        public int PriorityId { get; set; }

        [BsonElement("baseCharge")]
        public decimal BaseCharge { get; set; }

        [BsonElement("urgentSurcharge")]
        public decimal UrgentSurcharge { get; set; }

        [BsonElement("taxPercentage")]
        public decimal TaxPercentage { get; set; }
    }
}
