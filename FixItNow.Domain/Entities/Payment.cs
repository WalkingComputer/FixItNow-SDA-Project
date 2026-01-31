using MongoDB.Bson.Serialization.Attributes;
using System;

namespace FixItNow.Domain.Entities
{
    /// <summary>
    /// ?? DUMMY PAYMENT - FOR DEMONSTRATION ONLY
    /// No real transactions are processed
    /// </summary>
    public class Payment
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("paymentId")]
        public int PaymentId { get; set; }

        [BsonElement("paymentCode")]
        public string PaymentCode { get; set; } // PAY-00001

        [BsonElement("ticketId")]
        public int TicketId { get; set; }

        [BsonElement("residentId")]
        public int ResidentId { get; set; }

        [BsonElement("amount")]
        public decimal Amount { get; set; }

        [BsonElement("paymentMethodId")]
        public int PaymentMethodId { get; set; }

        [BsonElement("paymentStatusId")]
        public int PaymentStatusId { get; set; }

        [BsonElement("paidAt")]
        public DateTime? PaidAt { get; set; }

        [BsonElement("transactionId")]
        public string TransactionId { get; set; } // DUMMY TXN ID

        [BsonElement("paymentGateway")]
        public string PaymentGateway { get; set; } // Mock: JazzCash, EasyPaisa, etc.

        [BsonElement("receiptNumber")]
        public string ReceiptNumber { get; set; }

        [BsonElement("isDummy")]
        public bool IsDummy { get; set; } = true; // ? ALWAYS TRUE - SIMULATED

        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; }
    }
}
