using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FixItNow.Domain.Entities
{
    public class TicketStatus
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("statusId")]
        public int StatusId { get; set; }

        [BsonElement("statusName")]
        public string StatusName { get; set; }
    }
}