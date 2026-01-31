using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FixItNow.Domain.Entities
{
    public class Category
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("categoryId")]
        public int CategoryId { get; set; }

        [BsonElement("categoryName")]
        public string CategoryName { get; set; }

        [BsonElement("isActive")]
        public bool IsActive { get; set; } = true;
    }
}