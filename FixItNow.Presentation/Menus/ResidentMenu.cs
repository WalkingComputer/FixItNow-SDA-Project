using FixItNow.Application.DTOs;
using FixItNow.Application.Interfaces;
using FixItNow.Presentation.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FixItNow.Presentation.Menus
{
    public class ResidentMenu
    {
        private readonly IAuthenticationService _authService;
        private readonly ITicketService _ticketService;

        public ResidentMenu(IAuthenticationService authService, ITicketService ticketService)
        {
            _authService = authService;
            _ticketService = ticketService;
        }

        public async Task ShowAsync()
        {
            while (true)
            {
                var currentUser = _authService.GetCurrentUser();
                ConsoleHelper.PrintHeader($"Resident Menu - Welcome {currentUser.FullName}");

                Console.WriteLine("1. Create New Ticket");
                Console.WriteLine("2. View My Tickets");
                Console.WriteLine("3. Logout");
                Console.Write("\nSelect option: ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await CreateTicketAsync();
                        break;
                    case "2":
                        await ViewMyTicketsAsync();
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

        private async Task CreateTicketAsync()
        {
            ConsoleHelper.PrintHeader("Create New Ticket");

            Console.Write("Title: ");
            var title = Console.ReadLine();

            Console.Write("Description: ");
            var description = Console.ReadLine();

            Console.WriteLine("\nCategories:");
            Console.WriteLine("1. Plumbing");
            Console.WriteLine("2. Electric");
            Console.WriteLine("3. WiFi");
            Console.WriteLine("4. Furniture");
            Console.WriteLine("5. Other");
            Console.Write("Select category: ");
            var categoryId = int.Parse(Console.ReadLine() ?? "1");

            Console.WriteLine("\nPriorities:");
            Console.WriteLine("1. Low");
            Console.WriteLine("2. Medium");
            Console.WriteLine("3. High");
            Console.WriteLine("4. Urgent");
            Console.Write("Select priority: ");
            var priorityId = int.Parse(Console.ReadLine() ?? "1");

            Console.Write("Location ID (1-4): ");
            var locationId = int.Parse(Console.ReadLine() ?? "1");

            try
            {
                var request = new CreateTicketRequest
                {
                    Title = title,
                    Description = description,
                    CategoryId = categoryId,
                    LocationId = locationId,
                    PriorityId = priorityId,
                    CreatedByUserId = _authService.GetCurrentUser().UserId
                };

                var ticket = await _ticketService.CreateTicketAsync(request);
                Console.WriteLine($"\n✅ Ticket created successfully! Code: {ticket.TicketCode}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n❌ Error: {ex.Message}");
            }

            ConsoleHelper.PressAnyKey();
        }

        private async Task ViewMyTicketsAsync()
        {
            ConsoleHelper.PrintHeader("My Tickets");

            var currentUser = _authService.GetCurrentUser();
            var tickets = await _ticketService.GetMyTicketsAsync(currentUser.UserId);

            if (!tickets.Any())
            {
                Console.WriteLine("No tickets found.");
            }
            else
            {
                foreach (var ticket in tickets)
                {
                    Console.WriteLine($"\n{ticket.TicketCode} - {ticket.Title}");
                    Console.WriteLine($"Status: {ticket.StatusId} | Priority: {ticket.PriorityId}");
                    Console.WriteLine($"Created: {ticket.CreatedAt:yyyy-MM-dd}");
                    Console.WriteLine(new string('-', 60));
                }
            }

            ConsoleHelper.PressAnyKey();
        }
    }
}