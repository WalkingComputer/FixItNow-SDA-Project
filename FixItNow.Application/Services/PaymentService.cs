using FixItNow.Domain.Entities;
using FixItNow.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FixItNow.Application.Services
{
    /// <summary>
    /// ?? DUMMY PAYMENT SERVICE - FOR DEMONSTRATION ONLY
    /// All transactions are SIMULATED - NO REAL MONEY
    /// </summary>
    public interface IPaymentService
    {
        Task<Payment> ProcessPaymentAsync(int ticketId, int paymentMethodId, string transactionId = null);
        Task<Payment> ProcessWalletPaymentAsync(int ticketId, int residentId);
        Task<Payment> GetPaymentByTicketIdAsync(int ticketId);
        Task<List<Payment>> GetPaymentHistoryAsync(int residentId);
        Task<string> GenerateReceiptAsync(int paymentId);
        Task<Payment> RefundPaymentAsync(int paymentId, string reason);
    }

    public class PaymentService : IPaymentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IInvoiceService _invoiceService;
        private readonly IWalletService _walletService;

        public PaymentService(IUnitOfWork unitOfWork, IInvoiceService invoiceService, IWalletService walletService)
        {
            _unitOfWork = unitOfWork;
            _invoiceService = invoiceService;
            _walletService = walletService;
        }

        /// <summary>
        /// ? SIMULATE PAYMENT - NO REAL TRANSACTION
        /// </summary>
        public async Task<Payment> ProcessPaymentAsync(int ticketId, int paymentMethodId, string transactionId = null)
        {
            var invoice = await _unitOfWork.Invoices.GetByTicketIdAsync(ticketId);
            if (invoice == null)
                throw new Exception("Invoice not found");

            if (invoice.IsPaid)
                throw new Exception("Invoice already paid");

            // Generate payment ID
            var allPayments = await _unitOfWork.Payments.GetAllAsync();
            var paymentId = allPayments.Any() ? allPayments.Max(p => p.PaymentId) + 1 : 1;

            var paymentMethod = await _unitOfWork.PaymentMethods.GetByIdAsync(paymentMethodId);

            // ? SIMULATE SUCCESS - ALWAYS SUCCEEDS FOR DEMO
            var payment = new Payment
            {
                PaymentId = paymentId,
                PaymentCode = $"PAY-{paymentId:D5}",
                TicketId = ticketId,
                ResidentId = invoice.ResidentId,
                Amount = invoice.TotalAmount,
                PaymentMethodId = paymentMethodId,
                PaymentStatusId = 2, // Paid (SIMULATED)
                PaidAt = DateTime.Now,
                TransactionId = transactionId ?? $"TXN-DUMMY-{Guid.NewGuid().ToString().Substring(0, 8).ToUpper()}",
                PaymentGateway = paymentMethod?.MethodName ?? "Unknown",
                ReceiptNumber = $"REC-{paymentId:D5}",
                IsDummy = true, // MARK AS DUMMY
                CreatedAt = DateTime.Now
            };

            await _unitOfWork.Payments.AddAsync(payment);

            // Update invoice
            invoice.IsPaid = true;
            invoice.PaidAt = DateTime.Now;
            await _unitOfWork.Invoices.UpdateAsync(invoice);

            // Update ticket status
            var ticket = await _unitOfWork.Tickets.GetByIdAsync(ticketId);
            if (ticket != null && ticket.StatusId == 1) // If pending
            {
                ticket.StatusId = 2; // Mark as Assigned
                await _unitOfWork.Tickets.UpdateAsync(ticket);
            }

            // Create notification
            await CreatePaymentNotificationAsync(payment);

            return payment;
        }

        /// <summary>
        /// ? SIMULATE WALLET PAYMENT
        /// </summary>
        public async Task<Payment> ProcessWalletPaymentAsync(int ticketId, int residentId)
        {
            var invoice = await _unitOfWork.Invoices.GetByTicketIdAsync(ticketId);
            if (invoice == null)
                throw new Exception("Invoice not found");

            var wallet = await _walletService.GetWalletAsync(residentId);

            // Check balance
            if (wallet.Balance < invoice.TotalAmount)
            {
                throw new Exception($"Insufficient balance. Required: PKR {invoice.TotalAmount:N2}, Available: PKR {wallet.Balance:N2}");
            }

            // Deduct from wallet
            await _walletService.DeductMoneyAsync(residentId, invoice.TotalAmount, $"Payment for ticket {ticketId}");

            // Process payment
            return await ProcessPaymentAsync(ticketId, 5); // 5 = Wallet
        }

        public async Task<Payment> GetPaymentByTicketIdAsync(int ticketId)
        {
            return await _unitOfWork.Payments.GetByTicketIdAsync(ticketId);
        }

        public async Task<List<Payment>> GetPaymentHistoryAsync(int residentId)
        {
            return await _unitOfWork.Payments.GetByResidentIdAsync(residentId);
        }

        /// <summary>
        /// ? GENERATE DUMMY RECEIPT (TEXT FORMAT)
        /// </summary>
        public async Task<string> GenerateReceiptAsync(int paymentId)
        {
            var payment = await _unitOfWork.Payments.GetByIdAsync(paymentId);
            if (payment == null)
                throw new Exception("Payment not found");

            var invoice = await _unitOfWork.Invoices.GetByTicketIdAsync(payment.TicketId);
            var resident = await _unitOfWork.Users.GetByIdAsync(payment.ResidentId);

            var receipt = $@"
???????????????????????????????????????????????????????
               FixItNow PAYMENT RECEIPT
               ** SIMULATED - NOT REAL **
???????????????????????????????????????????????????????

Receipt Number: {payment.ReceiptNumber}
Payment Code:   {payment.PaymentCode}
Transaction ID: {payment.TransactionId}

Date & Time:    {payment.PaidAt:yyyy-MM-dd HH:mm:ss}
Payment Method: {payment.PaymentGateway}

???????????????????????????????????????????????????????
Customer Details:
???????????????????????????????????????????????????????
Name:           {resident.FullName}
User ID:        {resident.UserId}

???????????????????????????????????????????????????????
Invoice Details:
???????????????????????????????????????????????????????
Invoice Number: {invoice.InvoiceNumber}
Ticket ID:      {payment.TicketId}
Category:       {invoice.CategoryName}
Priority:       {invoice.PriorityName}

???????????????????????????????????????????????????????
Payment Breakdown:
???????????????????????????????????????????????????????
Sub Total:      PKR {invoice.SubTotal:N2}
Tax (16%):      PKR {invoice.TaxAmount:N2}
???????????????????????????????????????????????????????
TOTAL PAID:     PKR {payment.Amount:N2}
???????????????????????????????????????????????????????

Status:         ? PAID (SIMULATED)
Payment Date:   {payment.PaidAt:yyyy-MM-dd}

???????????????????????????????????????????????????????
         Thank you for using FixItNow!
         ** THIS IS A DEMO RECEIPT **
???????????????????????????????????????????????????????
";
            return receipt;
        }

        /// <summary>
        /// ? SIMULATE REFUND
        /// </summary>
        public async Task<Payment> RefundPaymentAsync(int paymentId, string reason)
        {
            var payment = await _unitOfWork.Payments.GetByIdAsync(paymentId);
            if (payment == null)
                throw new Exception("Payment not found");

            if (payment.PaymentStatusId == 4)
                throw new Exception("Payment already refunded");

            // Mark as refunded
            payment.PaymentStatusId = 4;
            await _unitOfWork.Payments.UpdateAsync(payment);

            // Update invoice
            var invoice = await _unitOfWork.Invoices.GetByTicketIdAsync(payment.TicketId);
            invoice.IsPaid = false;
            invoice.PaidAt = null;
            invoice.Notes = $"Refunded: {reason}";
            await _unitOfWork.Invoices.UpdateAsync(invoice);

            // If wallet payment, credit back
            if (payment.PaymentMethodId == 5)
            {
                await _walletService.AddMoneyAsync(payment.ResidentId, payment.Amount,
                    $"Refund for ticket {payment.TicketId}: {reason}");
            }

            return payment;
        }

        private async Task CreatePaymentNotificationAsync(Payment payment)
        {
            var notification = new Notification
            {
                NotificationId = 0,
                UserId = payment.ResidentId,
                TicketId = payment.TicketId,
                Message = $"Payment successful! Receipt: {payment.ReceiptNumber}. Amount: PKR {payment.Amount:N2} (SIMULATED)",
                CreatedAt = DateTime.Now,
                IsRead = false
            };

            await _unitOfWork.Notifications.AddAsync(notification);
        }
    }
}
