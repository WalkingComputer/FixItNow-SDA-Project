using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace FixItNow.Domain.Entities
{
    public class Ticket
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("ticketId")]
        public int TicketId { get; set; }

        [BsonElement("ticketCode")]
        public string TicketCode { get; set; }

        [BsonElement("title")]
        public string Title { get; set; }

        [BsonElement("description")]
        public string Description { get; set; }

        [BsonElement("categoryId")]
        public int CategoryId { get; set; }

        [BsonElement("locationId")]
        public int LocationId { get; set; }

        [BsonElement("priorityId")]
        public int PriorityId { get; set; }

        [BsonElement("statusId")]
        public int StatusId { get; set; }

        [BsonElement("createdByUserId")]
        public int CreatedByUserId { get; set; }

        [BsonElement("currentTechnicianId")]
        public int? CurrentTechnicianId { get; set; }

        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; }

        [BsonElement("updatedAt")]
        public DateTime? UpdatedAt { get; set; }

        [BsonElement("resolvedAt")]
        public DateTime? ResolvedAt { get; set; }

        [BsonElement("closedAt")]
        public DateTime? ClosedAt { get; set; }

        [BsonElement("reopenedAt")]
        public DateTime? ReopenedAt { get; set; }

        // ? NEW FIELDS FOR TECHNICIAN RATING SYSTEM
        [BsonElement("selectedTechnicianId")]
        public int? SelectedTechnicianId { get; set; }

        [BsonElement("technicianRatingGiven")]
        public int? TechnicianRatingGiven { get; set; }

        [BsonElement("userReview")]
        public string UserReview { get; set; }
    }
}