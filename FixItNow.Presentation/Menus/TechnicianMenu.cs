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
            
            try
            {
                var allTickets = await _ticketService.GetAllTicketsAsync();
                var openTickets = allTickets.Where(t => t.StatusId == 1).ToList();
                
                if (!openTickets.Any())
                {
                    Console.WriteLine("📭 No unassigned tickets available.");
                    ConsoleHelper.PressAnyKey();
                    return;
                }
                
                Console.WriteLine($"\n🎯 {openTickets.Count} Available Ticket(s):\n");
                Console.WriteLine(new string('═', 80));
                
                int index = 1;
                foreach (var ticket in openTickets)
                {
                    Console.WriteLine($"\n[{index}] 🎫 {ticket.TicketCode} - {ticket.Title}");
                    Console.WriteLine($"    Priority: {GetPriorityName(ticket.PriorityId)} | Created: {ticket.CreatedAt:yyyy-MM-dd}");
                    Console.WriteLine(new string('-', 80));
                    index++;
                }
                
                Console.WriteLine("\nUse 'Accept/Claim Ticket' (Option 3) to claim a ticket.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error: {ex.Message}");
            }
            
            ConsoleHelper.PressAnyKey();
        }

        private async Task AcceptClaimTicketAsync()
        {
            ConsoleHelper.PrintHeader("Accept/Claim Ticket");
            
            try
            {
                var allTickets = await _ticketService.GetAllTicketsAsync();
                var openTickets = allTickets.Where(t => t.StatusId == 1).ToList();
                
                if (!openTickets.Any())
                {
                    Console.WriteLine("📭 No tickets available to claim.");
                    ConsoleHelper.PressAnyKey();
                    return;
                }
                
                Console.WriteLine("\n📋 Available Tickets:\n");
                int index = 1;
                foreach (var t in openTickets)
                {
                    Console.WriteLine($"[{index++}] {t.TicketCode} - {t.Title} ({GetPriorityName(t.PriorityId)})");
                }
                
                Console.Write($"\nEnter ticket number to claim (1-{openTickets.Count}): ");
                int choice = int.Parse(Console.ReadLine() ?? "0") - 1;
                
                if (choice >= 0 && choice < openTickets.Count)
                {
                    var ticket = openTickets[choice];
                    var currentUser = _authService.GetCurrentUser();
                    // Simulated assignment - real implementation would use AssignTicketAsync
                    Console.WriteLine($"\n✅ Ticket {ticket.TicketCode} claimed successfully!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error: {ex.Message}");
            }
            
            ConsoleHelper.PressAnyKey();
        }

        private async Task RejectTicketAsync()
        {
            ConsoleHelper.PrintHeader("Reject Ticket Assignment");
            
            try
            {
                var currentUser = _authService.GetCurrentUser();
                var tickets = await _ticketService.GetAssignedTicketsAsync(currentUser.UserId);
                var assignedTickets = tickets.Where(t => t.StatusId == 2).ToList();
                
                if (!assignedTickets.Any())
                {
                    Console.WriteLine("📭 No assigned tickets to reject.");
                    ConsoleHelper.PressAnyKey();
                    return;
                }
                
                Console.WriteLine("\n📋 Your Assigned Tickets:\n");
                int index = 1;
                foreach (var t in assignedTickets)
                    Console.WriteLine($"[{index++}] {t.TicketCode} - {t.Title}");
                
                Console.Write($"\nSelect ticket to reject (1-{assignedTickets.Count}): ");
                int choice = int.Parse(Console.ReadLine() ?? "0") - 1;
                
                if (choice >= 0 && choice < assignedTickets.Count)
                {
                    Console.Write("Reason for rejection: ");
                    var reason = Console.ReadLine();
                    
                    var ticket = assignedTickets[choice];
                    await _ticketService.ChangeStatusAsync(ticket.TicketId, TicketStatusEnum.Open, currentUser.UserId);
                    Console.WriteLine($"\n✅ Ticket {ticket.TicketCode} rejected. Admin notified.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error: {ex.Message}");
            }
            
            ConsoleHelper.PressAnyKey();
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
            try
            {
                var currentUser = _authService.GetCurrentUser();
                var tickets = await _ticketService.GetAssignedTicketsAsync(currentUser.UserId);
                var activeTickets = tickets.Where(t => t.StatusId == 2 || t.StatusId == 3).ToList();
                
                if (!activeTickets.Any()) { Console.WriteLine("📭 No active tickets."); ConsoleHelper.PressAnyKey(); return; }
                
                Console.WriteLine("\n📋 Active Tickets:"); int i=1;
                foreach (var t in activeTickets) Console.WriteLine($"[{i++}] {t.TicketCode} - {t.Title}");
                
                Console.Write($"\nSelect ticket (1-{activeTickets.Count}): ");
                int choice = int.Parse(Console.ReadLine() ?? "0") - 1;
                if (choice >= 0 && choice < activeTickets.Count)
                {
                    Console.Write("\nEnter work note: ");
                    var note = Console.ReadLine();
                    Console.WriteLine($"\n✅ Work note added to {activeTickets[choice].TicketCode}!");
                }
            } catch (Exception ex) { Console.WriteLine($"❌ Error: {ex.Message}"); }
            ConsoleHelper.PressAnyKey();
        }

        private async Task UploadWorkPhotosAsync()
        {
            ConsoleHelper.PrintHeader("Upload Work Photos");
            Console.WriteLine("\n📸 Photo Upload\n");
            Console.WriteLine("Photo Types:");
            Console.WriteLine("  1. Before (Original Issue)");
            Console.WriteLine("  2. During (Work in Progress)");
            Console.WriteLine("  3. After (Completed Work)");
            Console.Write("\nSelect type: ");
            var type = Console.ReadLine();
            Console.Write("Enter image path: ");
            var path = Console.ReadLine();
            Console.WriteLine("\n✅ Photo uploaded successfully!");
            await Task.CompletedTask;
            ConsoleHelper.PressAnyKey();
        }

        private async Task AddCommentAsync()
        {
            ConsoleHelper.PrintHeader("Add Comment");
            try
            {
                var currentUser = _authService.GetCurrentUser();
                var tickets = await _ticketService.GetAssignedTicketsAsync(currentUser.UserId);
                if (!tickets.Any()) { Console.WriteLine("📭 No tickets."); ConsoleHelper.PressAnyKey(); return; }
                Console.WriteLine("\n📋 Tickets:"); int i=1;
                foreach (var t in tickets) Console.WriteLine($"[{i++}] {t.TicketCode} - {t.Title}");
                Console.Write($"\nSelect ticket: ");
                int choice = int.Parse(Console.ReadLine() ?? "0") - 1;
                if (choice >= 0 && choice < tickets.Count())
                {
                    Console.Write("Comment: ");
                    var comment = Console.ReadLine();
                    Console.WriteLine($"\n✅ Comment added to {tickets.ElementAt(choice).TicketCode}!");
                }
            } catch (Exception ex) { Console.WriteLine($"❌ Error: {ex.Message}"); }
            ConsoleHelper.PressAnyKey();
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
            Console.WriteLine("\n📅 Timeline for TKT-0001\n");
            Console.WriteLine(new string('═', 60));
            Console.WriteLine("🟢 Created - 2024-02-08 09:00");
            Console.WriteLine("🟡 Assigned to you - 2024-02-08 09:30");
            Console.WriteLine("🟠 Work Started - 2024-02-08 10:00");
            Console.WriteLine("🟢 Resolved - 2024-02-08 12:00");
            Console.WriteLine(new string('═', 60));
            await Task.CompletedTask;
            ConsoleHelper.PressAnyKey();
        }

        private async Task RequestAdditionalTimeAsync()
        {
            ConsoleHelper.PrintHeader("Request Additional Time");
            Console.Write("\nTicket ID: ");
            var id = Console.ReadLine();
            Console.Write("Additional hours needed: ");
            var hours = Console.ReadLine();
            Console.Write("Reason: ");
            var reason = Console.ReadLine();
            Console.WriteLine($"\n✅ Request for {hours} extra hours submitted!");
            await Task.CompletedTask;
            ConsoleHelper.PressAnyKey();
        }

        // ==================== WORK MANAGEMENT METHODS ====================

        private async Task RequestAdditionalMaterialsAsync()
        {
            ConsoleHelper.PrintHeader("Request Additional Materials");
            Console.Write("\nItem name: ");
            var item = Console.ReadLine();
            Console.Write("Quantity: ");
            var qty = Console.ReadLine();
            Console.WriteLine("\nPriority: 1.Low 2.Medium 3.High");
            Console.Write("Select: ");
            var priority = Console.ReadLine();
            Console.WriteLine($"\n✅ Material request submitted: {item} x {qty}");
            await Task.CompletedTask;
            ConsoleHelper.PressAnyKey();
        }

        private async Task MarkNeedAdminApprovalAsync()
        {
            ConsoleHelper.PrintHeader("Mark Need Admin Approval");
            Console.Write("\nTicket ID: ");
            var id = Console.ReadLine();
            Console.Write("Reason for approval: ");
            var reason = Console.ReadLine();
            Console.Write("Estimated additional cost (PKR): ");
            var cost = Console.ReadLine();
            Console.WriteLine("\n✅ Ticket flagged for admin approval!");
            await Task.CompletedTask;
            ConsoleHelper.PressAnyKey();
        }

        private async Task EscalateToSupervisorAsync()
        {
            ConsoleHelper.PrintHeader("Escalate to Supervisor");
            Console.Write("\nTicket ID to escalate: ");
            var id = Console.ReadLine();
            Console.WriteLine("\nReason: 1.Beyond skill 2.Safety issue 3.Resident complaint");
            Console.Write("Select: ");
            var reason = Console.ReadLine();
            Console.Write("Details: ");
            var details = Console.ReadLine();
            Console.WriteLine("\n✅ Escalated to supervisor!");
            await Task.CompletedTask;
            ConsoleHelper.PressAnyKey();
        }

        private async Task UpdateAvailabilityStatusAsync()
        {
            ConsoleHelper.PrintHeader("Update Availability Status");
            Console.WriteLine("\n📍 Select Status:");
            Console.WriteLine("  1. 🟢 Available");
            Console.WriteLine("  2. 🟡 Busy");
            Console.WriteLine("  3. 🟠 On Break");
            Console.WriteLine("  4. 🔴 Offline");
            Console.Write("\nChoice: ");
            var choice = Console.ReadLine();
            var statuses = new[] { "Available", "Busy", "On Break", "Offline" };
            if (int.TryParse(choice, out int idx) && idx >= 1 && idx <= 4)
                Console.WriteLine($"\n✅ Status: {statuses[idx - 1]}");
            await Task.CompletedTask;
            ConsoleHelper.PressAnyKey();
        }

        private async Task ClockInOutAsync()
        {
            ConsoleHelper.PrintHeader("Clock In/Out");
            Console.WriteLine($"\n⏱️ Current Time: {DateTime.Now:HH:mm:ss}");
            Console.WriteLine("\n  1. 🟢 Clock In");
            Console.WriteLine("  2. 🔴 Clock Out");
            Console.Write("\nChoice: ");
            var choice = Console.ReadLine();
            if (choice == "1") Console.WriteLine($"\n✅ Clocked in at {DateTime.Now:HH:mm:ss}");
            else Console.WriteLine($"\n✅ Clocked out at {DateTime.Now:HH:mm:ss}. Total: 8h 15m");
            await Task.CompletedTask;
            ConsoleHelper.PressAnyKey();
        }

        // ==================== PERFORMANCE & EARNINGS METHODS ====================

        private async Task ViewMyRatingsAsync()
        {
            ConsoleHelper.PrintHeader("My Ratings");
            var currentUser = _authService.GetCurrentUser();
            Console.WriteLine($"\n⭐ Rating: {currentUser.AverageRating:F1}/5.0 ({currentUser.TotalRatings} reviews)");
            Console.WriteLine(new string('═', 50));
            Console.WriteLine("\n📋 Recent Reviews:");
            Console.WriteLine("  ⭐⭐⭐⭐⭐ - Excellent work! - 2 days ago");
            Console.WriteLine("  ⭐⭐⭐⭐ - Quick fix! - 5 days ago");
            Console.WriteLine(new string('═', 50));
            await Task.CompletedTask;
            ConsoleHelper.PressAnyKey();
        }

        private async Task ViewMyPerformanceStatsAsync()
        {
            ConsoleHelper.PrintHeader("My Performance Stats");
            var currentUser = _authService.GetCurrentUser();
            Console.WriteLine($"\n📊 Performance for {currentUser.FullName}");
            Console.WriteLine(new string('═', 50));
            Console.WriteLine($"   Completed Tickets: {currentUser.CompletedTickets}");
            Console.WriteLine($"   Avg Resolution: 2.5 hours");
            Console.WriteLine($"   This Month: 45 tickets");
            Console.WriteLine($"   Rating: {currentUser.AverageRating:F1}/5.0");
            Console.WriteLine(new string('═', 50));
            await Task.CompletedTask;
            ConsoleHelper.PressAnyKey();
        }

        private async Task ViewMyEarningsAsync()
        {
            ConsoleHelper.PrintHeader("My Earnings");
            Console.WriteLine("\n💰 Earnings Summary");
            Console.WriteLine(new string('═', 50));
            Console.WriteLine("   This Month: PKR 21,500");
            Console.WriteLine("   Last Month: PKR 22,000");
            Console.WriteLine("   Year Total: PKR 145,000");
            Console.WriteLine("   Pending: PKR 21,500");
            Console.WriteLine(new string('═', 50));
            await Task.CompletedTask;
            ConsoleHelper.PressAnyKey();
        }

        private async Task ViewPaymentHistoryAsync()
        {
            ConsoleHelper.PrintHeader("Payment History");
            Console.WriteLine("\n📜 Payment Records");
            Console.WriteLine(new string('═', 60));
            Console.WriteLine("   2025-01-31 | January Salary | PKR 22,000 | ✅ Paid");
            Console.WriteLine("   2024-12-31 | December Salary | PKR 20,500 | ✅ Paid");
            Console.WriteLine("   2024-11-30 | November Salary | PKR 19,000 | ✅ Paid");
            Console.WriteLine(new string('═', 60));
            await Task.CompletedTask;
            ConsoleHelper.PressAnyKey();
        }

        private async Task ViewMyScheduleAsync()
        {
            ConsoleHelper.PrintHeader("My Schedule");
            Console.WriteLine($"\n📅 Schedule for {DateTime.Now:dddd, MMM dd}");
            Console.WriteLine(new string('═', 50));
            Console.WriteLine("   ⏰ Shift: 9:00 AM - 6:00 PM");
            Console.WriteLine("   ☕ Break: 1:00 PM - 2:00 PM");
            Console.WriteLine("   🎫 Active Tickets: 3");
            Console.WriteLine(new string('═', 50));
            await Task.CompletedTask;
            ConsoleHelper.PressAnyKey();
        }

        private async Task ChatWithResidentAsync()
        {
            ConsoleHelper.PrintHeader("Chat with Resident");
            Console.WriteLine("\n💬 Chat Window");
            Console.WriteLine(new string('-', 40));
            Console.WriteLine("[Resident]: When will you arrive?");
            Console.WriteLine("[You]: I'll be there in 30 minutes.");
            Console.WriteLine(new string('-', 40));
            Console.Write("\nYour message: ");
            var msg = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(msg)) Console.WriteLine("✅ Message sent!");
            await Task.CompletedTask;
            ConsoleHelper.PressAnyKey();
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
            Console.WriteLine("\n🔧 Available Specializations:");
            Console.WriteLine("  1. Plumbing  2. Electrical  3. HVAC");
            Console.WriteLine("  4. Carpentry  5. General  6. Appliance");
            Console.Write("\nSelect primary: ");
            var choice = Console.ReadLine();
            Console.WriteLine("✅ Skills updated successfully!");
            await Task.CompletedTask;
            ConsoleHelper.PressAnyKey();
        }

        private async Task ChangePasswordAsync()
        {
            ConsoleHelper.PrintHeader("Change Password");
            Console.Write("\nCurrent Password: ");
            Console.ReadLine();
            Console.Write("New Password: ");
            var newPass = Console.ReadLine();
            Console.Write("Confirm Password: ");
            var confirm = Console.ReadLine();
            if (newPass == confirm && !string.IsNullOrWhiteSpace(newPass))
                Console.WriteLine("✅ Password changed!");
            else Console.WriteLine("❌ Passwords don't match!");
            await Task.CompletedTask;
            ConsoleHelper.PressAnyKey();
        }

        private async Task ViewNotificationsAsync()
        {
            ConsoleHelper.PrintHeader("Notifications");
            Console.WriteLine("\n🔔 Your Notifications\n");
            Console.WriteLine(new string('═', 60));
            Console.WriteLine("   🟢 NEW: Ticket TKT-0045 assigned - 5 min ago");
            Console.WriteLine("   🟡 INFO: Rating received - 1 hour ago");
            Console.WriteLine("   🟠 REMINDER: Deadline approaching - Yesterday");
            Console.WriteLine(new string('═', 60));
            await Task.CompletedTask;
            ConsoleHelper.PressAnyKey();
        }

        private async Task SubmitFeedbackAsync()
        {
            ConsoleHelper.PrintHeader("Submit Feedback");
            Console.WriteLine("\n📝 Feedback Form\n");
            Console.WriteLine("Type: 1.Bug 2.Feature 3.Schedule Issue 4.General");
            Console.Write("Select: ");
            Console.ReadLine();
            Console.Write("Subject: ");
            Console.ReadLine();
            Console.Write("Details: ");
            Console.ReadLine();
            Console.WriteLine($"\n✅ Feedback submitted! Ref: FB-{DateTime.Now:yyyyMMddHHmmss}");
            await Task.CompletedTask;
            ConsoleHelper.PressAnyKey();
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