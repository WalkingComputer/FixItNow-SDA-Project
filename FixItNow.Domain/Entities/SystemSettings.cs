using MongoDB.Bson.Serialization.Attributes;
using System;

namespace FixItNow.Domain.Entities
{
    public class SystemSettings
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("settingId")]
        public int SettingId { get; set; }

        [BsonElement("settingKey")]
        public string SettingKey { get; set; }

        [BsonElement("settingValue")]
        public string SettingValue { get; set; }

        [BsonElement("description")]
        public string Description { get; set; }

        [BsonElement("lastModified")]
        public DateTime LastModified { get; set; }

        [BsonElement("modifiedBy")]
        public int ModifiedBy { get; set; }
    }
}
