using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace FixItNow.Domain.Entities
{
    public class TicketAssignment
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("assignmentId")]
        public int AssignmentId { get; set; }

        [BsonElement("ticketId")]
        public int TicketId { get; set; }

        [BsonElement("technicianId")]
        public int TechnicianId { get; set; }

        [BsonElement("assignedByAdminId")]
        public int AssignedByAdminId { get; set; }

        [BsonElement("assignedAt")]
        public DateTime AssignedAt { get; set; }

        [BsonElement("unassignedAt")]
        public DateTime? UnassignedAt { get; set; }

        [BsonElement("isActive")]
        public bool IsActive { get; set; } = true;
    }
}