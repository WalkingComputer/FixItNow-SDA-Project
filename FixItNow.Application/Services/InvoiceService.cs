using FixItNow.Domain.Entities;
using FixItNow.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FixItNow.Application.Services
{
    /// <summary>
    /// ?? DUMMY INVOICE SERVICE - FOR DEMONSTRATION ONLY
    /// Generates simulated invoices
    /// </summary>
    public interface IInvoiceService
    {
        Task<Invoice> GenerateInvoiceAsync(int ticketId);
        Task<Invoice> GetInvoiceByTicketIdAsync(int ticketId);
        Task<List<Invoice>> GetResidentInvoicesAsync(int residentId);
        Task<List<Invoice>> GetUnpaidInvoicesAsync();
        Task<List<Invoice>> GetAllInvoicesAsync();
        Task<string> GenerateInvoicePDFAsync(int invoiceId);
    }

    public class InvoiceService : IInvoiceService
    {
        private readonly IUnitOfWork _unitOfWork;

        public InvoiceService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// ? Generate dummy invoice with simulated charges
        /// </summary>
        public async Task<Invoice> GenerateInvoiceAsync(int ticketId)
        {
            var existingInvoice = await _unitOfWork.Invoices.GetByTicketIdAsync(ticketId);
            if (existingInvoice != null)
                return existingInvoice;

            var ticket = await _unitOfWork.Tickets.GetByIdAsync(ticketId);
            if (ticket == null)
                throw new Exception("Ticket not found");

            // Get category and priority names
            string categoryName = GetCategoryName(ticket.CategoryId);
            string priorityName = GetPriorityName(ticket.PriorityId);

            // ? SIMULATED SERVICE CHARGE CALCULATION
            decimal baseCharge = GetBaseCharge(ticket.CategoryId);
            decimal urgentSurcharge = ticket.PriorityId == 4 ? 500 : 0; // Urgent surcharge
            decimal subTotal = baseCharge + urgentSurcharge;
            decimal taxPercentage = 16;
            decimal taxAmount = subTotal * (taxPercentage / 100);
            decimal totalAmount = subTotal + taxAmount;

            // Generate invoice ID
            var allInvoices = await _unitOfWork.Invoices.GetAllAsync();
            var invoiceId = allInvoices.Any() ? allInvoices.Max(i => i.InvoiceId) + 1 : 1;

            var invoice = new Invoice
            {
                InvoiceId = invoiceId,
                InvoiceNumber = $"INV-{invoiceId:D5}",
                TicketId = ticketId,
                ResidentId = ticket.CreatedByUserId,
                CategoryName = categoryName,
                PriorityName = priorityName,
                SubTotal = subTotal,
                TaxAmount = taxAmount,
                TotalAmount = totalAmount,
                IssuedAt = DateTime.Now,
                DueDate = DateTime.Now.AddDays(7),
                IsPaid = false,
                Notes = "** SIMULATED INVOICE FOR DEMONSTRATION **"
            };

            await _unitOfWork.Invoices.AddAsync(invoice);
            return invoice;
        }

        public async Task<Invoice> GetInvoiceByTicketIdAsync(int ticketId)
        {
            return await _unitOfWork.Invoices.GetByTicketIdAsync(ticketId);
        }

        public async Task<List<Invoice>> GetResidentInvoicesAsync(int residentId)
        {
            return await _unitOfWork.Invoices.GetByResidentIdAsync(residentId);
        }

        public async Task<List<Invoice>> GetUnpaidInvoicesAsync()
        {
            return await _unitOfWork.Invoices.GetUnpaidAsync();
        }

        public async Task<List<Invoice>> GetAllInvoicesAsync()
        {
            return await _unitOfWork.Invoices.GetAllAsync();
        }

        /// <summary>
        /// ? Generate text-format invoice (simulated PDF)
        /// </summary>
        public async Task<string> GenerateInvoicePDFAsync(int invoiceId)
        {
            var invoice = await _unitOfWork.Invoices.GetByIdAsync(invoiceId);
            if (invoice == null)
                throw new Exception("Invoice not found");

            var resident = await _unitOfWork.Users.GetByIdAsync(invoice.ResidentId);
            var ticket = await _unitOfWork.Tickets.GetByIdAsync(invoice.TicketId);

            var pdf = $@"
???????????????????????????????????????????????????????
                    FixItNow INVOICE
             ** SIMULATED - FOR DEMO ONLY **
???????????????????????????????????????????????????????

Invoice Number:  {invoice.InvoiceNumber}
Issue Date:      {invoice.IssuedAt:yyyy-MM-dd}
Due Date:        {invoice.DueDate:yyyy-MM-dd}
Status:          {(invoice.IsPaid ? "? PAID" : "? UNPAID")}

???????????????????????????????????????????????????????
Bill To:
???????????????????????????????????????????????????????
Name:            {resident.FullName}
User ID:         {resident.UserId}
Email:           {resident.Email}
Phone:           {resident.Phone}

???????????????????????????????????????????????????????
Service Details:
???????????????????????????????????????????????????????
Ticket ID:       {invoice.TicketId}
Ticket Code:     {ticket.TicketCode}
Category:        {invoice.CategoryName}
Priority:        {invoice.PriorityName}
Description:     {ticket.Title}

???????????????????????????????????????????????????????
Charges:
???????????????????????????????????????????????????????
Service Charge:  PKR {invoice.SubTotal:N2}
Tax (16%):       PKR {invoice.TaxAmount:N2}
???????????????????????????????????????????????????????
TOTAL AMOUNT:    PKR {invoice.TotalAmount:N2}
???????????????????????????????????????????????????????

Payment Status:  {(invoice.IsPaid ? $"? PAID on {invoice.PaidAt:yyyy-MM-dd}" : "? PENDING")}

{invoice.Notes}

???????????????????????????????????????????????????????
         This is a simulated invoice
         ** NO REAL CHARGES **
???????????????????????????????????????????????????????
";
            return pdf;
        }

        private string GetCategoryName(int categoryId)
        {
            switch (categoryId)
            {
                case 1: return "Plumbing";
                case 2: return "Electric";
                case 3: return "WiFi";
                case 4: return "Furniture";
                case 5: return "Other";
                default: return "Unknown";
            }
        }

        private string GetPriorityName(int priorityId)
        {
            switch (priorityId)
            {
                case 1: return "Low";
                case 2: return "Medium";
                case 3: return "High";
                case 4: return "Urgent";
                default: return "Unknown";
            }
        }

        private decimal GetBaseCharge(int categoryId)
        {
            switch (categoryId)
            {
                case 1: return 500;   // Plumbing
                case 2: return 600;   // Electric
                case 3: return 400;   // WiFi
                case 4: return 450;   // Furniture
                case 5: return 300;   // Other
                default: return 400;
            }
        }
    }
}
