using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace FixItNow.Domain.Entities
{
    public class Feedback
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("feedbackId")]
        public int FeedbackId { get; set; }

        [BsonElement("ticketId")]
        public int TicketId { get; set; }

        [BsonElement("residentUserId")]
        public int ResidentUserId { get; set; }

        [BsonElement("rating")]
        public int Rating { get; set; }

        [BsonElement("feedbackText")]
        public string FeedbackText { get; set; }

        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; }
    }
}