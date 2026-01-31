using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace FixItNow.Domain.Entities
{
    public class Attachment
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("attachmentId")]
        public int AttachmentId { get; set; }

        [BsonElement("ticketId")]
        public int TicketId { get; set; }

        [BsonElement("uploadedByUserId")]
        public int UploadedByUserId { get; set; }

        [BsonElement("fileName")]
        public string FileName { get; set; }

        [BsonElement("filePathOrUrl")]
        public string FilePathOrUrl { get; set; }

        [BsonElement("uploadedAt")]
        public DateTime UploadedAt { get; set; }
    }
}