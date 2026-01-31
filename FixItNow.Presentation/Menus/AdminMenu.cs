using FixItNow.Application.Interfaces;
using FixItNow.Domain.Enums;
using FixItNow.Presentation.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FixItNow.Presentation.Menus
{
    public class AdminMenu
    {
        private readonly IAuthenticationService _authService;
        private readonly ITicketService _ticketService;
        private readonly IUserService _userService;

        public AdminMenu(
            IAuthenticationService authService,
            ITicketService ticketService,
            IUserService userService)
        {
            _authService = authService;
            _ticketService = ticketService;
            _userService = userService;
        }

        public async Task ShowAsync()
        {
            while (true)
            {
                var currentUser = _authService.GetCurrentUser();
                ConsoleHelper.PrintHeader($"Admin Menu - Welcome {currentUser.FullName}");

                Console.WriteLine("1. View All Tickets");
                Console.WriteLine("2. Assign Ticket to Technician");
                Console.WriteLine("3. View All Users");
                Console.WriteLine("4. Logout");
                Console.Write("\nSelect option: ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await ViewAllTicketsAsync();
                        break;
                    case "2":
                        await AssignTicketAsync();
                        break;
                    case "3":
                        await ViewAllUsersAsync();
                        break;
                    case "4":
                        return;
                    default:
                        Console.WriteLine("\n❌ Invalid option!");
                        ConsoleHelper.PressAnyKey();
                        break;
                }
            }
        }

        private async Task ViewAllTicketsAsync()
        {
            ConsoleHelper.PrintHeader("All Tickets");

            var tickets = await _ticketService.GetAllTicketsAsync();

            if (!tickets.Any())
            {
                Console.WriteLine("No tickets found.");
            }
            else
            {
                foreach (var ticket in tickets)
                {
                    Console.WriteLine($"\n[{ticket.TicketId}] {ticket.TicketCode} - {ticket.Title}");
                    Console.WriteLine($"Status: {ticket.StatusId} | Priority: {ticket.PriorityId}");
                    Console.WriteLine($"Created By: User#{ticket.CreatedByUserId} on {ticket.CreatedAt:yyyy-MM-dd}");
                    Console.WriteLine(new string('-', 60));
                }
            }

            ConsoleHelper.PressAnyKey();
        }

        private async Task AssignTicketAsync()
        {
            ConsoleHelper.PrintHeader("Assign Ticket");

            Console.Write("Enter Ticket ID: ");
            var ticketId = int.Parse(Console.ReadLine() ?? "0");

            // Show available technicians
            var technicians = await _userService.GetTechniciansAsync();
            Console.WriteLine("\nAvailable Technicians:");
            foreach (var tech in technicians)
            {
                Console.WriteLine($"[{tech.UserId}] {tech.FullName}");
            }

            Console.Write("\nSelect Technician ID: ");
            var technicianId = int.Parse(Console.ReadLine() ?? "0");

            try
            {
                var currentUser = _authService.GetCurrentUser();
                await _ticketService.AssignTicketAsync(ticketId, technicianId, currentUser.UserId);
                Console.WriteLine("\n✅ Ticket assigned successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n❌ Error: {ex.Message}");
            }

            ConsoleHelper.PressAnyKey();
        }

        private async Task ViewAllUsersAsync()
        {
            ConsoleHelper.PrintHeader("All Users");

            var users = await _userService.GetAllUsersAsync();

            foreach (var user in users)
            {
                Console.WriteLine($"\n[{user.UserId}] {user.FullName} ({user.Username})");
                Console.WriteLine($"Role: {user.RoleId} | Active: {user.IsActive}");
                Console.WriteLine(new string('-', 60));
            }

            ConsoleHelper.PressAnyKey();
        }
    }
}