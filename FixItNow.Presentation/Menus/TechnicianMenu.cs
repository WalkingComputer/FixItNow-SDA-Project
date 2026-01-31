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
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("╔════════════════════════════════════════════════════════════════════════════════╗");
                Console.WriteLine("║                         TECHNICIAN DASHBOARD                                   ║");
                Console.WriteLine("╚════════════════════════════════════════════════════════════════════════════════╝");
                Console.ResetColor();
                
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n>> TICKET MANAGEMENT");
                Console.ResetColor();
                Console.WriteLine("════════════════════════════════════════════════════════════════════════════════");
                Console.WriteLine(" 1. View Assigned Tickets         2. View Available Tickets (to claim)");
                Console.WriteLine(" 3. Accept/Claim Ticket           4. Reject Ticket Assignment");
                Console.WriteLine(" 5. Update Ticket Status          6. Start Work (In Progress)");
                Console.WriteLine(" 7. Mark as Resolved              8. Add Work Notes");
                Console.WriteLine(" 9. Upload Work Photos           10. Add Comment to Ticket");
                Console.WriteLine("11. View Ticket Details          12. View Ticket Timeline");
                Console.WriteLine("13. Request Additional Time");
                
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n>> WORK MANAGEMENT");
                Console.ResetColor();
                Console.WriteLine("════════════════════════════════════════════════════════════════════════════════");
                Console.WriteLine("14. Request Additional Materials 15. Mark Need Admin Approval");
                Console.WriteLine("16. Escalate to Supervisor       17. Update Availability Status");
                Console.WriteLine("18. Clock In/Out");
                
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n>> PERFORMANCE & EARNINGS");
                Console.ResetColor();
                Console.WriteLine("════════════════════════════════════════════════════════════════════════════════");
                Console.WriteLine("19. View My Ratings              20. View My Performance Stats");
                Console.WriteLine("21. View My Earnings             22. View Payment History");
                Console.WriteLine("23. View My Schedule             24. Chat with Resident");
                
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n>> PROFILE & SETTINGS");
                Console.ResetColor();
                Console.WriteLine("════════════════════════════════════════════════════════════════════════════════");
                Console.WriteLine("25. View My Profile              26. Update Skills/Specialization");
                Console.WriteLine("27. Change Password              28. View Notifications");
                Console.WriteLine("29. Submit Feedback");
                
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n>> EXIT");
                Console.ResetColor();
                Console.WriteLine("════════════════════════════════════════════════════════════════════════════════");
                Console.WriteLine("30. Logout");
                Console.WriteLine("════════════════════════════════════════════════════════════════════════════════");
                
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("\nSelect option (1-30): ");
                Console.ResetColor();

                var choice = Console.ReadLine();

                switch (choice)
                {
                    // TICKET MANAGEMENT
                    case "1": await ViewAssignedTicketsAsync(); break;
                    case "2": await ViewAvailableTicketsAsync(); break;
                    case "3": await AcceptClaimTicketAsync(); break;
                    case "4": await RejectTicketAsync(); break;
                    case "5": await UpdateTicketStatusAsync(); break;
                    case "6": await StartWorkAsync(); break;
                    case "7": await MarkAsResolvedAsync(); break;
                    case "8": await AddWorkNotesAsync(); break;
                    case "9": await UploadWorkPhotosAsync(); break;
                    case "10": await AddCommentAsync(); break;
                    case "11": await ViewTicketDetailsAsync(); break;
                    case "12": await ViewTicketTimelineAsync(); break;
                    case "13": await RequestAdditionalTimeAsync(); break;
                    
                    // WORK MANAGEMENT
                    case "14": await RequestAdditionalMaterialsAsync(); break;
                    case "15": await MarkNeedAdminApprovalAsync(); break;
                    case "16": await EscalateToSupervisorAsync(); break;
                    case "17": await UpdateAvailabilityStatusAsync(); break;
                    case "18": await ClockInOutAsync(); break;
                    
                    // PERFORMANCE & EARNINGS
                    case "19": await ViewMyRatingsAsync(); break;
                    case "20": await ViewMyPerformanceStatsAsync(); break;
                    case "21": await ViewMyEarningsAsync(); break;
                    case "22": await ViewPaymentHistoryAsync(); break;
                    case "23": await ViewMyScheduleAsync(); break;
                    case "24": await ChatWithResidentAsync(); break;
                    
                    // PROFILE & SETTINGS
                    case "25": await ViewMyProfileAsync(); break;
                    case "26": await UpdateSkillsAsync(); break;
                    case "27": await ChangePasswordAsync(); break;
                    case "28": await ViewNotificationsAsync(); break;
                    case "29": await SubmitFeedbackAsync(); break;
                    
                    // EXIT
                    case "30": return;
                    
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\n❌ Invalid option!");
                        Console.ResetColor();
                        ConsoleHelper.PressAnyKey();
                        break;
                }
            }
        }

        // ==================== TICKET MANAGEMENT METHODS ====================

        private async Task ViewAssignedTicketsAsync()
        {
            ConsoleHelper.PrintHeader("My Assigned Tickets");

            var currentUser = _authService.GetCurrentUser();
            var tickets = await _ticketService.GetAssignedTicketsAsync(currentUser.UserId);

            if (!tickets.Any())
            {
                Console.WriteLine("📭 No assigned tickets.");
            }
            else
            {
                Console.WriteLine($"\n📋 You have {tickets.Count()} assigned ticket(s):\n");
                Console.WriteLine(new string('═', 80));
                
                foreach (var ticket in tickets)
                {
                    string status = GetStatusName(ticket.StatusId);
                    string priority = GetPriorityName(ticket.PriorityId);
                    
                    Console.WriteLine($"\n🎫 [{ticket.TicketCode}] {ticket.Title}");
                    Console.WriteLine($"   Status: {status} | Priority: {priority}");
                    Console.WriteLine($"   Description: {ticket.Description}");
                    Console.WriteLine($"   Created: {ticket.CreatedAt:yyyy-MM-dd HH:mm}");
                    Console.WriteLine(new string('-', 80));
                }
            }

            ConsoleHelper.PressAnyKey();
        }

        private async Task ViewAvailableTicketsAsync()
        {
            ConsoleHelper.PrintHeader("Available Tickets to Claim");
            Console.WriteLine("🎯 Feature Coming Soon!");
            Console.WriteLine("View unassigned tickets you can claim.");
            ConsoleHelper.PressAnyKey();
            await Task.CompletedTask;
        }

        private async Task AcceptClaimTicketAsync()
        {
            ConsoleHelper.PrintHeader("Accept/Claim Ticket");
            Console.WriteLine("✅ Feature Coming Soon!");
            Console.WriteLine("Accept assigned tickets or claim available ones.");
            ConsoleHelper.PressAnyKey();
            await Task.CompletedTask;
        }

        private async Task RejectTicketAsync()
        {
            ConsoleHelper.PrintHeader("Reject Ticket Assignment");
            Console.WriteLine("❌ Feature Coming Soon!");
            Console.WriteLine("Reject tickets with a reason.");
            ConsoleHelper.PressAnyKey();
            await Task.CompletedTask;
        }

        private async Task UpdateTicketStatusAsync()
        {
            ConsoleHelper.PrintHeader("Update Ticket Status");

            Console.Write("Enter Ticket ID: ");
            var ticketId = int.Parse(Console.ReadLine() ?? "0");

            Console.WriteLine("\n📊 Statuses:");
            Console.WriteLine("3. InProgress");
            Console.WriteLine("4. Resolved");
            Console.Write("Select new status: ");
            var statusChoice = Console.ReadLine();

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
                newStatus = TicketStatusEnum.InProgress;
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

        private async Task StartWorkAsync()
        {
            ConsoleHelper.PrintHeader("Start Work");
            
            try
            {
                var currentUser = _authService.GetCurrentUser();
                var tickets = await _ticketService.GetAssignedTicketsAsync(currentUser.UserId);
                var assignedTickets = tickets.Where(t => t.StatusId == 2).ToList();
                
                if (!assignedTickets.Any())
                {
                    Console.WriteLine("📭 No assigned tickets to start.");
                    ConsoleHelper.PressAnyKey();
                    return;
                }
                
                Console.WriteLine("\n📋 Assigned Tickets:");
                for (int i = 0; i < assignedTickets.Count(); i++)
                {
                    Console.WriteLine($"{i + 1}. {assignedTickets.ElementAt(i).TicketCode} - {assignedTickets.ElementAt(i).Title}");
                }
                
                Console.Write($"\nSelect ticket to start (1-{assignedTickets.Count()}): ");
                int choice = int.Parse(Console.ReadLine() ?? "0") - 1;
                
                if (choice >= 0 && choice < assignedTickets.Count())
                {
                    var ticket = assignedTickets.ElementAt(choice);
                    await _ticketService.ChangeStatusAsync(ticket.TicketId, TicketStatusEnum.InProgress, currentUser.UserId);
                    Console.WriteLine("\n✅ Work started! Ticket status: In Progress");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error: {ex.Message}");
            }
            
            ConsoleHelper.PressAnyKey();
        }

        private async Task MarkAsResolvedAsync()
        {
            ConsoleHelper.PrintHeader("Mark as Resolved");
            
            try
            {
                var currentUser = _authService.GetCurrentUser();
                var tickets = await _ticketService.GetAssignedTicketsAsync(currentUser.UserId);
                var inProgressTickets = tickets.Where(t => t.StatusId == 3).ToList();
                
                if (!inProgressTickets.Any())
                {
                    Console.WriteLine("📭 No in-progress tickets.");
                    ConsoleHelper.PressAnyKey();
                    return;
                }
                
                Console.WriteLine("\n📋 In-Progress Tickets:");
                for (int i = 0; i < inProgressTickets.Count(); i++)
                {
                    Console.WriteLine($"{i + 1}. {inProgressTickets.ElementAt(i).TicketCode} - {inProgressTickets.ElementAt(i).Title}");
                }
                
                Console.Write($"\nSelect ticket to resolve (1-{inProgressTickets.Count()}): ");
                int choice = int.Parse(Console.ReadLine() ?? "0") - 1;
                
                if (choice >= 0 && choice < inProgressTickets.Count())
                {
                    var ticket = inProgressTickets.ElementAt(choice);
                    await _ticketService.ChangeStatusAsync(ticket.TicketId, TicketStatusEnum.Resolved, currentUser.UserId);
                    Console.WriteLine("\n✅ Ticket marked as resolved!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error: {ex.Message}");
            }
            
            ConsoleHelper.PressAnyKey();
        }

        private async Task AddWorkNotesAsync()
        {
            ConsoleHelper.PrintHeader("Add Work Notes");
            Console.WriteLine("📝 Feature Coming Soon!");
            Console.WriteLine("Add detailed notes about your work.");
            ConsoleHelper.PressAnyKey();
            await Task.CompletedTask;
        }

        private async Task UploadWorkPhotosAsync()
        {
            ConsoleHelper.PrintHeader("Upload Work Photos");
            Console.WriteLine("📸 Feature Coming Soon!");
            Console.WriteLine("Upload before/after photos of your work.");
            ConsoleHelper.PressAnyKey();
            await Task.CompletedTask;
        }

        private async Task AddCommentAsync()
        {
            ConsoleHelper.PrintHeader("Add Comment");
            Console.WriteLine("💬 Feature Coming Soon!");
            Console.WriteLine("Add comments to tickets.");
            ConsoleHelper.PressAnyKey();
            await Task.CompletedTask;
        }

        private async Task ViewTicketDetailsAsync()
        {
            ConsoleHelper.PrintHeader("View Ticket Details");
            
            try
            {
                var currentUser = _authService.GetCurrentUser();
                var tickets = await _ticketService.GetAssignedTicketsAsync(currentUser.UserId);
                
                if (!tickets.Any())
                {
                    Console.WriteLine("📭 No tickets assigned.");
                    ConsoleHelper.PressAnyKey();
                    return;
                }
                
                Console.WriteLine("\n📋 Your Tickets:");
                int index = 1;
                foreach (var t in tickets)
                {
                    Console.WriteLine($"{index++}. {t.TicketCode} - {t.Title}");
                }
                
                Console.Write($"\nSelect ticket (1-{tickets.Count()}): ");
                int choice = int.Parse(Console.ReadLine() ?? "0") - 1;
                
                if (choice >= 0 && choice < tickets.Count())
                {
                    var ticket = tickets.ElementAt(choice);
                    Console.WriteLine("\n" + new string('═', 80));
                    Console.WriteLine($"🎫 {ticket.TicketCode} - {ticket.Title}");
                    Console.WriteLine($"   Description: {ticket.Description}");
                    Console.WriteLine($"   Status: {GetStatusName(ticket.StatusId)}");
                    Console.WriteLine($"   Priority: {GetPriorityName(ticket.PriorityId)}");
                    Console.WriteLine($"   Created: {ticket.CreatedAt:yyyy-MM-dd HH:mm:ss}");
                    Console.WriteLine(new string('═', 80));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error: {ex.Message}");
            }
            
            ConsoleHelper.PressAnyKey();
        }

        private async Task ViewTicketTimelineAsync()
        {
            ConsoleHelper.PrintHeader("View Ticket Timeline");
            Console.WriteLine("📅 Feature Coming Soon!");
            Console.WriteLine("View complete ticket history.");
            ConsoleHelper.PressAnyKey();
            await Task.CompletedTask;
        }

        private async Task RequestAdditionalTimeAsync()
        {
            ConsoleHelper.PrintHeader("Request Additional Time");
            Console.WriteLine("⏰ Feature Coming Soon!");
            Console.WriteLine("Request deadline extension.");
            ConsoleHelper.PressAnyKey();
            await Task.CompletedTask;
        }

        // ==================== WORK MANAGEMENT METHODS ====================

        private async Task RequestAdditionalMaterialsAsync()
        {
            ConsoleHelper.PrintHeader("Request Additional Materials");
            Console.WriteLine("🔧 Feature Coming Soon!");
            Console.WriteLine("Request tools or materials from admin.");
            ConsoleHelper.PressAnyKey();
            await Task.CompletedTask;
        }

        private async Task MarkNeedAdminApprovalAsync()
        {
            ConsoleHelper.PrintHeader("Mark Need Admin Approval");
            Console.WriteLine("⚠️ Feature Coming Soon!");
            Console.WriteLine("Flag tickets requiring admin review.");
            ConsoleHelper.PressAnyKey();
            await Task.CompletedTask;
        }

        private async Task EscalateToSupervisorAsync()
        {
            ConsoleHelper.PrintHeader("Escalate to Supervisor");
            Console.WriteLine("🆙 Feature Coming Soon!");
            Console.WriteLine("Escalate complex issues to supervisor.");
            ConsoleHelper.PressAnyKey();
            await Task.CompletedTask;
        }

        private async Task UpdateAvailabilityStatusAsync()
        {
            ConsoleHelper.PrintHeader("Update Availability Status");
            Console.WriteLine("📍 Feature Coming Soon!");
            Console.WriteLine("Set yourself as available/busy/offline.");
            ConsoleHelper.PressAnyKey();
            await Task.CompletedTask;
        }

        private async Task ClockInOutAsync()
        {
            ConsoleHelper.PrintHeader("Clock In/Out");
            Console.WriteLine("⏱️ Feature Coming Soon!");
            Console.WriteLine("Track your working hours.");
            ConsoleHelper.PressAnyKey();
            await Task.CompletedTask;
        }

        // ==================== PERFORMANCE & EARNINGS METHODS ====================

        private async Task ViewMyRatingsAsync()
        {
            ConsoleHelper.PrintHeader("My Ratings");
            Console.WriteLine("⭐ Feature Coming Soon!");
            Console.WriteLine("View your ratings and reviews from residents.");
            ConsoleHelper.PressAnyKey();
            await Task.CompletedTask;
        }

        private async Task ViewMyPerformanceStatsAsync()
        {
            ConsoleHelper.PrintHeader("My Performance Stats");
            Console.WriteLine("📊 Feature Coming Soon!");
            Console.WriteLine("View tickets completed, average time, etc.");
            ConsoleHelper.PressAnyKey();
            await Task.CompletedTask;
        }

        private async Task ViewMyEarningsAsync()
        {
            ConsoleHelper.PrintHeader("My Earnings");
            Console.WriteLine("💰 Feature Coming Soon!");
            Console.WriteLine("View your total earnings.");
            ConsoleHelper.PressAnyKey();
            await Task.CompletedTask;
        }

        private async Task ViewPaymentHistoryAsync()
        {
            ConsoleHelper.PrintHeader("Payment History");
            Console.WriteLine("📜 Feature Coming Soon!");
            Console.WriteLine("View your payment history.");
            ConsoleHelper.PressAnyKey();
            await Task.CompletedTask;
        }

        private async Task ViewMyScheduleAsync()
        {
            ConsoleHelper.PrintHeader("My Schedule");
            Console.WriteLine("📅 Feature Coming Soon!");
            Console.WriteLine("View your work schedule.");
            ConsoleHelper.PressAnyKey();
            await Task.CompletedTask;
        }

        private async Task ChatWithResidentAsync()
        {
            ConsoleHelper.PrintHeader("Chat with Resident");
            Console.WriteLine("💬 Feature Coming Soon!");
            Console.WriteLine("Message residents about their tickets.");
            ConsoleHelper.PressAnyKey();
            await Task.CompletedTask;
        }

        // ==================== PROFILE & SETTINGS METHODS ====================

        private async Task ViewMyProfileAsync()
        {
            ConsoleHelper.PrintHeader("My Profile");
            
            var currentUser = _authService.GetCurrentUser();
            Console.WriteLine("\n👤 Your Profile:\n");
            Console.WriteLine(new string('═', 60));
            Console.WriteLine($"Name: {currentUser.FullName}");
            Console.WriteLine($"Username: {currentUser.Username}");
            Console.WriteLine($"Email: {currentUser.Email}");
            Console.WriteLine($"Phone: {currentUser.Phone}");
            Console.WriteLine($"Role: Technician");
            Console.WriteLine($"Specialization: {currentUser.Specialization ?? "General"}");
            Console.WriteLine($"Average Rating: {currentUser.AverageRating:F2}/5.0");
            Console.WriteLine($"Total Ratings: {currentUser.TotalRatings}");
            Console.WriteLine($"Completed Tickets: {currentUser.CompletedTickets}");
            Console.WriteLine(new string('═', 60));
            
            ConsoleHelper.PressAnyKey();
            await Task.CompletedTask;
        }

        private async Task UpdateSkillsAsync()
        {
            ConsoleHelper.PrintHeader("Update Skills/Specialization");
            Console.WriteLine("🔧 Feature Coming Soon!");
            Console.WriteLine("Update your skills and specialization.");
            ConsoleHelper.PressAnyKey();
            await Task.CompletedTask;
        }

        private async Task ChangePasswordAsync()
        {
            ConsoleHelper.PrintHeader("Change Password");
            Console.WriteLine("🔒 Feature Coming Soon!");
            Console.WriteLine("Change your account password.");
            ConsoleHelper.PressAnyKey();
            await Task.CompletedTask;
        }

        private async Task ViewNotificationsAsync()
        {
            ConsoleHelper.PrintHeader("Notifications");
            Console.WriteLine("🔔 Feature Coming Soon!");
            Console.WriteLine("View all your notifications.");
            ConsoleHelper.PressAnyKey();
            await Task.CompletedTask;
        }

        private async Task SubmitFeedbackAsync()
        {
            ConsoleHelper.PrintHeader("Submit Feedback");
            Console.WriteLine("📝 Feature Coming Soon!");
            Console.WriteLine("Share feedback about the system.");
            ConsoleHelper.PressAnyKey();
            await Task.CompletedTask;
        }

        // ==================== HELPER METHODS ====================

        private string GetStatusName(int statusId)
        {
            switch (statusId)
            {
                case 1: return "Open";
                case 2: return "Assigned";
                case 3: return "In Progress";
                case 4: return "Resolved";
                case 5: return "Closed";
                case 6: return "Reopened";
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
    }
}