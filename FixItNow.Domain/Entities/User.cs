using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace FixItNow.Domain.Entities
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("userId")]
        public int UserId { get; set; }

        [BsonElement("roleId")]
        public int RoleId { get; set; }

        [BsonElement("fullName")]
        public string FullName { get; set; }

        [BsonElement("username")]
        public string Username { get; set; }

        [BsonElement("email")]
        public string Email { get; set; }

        [BsonElement("passwordHash")]
        public string PasswordHash { get; set; }

        [BsonElement("phone")]
        public string Phone { get; set; }

        [BsonElement("isActive")]
        public bool IsActive { get; set; } = true;

        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; }

        // ? NEW FIELDS FOR TECHNICIAN RATING SYSTEM
        [BsonElement("averageRating")]
        public double AverageRating { get; set; } = 0.0;

        [BsonElement("totalRatings")]
        public int TotalRatings { get; set; } = 0;

        [BsonElement("completedTickets")]
        public int CompletedTickets { get; set; } = 0;

        [BsonElement("specialization")]
        public string Specialization { get; set; }
    }
}