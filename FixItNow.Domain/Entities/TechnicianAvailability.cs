using MongoDB.Bson.Serialization.Attributes;
using System;

namespace FixItNow.Domain.Entities
{
    public class TechnicianAvailability
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("availabilityId")]
        public int AvailabilityId { get; set; }

        [BsonElement("technicianId")]
        public int TechnicianId { get; set; }

        [BsonElement("isAvailable")]
        public bool IsAvailable { get; set; }

        [BsonElement("availableFrom")]
        public DateTime? AvailableFrom { get; set; }

        [BsonElement("availableTo")]
        public DateTime? AvailableTo { get; set; }

        [BsonElement("reason")]
        public string Reason { get; set; }

        [BsonElement("lastUpdated")]
        public DateTime LastUpdated { get; set; }
    }
}
