using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FixItNow.Domain.Entities
{
    public class Role
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("roleId")]
        public int RoleId { get; set; }

        [BsonElement("roleName")]
        public string RoleName { get; set; }
    }
}