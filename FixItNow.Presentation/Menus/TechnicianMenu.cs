using FixItNow.Application.Interfaces;
using FixItNow.Domain.Enums;
using FixItNow.Presentation.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FixItNow.Presentation.Menus
{
    public class TechnicianMenu
    {
        private readonly IAuthenticationService _authService;
        private readonly ITicketService _ticketService;

        public TechnicianMenu(IAuthenticationService authService, ITicketService ticketService)
        {
            _authService = authService;
            _ticketService = ticketService;
        }

        public async Task ShowAsync()
        {
            while (true)
            {
                var currentUser = _authService.GetCurrentUser();
                ConsoleHelper.PrintHeader($"Technician Menu - Welcome {currentUser.FullName}");

                Console.WriteLine("1. View My Assigned Tickets");
                Console.WriteLine("2. Update Ticket Status");
                Console.WriteLine("3. Logout");
                Console.Write("\nSelect option: ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await ViewAssignedTicketsAsync();
                        break;
                    case "2":
                        await UpdateTicketStatusAsync();
                        break;
                    case "3":
                        return;
                    default:
                        Console.WriteLine("\n❌ Invalid option!");
                        ConsoleHelper.PressAnyKey();
                        break;
                }
            }
        }

        private async Task ViewAssignedTicketsAsync()
        {
            ConsoleHelper.PrintHeader("My Assigned Tickets");

            var currentUser = _authService.GetCurrentUser();
            var tickets = await _ticketService.GetAssignedTicketsAsync(currentUser.UserId);

            if (!tickets.Any())
            {
                Console.WriteLine("No assigned tickets.");
            }
            else
            {
                foreach (var ticket in tickets)
                {
                    Console.WriteLine($"\n[{ticket.TicketId}] {ticket.TicketCode} - {ticket.Title}");
                    Console.WriteLine($"Status: {ticket.StatusId} | Priority: {ticket.PriorityId}");
                    Console.WriteLine($"Description: {ticket.Description}");
                    Console.WriteLine(new string('-', 60));
                }
            }

            ConsoleHelper.PressAnyKey();
        }

        private async Task UpdateTicketStatusAsync()
        {
            ConsoleHelper.PrintHeader("Update Ticket Status");

            Console.Write("Enter Ticket ID: ");
            var ticketId = int.Parse(Console.ReadLine() ?? "0");

            Console.WriteLine("\nStatuses:");
            Console.WriteLine("3. InProgress");
            Console.WriteLine("4. Resolved");
            Console.Write("Select new status: ");
            var statusChoice = Console.ReadLine();

            // FIX: Use traditional if-else instead of switch expression
            TicketStatusEnum newStatus;
            if (statusChoice == "3")
            {
                newStatus = TicketStatusEnum.InProgress;
            }
            else if (statusChoice == "4")
            {
                newStatus = TicketStatusEnum.Resolved;
            }
            else
            {
                newStatus = TicketStatusEnum.InProgress; // Default
            }

            try
            {
                var currentUser = _authService.GetCurrentUser();
                await _ticketService.ChangeStatusAsync(ticketId, newStatus, currentUser.UserId);
                Console.WriteLine("\n✅ Status updated successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n❌ Error: {ex.Message}");
            }

            ConsoleHelper.PressAnyKey();
        }
    }
}