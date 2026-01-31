using MongoDB.Bson.Serialization.Attributes;
using System;

namespace FixItNow.Domain.Entities
{
    /// <summary>
    /// ?? DUMMY TRANSACTION - FOR DEMONSTRATION ONLY
    /// </summary>
    public class Transaction
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("transactionId")]
        public int TransactionId { get; set; }

        [BsonElement("walletId")]
        public int WalletId { get; set; }

        [BsonElement("amount")]
        public decimal Amount { get; set; }

        [BsonElement("transactionType")]
        public string TransactionType { get; set; } // Credit, Debit

        [BsonElement("description")]
        public string Description { get; set; }

        [BsonElement("referenceId")]
        public string ReferenceId { get; set; }

        [BsonElement("transactionDate")]
        public DateTime TransactionDate { get; set; }

        [BsonElement("isSimulated")]
        public bool IsSimulated { get; set; } = true; // MOCK TRANSACTION
    }
}
