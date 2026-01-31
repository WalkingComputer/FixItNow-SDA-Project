using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FixItNow.Domain.Entities
{
    public class Priority
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("priorityId")]
        public int PriorityId { get; set; }

        [BsonElement("priorityName")]
        public string PriorityName { get; set; }

        [BsonElement("priorityOrder")]
        public int PriorityOrder { get; set; }
    }
}