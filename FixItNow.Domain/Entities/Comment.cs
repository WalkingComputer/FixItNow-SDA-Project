using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace FixItNow.Domain.Entities
{
    public class Comment
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("commentId")]
        public int CommentId { get; set; }

        [BsonElement("ticketId")]
        public int TicketId { get; set; }

        [BsonElement("authorUserId")]
        public int AuthorUserId { get; set; }

        [BsonElement("commentText")]
        public string CommentText { get; set; }

        [BsonElement("isInternal")]
        public bool IsInternal { get; set; } = false;

        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; }
    }
}