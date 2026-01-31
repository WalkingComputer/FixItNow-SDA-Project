using MongoDB.Bson.Serialization.Attributes;
using System;

namespace FixItNow.Domain.Entities
{
    /// <summary>
    /// ?? DUMMY WALLET - SIMULATED BALANCE ONLY
    /// </summary>
    public class Wallet
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("walletId")]
        public int WalletId { get; set; }

        [BsonElement("userId")]
        public int UserId { get; set; }

        [BsonElement("balance")]
        public decimal Balance { get; set; } = 5000; // Default dummy balance

        [BsonElement("lastUpdated")]
        public DateTime LastUpdated { get; set; }

        [BsonElement("isDummy")]
        public bool IsDummy { get; set; } = true; // ? ALWAYS TRUE
    }
}
