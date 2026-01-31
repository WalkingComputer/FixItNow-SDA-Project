using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FixItNow.Domain.Entities
{
    public class Location
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("locationId")]
        public int LocationId { get; set; }

        [BsonElement("hostelName")]
        public string HostelName { get; set; }

        [BsonElement("block")]
        public string Block { get; set; }

        [BsonElement("floor")]
        public string Floor { get; set; }

        [BsonElement("roomNumber")]
        public string RoomNumber { get; set; }

        [BsonElement("notes")]
        public string Notes { get; set; }
    }
}