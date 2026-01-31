using MongoDB.Bson.Serialization.Attributes;
using System;

namespace FixItNow.Domain.Entities
{
    public class Invoice
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("invoiceId")]
        public int InvoiceId { get; set; }

        [BsonElement("invoiceNumber")]
        public string InvoiceNumber { get; set; }

        [BsonElement("ticketId")]
        public int TicketId { get; set; }

        [BsonElement("residentId")]
        public int ResidentId { get; set; }

        [BsonElement("categoryName")]
        public string CategoryName { get; set; }

        [BsonElement("priorityName")]
        public string PriorityName { get; set; }

        [BsonElement("subTotal")]
        public decimal SubTotal { get; set; }

        [BsonElement("taxAmount")]
        public decimal TaxAmount { get; set; }

        [BsonElement("totalAmount")]
        public decimal TotalAmount { get; set; }

        [BsonElement("issuedAt")]
        public DateTime IssuedAt { get; set; }

        [BsonElement("dueDate")]
        public DateTime? DueDate { get; set; }

        [BsonElement("isPaid")]
        public bool IsPaid { get; set; }

        [BsonElement("paidAt")]
        public DateTime? PaidAt { get; set; }

        [BsonElement("notes")]
        public string Notes { get; set; } = "** SIMULATED INVOICE FOR DEMONSTRATION **";
    }
}
