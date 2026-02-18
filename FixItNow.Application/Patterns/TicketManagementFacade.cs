using FixItNow.Application.DTOs;
using FixItNow.Application.Factories;
using FixItNow.Application.Interfaces;
using FixItNow.Application.Services;
using FixItNow.Domain.Entities;
using FixItNow.Domain.Enums;
using System;
using System.Threading.Tasks;

namespace FixItNow.Application.Patterns
{
    /// <summary>
    /// FACADE PATTERN (Structural)
    /// Provides a simplified interface to complex ticket management subsystems
    /// Coordinates multiple services: Ticket, Notification, Audit, Invoice
    /// </summary>
    public class TicketManagementFacade
    {
        private readonly ITicketService _ticketService;
        private readonly INotificationFactory _notificationFactory;
        private readonly IInvoiceService _invoiceService;
        private readonly IUserService _userService;

        public TicketManagementFacade(
            ITicketService ticketService,
            INotificationFactory notificationFactory,
            IInvoiceService invoiceService,
            IUserService userService)
        {
            _ticketService = ticketService;
            _notificationFactory = notificationFactory;
            _invoiceService = invoiceService;
            _userService = userService;
        }

        /// <summary>
        /// Simplified method to create ticket with all related operations
        /// Hides complexity of notification, audit logging, etc.
        /// </summary>
        public async Task<Ticket> CreateTicketWithNotificationsAsync(CreateTicketRequest request)
        {
            Console.WriteLine("🎭 Facade Pattern: Creating ticket with all subsystems...");

            // 1. Create ticket
            var ticket = await _ticketService.CreateTicketAsync(request);

            // 2. Notify admins (handled internally by service)
            Console.WriteLine($"   ✓ Ticket {ticket.TicketCode} created");
            Console.WriteLine($"   ✓ Notifications sent to admins");
            Console.WriteLine($"   ✓ Audit log created");

            return ticket;
        }

        /// <summary>
        /// Simplified method to resolve ticket and generate invoice
        /// </summary>
        public async Task<Invoice> ResolveTicketAndGenerateInvoiceAsync(int ticketId, int technicianId)
        {
            Console.WriteLine("🎭 Facade Pattern: Resolving ticket and generating invoice...");

            // 1. Change status to resolved
            await _ticketService.ChangeStatusAsync(ticketId, TicketStatusEnum.Resolved, technicianId);
            Console.WriteLine($"   ✓ Ticket status changed to Resolved");

            // 2. Generate invoice
            var invoice = await _invoiceService.GenerateInvoiceAsync(ticketId);
            Console.WriteLine($"   ✓ Invoice {invoice.InvoiceNumber} generated");

            // 3. Notify resident (handled internally)
            Console.WriteLine($"   ✓ Resident notified");

            return invoice;
        }

        /// <summary>
        /// Simplified method to assign ticket to technician
        /// </summary>
        public async Task AssignTicketToTechnicianAsync(int ticketId, int technicianId, int adminId)
        {
            Console.WriteLine("🎭 Facade Pattern: Assigning ticket to technician...");

            // 1. Assign ticket
            await _ticketService.AssignTicketAsync(ticketId, technicianId, adminId);
            Console.WriteLine($"   ✓ Ticket assigned to technician");

            // 2. Notifications sent automatically
            Console.WriteLine($"   ✓ Technician notified");
            Console.WriteLine($"   ✓ Resident notified");
        }
    }
}
