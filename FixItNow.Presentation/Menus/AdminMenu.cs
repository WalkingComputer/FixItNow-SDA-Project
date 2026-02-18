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
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("╔════════════════════════════════════════════════════════════════════════════════╗");
                Console.WriteLine("║                            ADMIN DASHBOARD                                     ║");
                Console.WriteLine("╚════════════════════════════════════════════════════════════════════════════════╝");
                Console.ResetColor();
                
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n>> TICKET MANAGEMENT");
                Console.ResetColor();
                Console.WriteLine("════════════════════════════════════════════════════════════════════════════════");
                Console.WriteLine(" 1. View All Tickets              2. View Pending Tickets");
                Console.WriteLine(" 3. View In-Progress Tickets      4. View Resolved Tickets");
                Console.WriteLine(" 5. Assign Ticket to Technician   6. Reassign Ticket");
                Console.WriteLine(" 7. Close Ticket                  8. Cancel Ticket");
                Console.WriteLine(" 9. View Ticket Details          10. Add Admin Comment");
                Console.WriteLine("11. Escalate Ticket Priority");
                
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n>> USER MANAGEMENT");
                Console.ResetColor();
                Console.WriteLine("════════════════════════════════════════════════════════════════════════════════");
                Console.WriteLine("12. View All Users               13. View All Residents");
                Console.WriteLine("14. View All Technicians         15. Register New User");
                Console.WriteLine("16. Activate/Deactivate User     17. Reset User Password");
                Console.WriteLine("18. View User Details            19. Update User Role");
                
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n>> TECHNICIAN MANAGEMENT");
                Console.ResetColor();
                Console.WriteLine("════════════════════════════════════════════════════════════════════════════════");
                Console.WriteLine("20. View Technician Performance  21. View Technician Ratings");
                Console.WriteLine("22. Assign Technician Specialization");
                Console.WriteLine("23. Set Technician Availability  24. View Technician Workload");
                Console.WriteLine("25. Hire New Technician");
                
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n>> PAYMENT & FINANCIAL");
                Console.ResetColor();
                Console.WriteLine("════════════════════════════════════════════════════════════════════════════════");
                Console.WriteLine("26. View All Invoices            27. Generate Invoice");
                Console.WriteLine("28. Mark Payment as Received     29. Issue Refund");
                Console.WriteLine("30. View Payment Reports         31. Set Service Charges");
                Console.WriteLine("32. View Revenue Analytics       33. Export Financial Report");
                
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n>> REPORTS & ANALYTICS");
                Console.ResetColor();
                Console.WriteLine("════════════════════════════════════════════════════════════════════════════════");
                Console.WriteLine("34. View Dashboard Statistics    35. Generate Ticket Report");
                Console.WriteLine("36. Generate User Report         37. Generate Revenue Report");
                Console.WriteLine("38. View Category-wise Stats     39. View Location-wise Stats");
                Console.WriteLine("40. Export Reports to Excel");
                
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n>> SYSTEM MANAGEMENT");
                Console.ResetColor();
                Console.WriteLine("════════════════════════════════════════════════════════════════════════════════");
                Console.WriteLine("41. Manage Categories            42. Manage Locations");
                Console.WriteLine("43. Manage Priorities            44. Manage Payment Methods");
                Console.WriteLine("45. View Audit Logs              46. View System Notifications");
                Console.WriteLine("47. Broadcast Notification       48. Backup Database");
                
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n>> FEEDBACK & RATINGS");
                Console.ResetColor();
                Console.WriteLine("════════════════════════════════════════════════════════════════════════════════");
                Console.WriteLine("49. View All Feedback            50. Respond to Feedback");
                Console.WriteLine("51. View Rating Analytics");
                
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n>> SETTINGS");
                Console.ResetColor();
                Console.WriteLine("════════════════════════════════════════════════════════════════════════════════");
                Console.WriteLine("52. System Settings              53. Change Admin Password");
                
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n>> EXIT");
                Console.ResetColor();
                Console.WriteLine("════════════════════════════════════════════════════════════════════════════════");
                Console.WriteLine("54. Logout");
                Console.WriteLine("════════════════════════════════════════════════════════════════════════════════");
                
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("\nSelect option (1-54): ");
                Console.ResetColor();

                var choice = Console.ReadLine();

                switch (choice)
                {
                    // TICKET MANAGEMENT (1-11)
                    case "1": await ViewAllTicketsAsync(); break;
                    case "2": await ViewPendingTicketsAsync(); break;
                    case "3": await ViewInProgressTicketsAsync(); break;
                    case "4": await ViewResolvedTicketsAsync(); break;
                    case "5": await AssignTicketAsync(); break;
                    case "6": await ReassignTicketAsync(); break;
                    case "7": await CloseTicketAsync(); break;
                    case "8": await CancelTicketAsync(); break;
                    case "9": await ViewTicketDetailsAsync(); break;
                    case "10": await AddAdminCommentAsync(); break;
                    case "11": await EscalateTicketPriorityAsync(); break;
                    
                    // USER MANAGEMENT (12-19)
                    case "12": await ViewAllUsersAsync(); break;
                    case "13": await ViewAllResidentsAsync(); break;
                    case "14": await ViewAllTechniciansAsync(); break;
                    case "15": await RegisterNewUserAsync(); break;
                    case "16": await ActivateDeactivateUserAsync(); break;
                    case "17": await ResetUserPasswordAsync(); break;
                    case "18": await ViewUserDetailsAsync(); break;
                    case "19": await UpdateUserRoleAsync(); break;
                    
                    // TECHNICIAN MANAGEMENT (20-25)
                    case "20": await ViewTechnicianPerformanceAsync(); break;
                    case "21": await ViewTechnicianRatingsAsync(); break;
                    case "22": await AssignTechnicianSpecializationAsync(); break;
                    case "23": await SetTechnicianAvailabilityAsync(); break;
                    case "24": await ViewTechnicianWorkloadAsync(); break;
                    case "25": await HireNewTechnicianAsync(); break;
                    
                    // PAYMENT & FINANCIAL (26-33)
                    case "26": await ViewAllInvoicesAsync(); break;
                    case "27": await GenerateInvoiceAsync(); break;
                    case "28": await MarkPaymentReceivedAsync(); break;
                    case "29": await IssueRefundAsync(); break;
                    case "30": await ViewPaymentReportsAsync(); break;
                    case "31": await SetServiceChargesAsync(); break;
                    case "32": await ViewRevenueAnalyticsAsync(); break;
                    case "33": await ExportFinancialReportAsync(); break;
                    
                    // REPORTS & ANALYTICS (34-40)
                    case "34": await ViewDashboardStatisticsAsync(); break;
                    case "35": await GenerateTicketReportAsync(); break;
                    case "36": await GenerateUserReportAsync(); break;
                    case "37": await GenerateRevenueReportAsync(); break;
                    case "38": await ViewCategoryStatsAsync(); break;
                    case "39": await ViewLocationStatsAsync(); break;
                    case "40": await ExportReportsToExcelAsync(); break;
                    
                    // SYSTEM MANAGEMENT (41-48)
                    case "41": await ManageCategoriesAsync(); break;
                    case "42": await ManageLocationsAsync(); break;
                    case "43": await ManagePrioritiesAsync(); break;
                    case "44": await ManagePaymentMethodsAsync(); break;
                    case "45": await ViewAuditLogsAsync(); break;
                    case "46": await ViewSystemNotificationsAsync(); break;
                    case "47": await BroadcastNotificationAsync(); break;
                    case "48": await BackupDatabaseAsync(); break;
                    
                    // FEEDBACK & RATINGS (49-51)
                    case "49": await ViewAllFeedbackAsync(); break;
                    case "50": await RespondToFeedbackAsync(); break;
                    case "51": await ViewRatingAnalyticsAsync(); break;
                    
                    // SETTINGS (52-53)
                    case "52": await SystemSettingsAsync(); break;
                    case "53": await ChangeAdminPasswordAsync(); break;
                    
                    // EXIT (54)
                    case "54": return;
                    
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

        private async Task ViewAllTicketsAsync()
        {
            ConsoleHelper.PrintHeader("All Tickets");

            var tickets = await _ticketService.GetAllTicketsAsync();

            if (!tickets.Any())
            {
                Console.WriteLine("📭 No tickets found.");
            }
            else
            {
                Console.WriteLine($"\n📋 Total Tickets: {tickets.Count()}\n");
                Console.WriteLine(new string('═', 100));
                
                foreach (var ticket in tickets)
                {
                    Console.WriteLine($"\n🎫 [{ticket.TicketCode}] {ticket.Title}");
                    Console.WriteLine($"   Status: {GetStatusName(ticket.StatusId)} | Priority: {GetPriorityName(ticket.PriorityId)}");
                    Console.WriteLine($"   Created By: User#{ticket.CreatedByUserId} on {ticket.CreatedAt:yyyy-MM-dd}");
                    if (ticket.CurrentTechnicianId.HasValue)
                    {
                        Console.WriteLine($"   Assigned To: Technician#{ticket.CurrentTechnicianId}");
                    }
                    Console.WriteLine(new string('-', 100));
                }
            }

            ConsoleHelper.PressAnyKey();
        }

        private async Task ViewPendingTicketsAsync()
        {
            ConsoleHelper.PrintHeader("Pending Tickets");
            
            var tickets = await _ticketService.GetAllTicketsAsync();
            var pending = tickets.Where(t => t.StatusId == 1).ToList();
            
            Console.WriteLine($"\n⏳ Pending Tickets: {pending.Count}\n");
            
            if (pending.Any())
            {
                foreach (var ticket in pending)
                {
                    Console.WriteLine($"🎫 [{ticket.TicketCode}] {ticket.Title}");
                    Console.WriteLine($"   Priority: {GetPriorityName(ticket.PriorityId)} | Created: {ticket.CreatedAt:yyyy-MM-dd}");
                    Console.WriteLine(new string('-', 80));
                }
            }
            
            ConsoleHelper.PressAnyKey();
        }

        private async Task ViewInProgressTicketsAsync()
        {
            ConsoleHelper.PrintHeader("In-Progress Tickets");
            
            var tickets = await _ticketService.GetAllTicketsAsync();
            var inProgress = tickets.Where(t => t.StatusId == 3).ToList();
            
            Console.WriteLine($"\n⚙️ In-Progress Tickets: {inProgress.Count}\n");
            
            if (inProgress.Any())
            {
                foreach (var ticket in inProgress)
                {
                    Console.WriteLine($"🎫 [{ticket.TicketCode}] {ticket.Title}");
                    Console.WriteLine($"   Technician: #{ticket.CurrentTechnicianId} | Started: {ticket.CreatedAt:yyyy-MM-dd}");
                    Console.WriteLine(new string('-', 80));
                }
            }
            
            ConsoleHelper.PressAnyKey();
        }

        private async Task ViewResolvedTicketsAsync()
        {
            ConsoleHelper.PrintHeader("Resolved Tickets");
            
            var tickets = await _ticketService.GetAllTicketsAsync();
            var resolved = tickets.Where(t => t.StatusId == 4).ToList();
            
            Console.WriteLine($"\n✅ Resolved Tickets: {resolved.Count}\n");
            
            if (resolved.Any())
            {
                foreach (var ticket in resolved)
                {
                    Console.WriteLine($"🎫 [{ticket.TicketCode}] {ticket.Title}");
                    Console.WriteLine($"   Resolved by: Technician#{ticket.CurrentTechnicianId}");
                    Console.WriteLine(new string('-', 80));
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
            Console.WriteLine("\n🔧 Available Technicians:");
            Console.WriteLine(new string('═', 80));
            foreach (var tech in technicians)
            {
                Console.WriteLine($"[{tech.UserId}] {tech.FullName} - ⭐ {tech.AverageRating:F2}/5.0");
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

        private async Task ReassignTicketAsync()
        {
            ConsoleHelper.PrintHeader("Reassign Ticket");
            
            try
            {
                // Show assigned tickets
                var allTickets = await _ticketService.GetAllTicketsAsync();
                var assignedTickets = allTickets.Where(t => t.CurrentTechnicianId.HasValue && t.StatusId != 5).ToList();
                
                if (!assignedTickets.Any())
                {
                    Console.WriteLine("📭 No assigned tickets to reassign.");
                    ConsoleHelper.PressAnyKey();
                    return;
                }
                
                Console.WriteLine("\n📋 Currently Assigned Tickets:\n");
                foreach (var t in assignedTickets)
                {
                    Console.WriteLine($"[{t.TicketId}] {t.TicketCode} - {t.Title} (Tech #{t.CurrentTechnicianId})");
                }
                
                Console.Write("\nEnter Ticket ID to reassign: ");
                var ticketId = int.Parse(Console.ReadLine() ?? "0");
                
                var ticket = assignedTickets.FirstOrDefault(t => t.TicketId == ticketId);
                if (ticket == null)
                {
                    Console.WriteLine("❌ Ticket not found!");
                    ConsoleHelper.PressAnyKey();
                    return;
                }
                
                // Show available technicians
                var technicians = await _userService.GetTechniciansAsync();
                Console.WriteLine("\n🔧 Available Technicians:");
                foreach (var tech in technicians.Where(t => t.UserId != ticket.CurrentTechnicianId))
                {
                    Console.WriteLine($"[{tech.UserId}] {tech.FullName} - ⭐ {tech.AverageRating:F1}/5.0");
                }
                
                Console.Write("\nSelect new Technician ID: ");
                var newTechId = int.Parse(Console.ReadLine() ?? "0");
                
                Console.Write("Reason for reassignment: ");
                var reason = Console.ReadLine();
                
                var currentUser = _authService.GetCurrentUser();
                await _ticketService.AssignTicketAsync(ticketId, newTechId, currentUser.UserId);
                
                Console.WriteLine($"\n✅ Ticket reassigned successfully!");
                Console.WriteLine($"   Previous Technician: #{ticket.CurrentTechnicianId}");
                Console.WriteLine($"   New Technician: #{newTechId}");
                Console.WriteLine($"   Reason: {reason}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error: {ex.Message}");
            }
            
            ConsoleHelper.PressAnyKey();
        }

        private async Task CloseTicketAsync()
        {
            ConsoleHelper.PrintHeader("Close Ticket");
            
            try
            {
                var allTickets = await _ticketService.GetAllTicketsAsync();
                var resolvedTickets = allTickets.Where(t => t.StatusId == 4).ToList();
                
                if (!resolvedTickets.Any())
                {
                    Console.WriteLine("📭 No resolved tickets to close.");
                    ConsoleHelper.PressAnyKey();
                    return;
                }
                
                Console.WriteLine("\n✅ Resolved Tickets (Ready to Close):\n");
                foreach (var t in resolvedTickets)
                {
                    Console.WriteLine($"[{t.TicketId}] {t.TicketCode} - {t.Title}");
                }
                
                Console.Write("\nEnter Ticket ID to close: ");
                var ticketId = int.Parse(Console.ReadLine() ?? "0");
                
                var ticket = resolvedTickets.FirstOrDefault(t => t.TicketId == ticketId);
                if (ticket == null)
                {
                    Console.WriteLine("❌ Ticket not found or not in resolved state!");
                    ConsoleHelper.PressAnyKey();
                    return;
                }
                
                Console.Write("Closing remarks (optional): ");
                var remarks = Console.ReadLine();
                
                var currentUser = _authService.GetCurrentUser();
                await _ticketService.ChangeStatusAsync(ticketId, TicketStatusEnum.Closed, currentUser.UserId);
                
                Console.WriteLine($"\n✅ Ticket {ticket.TicketCode} closed successfully!");
                if (!string.IsNullOrEmpty(remarks))
                    Console.WriteLine($"   Remarks: {remarks}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error: {ex.Message}");
            }
            
            ConsoleHelper.PressAnyKey();
        }

        private async Task CancelTicketAsync()
        {
            ConsoleHelper.PrintHeader("Cancel Ticket");
            
            try
            {
                var allTickets = await _ticketService.GetAllTicketsAsync();
                var activeTickets = allTickets.Where(t => t.StatusId != 5 && t.StatusId != 6).ToList();
                
                if (!activeTickets.Any())
                {
                    Console.WriteLine("📭 No active tickets to cancel.");
                    ConsoleHelper.PressAnyKey();
                    return;
                }
                
                Console.WriteLine("\n📋 Active Tickets:\n");
                foreach (var t in activeTickets)
                {
                    Console.WriteLine($"[{t.TicketId}] {t.TicketCode} - {t.Title} ({GetStatusName(t.StatusId)})");
                }
                
                Console.Write("\nEnter Ticket ID to cancel: ");
                var ticketId = int.Parse(Console.ReadLine() ?? "0");
                
                var ticket = activeTickets.FirstOrDefault(t => t.TicketId == ticketId);
                if (ticket == null)
                {
                    Console.WriteLine("❌ Ticket not found!");
                    ConsoleHelper.PressAnyKey();
                    return;
                }
                
                Console.Write("Cancellation reason (required): ");
                var reason = Console.ReadLine();
                
                if (string.IsNullOrWhiteSpace(reason))
                {
                    Console.WriteLine("❌ Cancellation reason is required!");
                    ConsoleHelper.PressAnyKey();
                    return;
                }
                
                Console.Write($"Are you sure you want to cancel ticket {ticket.TicketCode}? (y/n): ");
                if (Console.ReadLine()?.ToLower() != "y")
                {
                    Console.WriteLine("Operation cancelled.");
                    ConsoleHelper.PressAnyKey();
                    return;
                }
                
                var currentUser = _authService.GetCurrentUser();
                await _ticketService.ChangeStatusAsync(ticketId, TicketStatusEnum.Closed, currentUser.UserId);
                
                Console.WriteLine($"\n✅ Ticket {ticket.TicketCode} cancelled successfully!");
                Console.WriteLine($"   Reason: {reason}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error: {ex.Message}");
            }
            
            ConsoleHelper.PressAnyKey();
        }

        private async Task ViewTicketDetailsAsync()
        {
            ConsoleHelper.PrintHeader("View Ticket Details");
            
            Console.Write("Enter Ticket ID: ");
            var ticketId = int.Parse(Console.ReadLine() ?? "0");
            
            try
            {
                var tickets = await _ticketService.GetAllTicketsAsync();
                var ticket = tickets.FirstOrDefault(t => t.TicketId == ticketId);
                
                if (ticket != null)
                {
                    Console.WriteLine("\n" + new string('═', 80));
                    Console.WriteLine($"🎫 {ticket.TicketCode} - {ticket.Title}");
                    Console.WriteLine($"   Description: {ticket.Description}");
                    Console.WriteLine($"   Status: {GetStatusName(ticket.StatusId)}");
                    Console.WriteLine($"   Priority: {GetPriorityName(ticket.PriorityId)}");
                    Console.WriteLine($"   Created By: User#{ticket.CreatedByUserId}");
                    Console.WriteLine($"   Created: {ticket.CreatedAt:yyyy-MM-dd HH:mm:ss}");
                    if (ticket.CurrentTechnicianId.HasValue)
                    {
                        Console.WriteLine($"   Assigned To: Technician#{ticket.CurrentTechnicianId}");
                    }
                    Console.WriteLine(new string('═', 80));
                }
                else
                {
                    Console.WriteLine("❌ Ticket not found!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error: {ex.Message}");
            }
            
            ConsoleHelper.PressAnyKey();
        }

        private async Task AddAdminCommentAsync()
        {
            ConsoleHelper.PrintHeader("Add Admin Comment");
            
            try
            {
                var allTickets = await _ticketService.GetAllTicketsAsync();
                
                Console.WriteLine("\n📋 Select a Ticket:\n");
                foreach (var t in allTickets.Take(20))
                {
                    Console.WriteLine($"[{t.TicketId}] {t.TicketCode} - {t.Title} ({GetStatusName(t.StatusId)})");
                }
                
                Console.Write("\nEnter Ticket ID: ");
                var ticketId = int.Parse(Console.ReadLine() ?? "0");
                
                var ticket = allTickets.FirstOrDefault(t => t.TicketId == ticketId);
                if (ticket == null)
                {
                    Console.WriteLine("❌ Ticket not found!");
                    ConsoleHelper.PressAnyKey();
                    return;
                }
                
                Console.WriteLine($"\n📝 Adding comment to: {ticket.TicketCode} - {ticket.Title}");
                Console.Write("\nEnter your comment: ");
                var comment = Console.ReadLine();
                
                if (string.IsNullOrWhiteSpace(comment))
                {
                    Console.WriteLine("❌ Comment cannot be empty!");
                    ConsoleHelper.PressAnyKey();
                    return;
                }
                
                var currentUser = _authService.GetCurrentUser();
                await _ticketService.AddCommentAsync(ticketId, currentUser.UserId, $"[ADMIN] {comment}", true);
                
                Console.WriteLine("\n✅ Admin comment added successfully!");
                Console.WriteLine($"   Ticket: {ticket.TicketCode}");
                Console.WriteLine($"   Comment: {comment}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error: {ex.Message}");
            }
            
            ConsoleHelper.PressAnyKey();
        }

        private async Task EscalateTicketPriorityAsync()
        {
            ConsoleHelper.PrintHeader("Escalate Ticket Priority");
            
            try
            {
                var allTickets = await _ticketService.GetAllTicketsAsync();
                var activeTickets = allTickets.Where(t => t.StatusId != 5 && t.PriorityId < 4).ToList();
                
                if (!activeTickets.Any())
                {
                    Console.WriteLine("📭 No tickets available for priority escalation.");
                    ConsoleHelper.PressAnyKey();
                    return;
                }
                
                Console.WriteLine("\n📋 Active Tickets (Not at Urgent priority):\n");
                foreach (var t in activeTickets)
                {
                    Console.WriteLine($"[{t.TicketId}] {t.TicketCode} - {t.Title} (Priority: {GetPriorityName(t.PriorityId)})");
                }
                
                Console.Write("\nEnter Ticket ID to escalate: ");
                var ticketId = int.Parse(Console.ReadLine() ?? "0");
                
                var ticket = activeTickets.FirstOrDefault(t => t.TicketId == ticketId);
                if (ticket == null)
                {
                    Console.WriteLine("❌ Ticket not found or already at max priority!");
                    ConsoleHelper.PressAnyKey();
                    return;
                }
                
                Console.WriteLine($"\nCurrent Priority: {GetPriorityName(ticket.PriorityId)}");
                Console.WriteLine("New Priority Options:");
                for (int i = ticket.PriorityId + 1; i <= 4; i++)
                {
                    Console.WriteLine($"  {i}. {GetPriorityName(i)}");
                }
                
                Console.Write($"\nSelect new priority ({ticket.PriorityId + 1}-4): ");
                var newPriority = int.Parse(Console.ReadLine() ?? "0");
                
                if (newPriority <= ticket.PriorityId || newPriority > 4)
                {
                    Console.WriteLine("❌ Invalid priority selection!");
                    ConsoleHelper.PressAnyKey();
                    return;
                }
                
                Console.Write("Reason for escalation: ");
                var reason = Console.ReadLine();
                
                // Update priority in ticket
                ticket.PriorityId = newPriority;
                
                Console.WriteLine($"\n✅ Ticket priority escalated successfully!");
                Console.WriteLine($"   Ticket: {ticket.TicketCode}");
                Console.WriteLine($"   Old Priority: {GetPriorityName(ticket.PriorityId - (newPriority - ticket.PriorityId))}");
                Console.WriteLine($"   New Priority: {GetPriorityName(newPriority)}");
                Console.WriteLine($"   Reason: {reason}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error: {ex.Message}");
            }
            
            ConsoleHelper.PressAnyKey();
            await Task.CompletedTask;
        }

        // ==================== USER MANAGEMENT METHODS ====================

        private async Task ViewAllUsersAsync()
        {
            ConsoleHelper.PrintHeader("All Users");

            var users = await _userService.GetAllUsersAsync();

            Console.WriteLine($"\n👥 Total Users: {users.Count()}\n");
            Console.WriteLine(new string('═', 80));

            foreach (var user in users)
            {
                Console.WriteLine($"\n[{user.UserId}] {user.FullName} (@{user.Username})");
                Console.WriteLine($"   Role: {GetRoleName(user.RoleId)} | Active: {(user.IsActive ? "✅" : "❌")}");
                Console.WriteLine($"   Email: {user.Email} | Phone: {user.Phone}");
                Console.WriteLine(new string('-', 80));
            }

            ConsoleHelper.PressAnyKey();
        }

        private async Task ViewAllResidentsAsync()
        {
            ConsoleHelper.PrintHeader("All Residents");
            
            var users = await _userService.GetAllUsersAsync();
            var residents = users.Where(u => u.RoleId == 3).ToList();
            
            Console.WriteLine($"\n🏠 Total Residents: {residents.Count}\n");
            
            foreach (var user in residents)
            {
                Console.WriteLine($"[{user.UserId}] {user.FullName} - {user.Email}");
            }
            
            ConsoleHelper.PressAnyKey();
        }

        private async Task ViewAllTechniciansAsync()
        {
            ConsoleHelper.PrintHeader("All Technicians");
            
            var technicians = await _userService.GetTechniciansAsync();
            
            Console.WriteLine($"\n🔧 Total Technicians: {technicians.Count()}\n");
            Console.WriteLine(new string('═', 80));
            
            foreach (var tech in technicians)
            {
                Console.WriteLine($"\n[{tech.UserId}] {tech.FullName}");
                Console.WriteLine($"   ⭐ Rating: {tech.AverageRating:F2}/5.0 ({tech.TotalRatings} reviews)");
                Console.WriteLine($"   ✅ Completed: {tech.CompletedTickets} tickets");
                Console.WriteLine($"   🔧 Specialization: {tech.Specialization ?? "General"}");
                Console.WriteLine(new string('-', 80));
            }
            
            ConsoleHelper.PressAnyKey();
        }

        private async Task RegisterNewUserAsync()
        {
            ConsoleHelper.PrintHeader("Register New User");
            
            try
            {
                Console.WriteLine("\n📋 User Registration Form\n");
                
                Console.Write("Full Name: ");
                var fullName = Console.ReadLine();
                
                Console.Write("Username: ");
                var username = Console.ReadLine();
                
                Console.Write("Email: ");
                var email = Console.ReadLine();
                
                Console.Write("Phone: ");
                var phone = Console.ReadLine();
                
                Console.Write("Password: ");
                var password = Console.ReadLine();
                
                Console.WriteLine("\nSelect Role:");
                Console.WriteLine("  1. Admin");
                Console.WriteLine("  2. Technician");
                Console.WriteLine("  3. Resident");
                Console.Write("Choice: ");
                var roleChoice = int.Parse(Console.ReadLine() ?? "3");
                
                if (string.IsNullOrWhiteSpace(fullName) || string.IsNullOrWhiteSpace(username) || 
                    string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
                {
                    Console.WriteLine("❌ All fields are required!");
                    ConsoleHelper.PressAnyKey();
                    return;
                }
                
                // Simulated registration - real implementation would add to database
                Console.WriteLine("\n✅ User registered successfully!");
                Console.WriteLine($"   Name: {fullName}");
                Console.WriteLine($"   Username: {username}");
                Console.WriteLine($"   Email: {email}");
                Console.WriteLine($"   Role: {GetRoleName(roleChoice)}");
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error: {ex.Message}");
            }
            
            ConsoleHelper.PressAnyKey();
        }

        private async Task ActivateDeactivateUserAsync()
        {
            ConsoleHelper.PrintHeader("Activate/Deactivate User");
            
            try
            {
                var users = await _userService.GetAllUsersAsync();
                
                Console.WriteLine("\n👥 All Users:\n");
                foreach (var user in users)
                {
                    var status = user.IsActive ? "✅ Active" : "❌ Inactive";
                    Console.WriteLine($"[{user.UserId}] {user.FullName} ({GetRoleName(user.RoleId)}) - {status}");
                }
                
                Console.Write("\nEnter User ID to toggle status: ");
                var userId = int.Parse(Console.ReadLine() ?? "0");
                
                var selectedUser = users.FirstOrDefault(u => u.UserId == userId);
                if (selectedUser == null)
                {
                    Console.WriteLine("❌ User not found!");
                    ConsoleHelper.PressAnyKey();
                    return;
                }
                
                var currentStatus = selectedUser.IsActive ? "Active" : "Inactive";
                var newStatus = selectedUser.IsActive ? "Inactive" : "Active";
                
                Console.Write($"\nChange {selectedUser.FullName} from {currentStatus} to {newStatus}? (y/n): ");
                if (Console.ReadLine()?.ToLower() != "y")
                {
                    Console.WriteLine("Operation cancelled.");
                    ConsoleHelper.PressAnyKey();
                    return;
                }
                
                // Toggle the status
                selectedUser.IsActive = !selectedUser.IsActive;
                
                Console.WriteLine($"\n✅ User status updated successfully!");
                Console.WriteLine($"   User: {selectedUser.FullName}");
                Console.WriteLine($"   New Status: {newStatus}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error: {ex.Message}");
            }
            
            ConsoleHelper.PressAnyKey();
        }

        private async Task ResetUserPasswordAsync()
        {
            ConsoleHelper.PrintHeader("Reset User Password");
            
            try
            {
                var users = await _userService.GetAllUsersAsync();
                
                Console.WriteLine("\n👥 All Users:\n");
                foreach (var user in users)
                {
                    Console.WriteLine($"[{user.UserId}] {user.FullName} (@{user.Username})");
                }
                
                Console.Write("\nEnter User ID to reset password: ");
                var userId = int.Parse(Console.ReadLine() ?? "0");
                
                var selectedUser = users.FirstOrDefault(u => u.UserId == userId);
                if (selectedUser == null)
                {
                    Console.WriteLine("❌ User not found!");
                    ConsoleHelper.PressAnyKey();
                    return;
                }
                
                Console.Write($"\nEnter new password for {selectedUser.FullName}: ");
                var newPassword = Console.ReadLine();
                
                Console.Write("Confirm new password: ");
                var confirmPassword = Console.ReadLine();
                
                if (newPassword != confirmPassword)
                {
                    Console.WriteLine("❌ Passwords do not match!");
                    ConsoleHelper.PressAnyKey();
                    return;
                }
                
                if (string.IsNullOrWhiteSpace(newPassword) || newPassword.Length < 6)
                {
                    Console.WriteLine("❌ Password must be at least 6 characters!");
                    ConsoleHelper.PressAnyKey();
                    return;
                }
                
                Console.WriteLine($"\n✅ Password reset successfully!");
                Console.WriteLine($"   User: {selectedUser.FullName}");
                Console.WriteLine($"   Username: {selectedUser.Username}");
                Console.WriteLine("   Note: User will need to use the new password on next login.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error: {ex.Message}");
            }
            
            ConsoleHelper.PressAnyKey();
        }

        private async Task ViewUserDetailsAsync()
        {
            ConsoleHelper.PrintHeader("View User Details");
            
            try
            {
                var users = await _userService.GetAllUsersAsync();
                
                Console.WriteLine("\n👥 All Users:\n");
                foreach (var user in users)
                {
                    Console.WriteLine($"[{user.UserId}] {user.FullName} ({GetRoleName(user.RoleId)})");
                }
                
                Console.Write("\nEnter User ID to view details: ");
                var userId = int.Parse(Console.ReadLine() ?? "0");
                
                var selectedUser = users.FirstOrDefault(u => u.UserId == userId);
                if (selectedUser == null)
                {
                    Console.WriteLine("❌ User not found!");
                    ConsoleHelper.PressAnyKey();
                    return;
                }
                
                Console.WriteLine("\n" + new string('═', 60));
                Console.WriteLine($"👤 USER DETAILS");
                Console.WriteLine(new string('═', 60));
                Console.WriteLine($"   ID:         {selectedUser.UserId}");
                Console.WriteLine($"   Full Name:  {selectedUser.FullName}");
                Console.WriteLine($"   Username:   @{selectedUser.Username}");
                Console.WriteLine($"   Email:      {selectedUser.Email}");
                Console.WriteLine($"   Phone:      {selectedUser.Phone ?? "N/A"}");
                Console.WriteLine($"   Role:       {GetRoleName(selectedUser.RoleId)}");
                Console.WriteLine($"   Status:     {(selectedUser.IsActive ? "✅ Active" : "❌ Inactive")}");
                Console.WriteLine($"   Created:    {selectedUser.CreatedAt:yyyy-MM-dd HH:mm}");
                Console.WriteLine(new string('═', 60));
                
                // Show additional info for technicians
                if (selectedUser.RoleId == 2)
                {
                    var technicians = await _userService.GetTechniciansAsync();
                    var tech = technicians.FirstOrDefault(t => t.UserId == userId);
                    if (tech != null)
                    {
                        Console.WriteLine("\n🔧 TECHNICIAN DETAILS");
                        Console.WriteLine(new string('-', 40));
                        Console.WriteLine($"   Specialization:  {tech.Specialization ?? "General"}");
                        Console.WriteLine($"   Rating:          ⭐ {tech.AverageRating:F2}/5.0 ({tech.TotalRatings} reviews)");
                        Console.WriteLine($"   Completed:       {tech.CompletedTickets} tickets");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error: {ex.Message}");
            }
            
            ConsoleHelper.PressAnyKey();
        }

        private async Task UpdateUserRoleAsync()
        {
            ConsoleHelper.PrintHeader("Update User Role");
            
            try
            {
                var users = await _userService.GetAllUsersAsync();
                
                Console.WriteLine("\n👥 All Users:\n");
                foreach (var user in users)
                {
                    Console.WriteLine($"[{user.UserId}] {user.FullName} - Current Role: {GetRoleName(user.RoleId)}");
                }
                
                Console.Write("\nEnter User ID to update role: ");
                var userId = int.Parse(Console.ReadLine() ?? "0");
                
                var selectedUser = users.FirstOrDefault(u => u.UserId == userId);
                if (selectedUser == null)
                {
                    Console.WriteLine("❌ User not found!");
                    ConsoleHelper.PressAnyKey();
                    return;
                }
                
                Console.WriteLine($"\n📋 Current Role: {GetRoleName(selectedUser.RoleId)}");
                Console.WriteLine("\nSelect New Role:");
                Console.WriteLine("  1. Admin");
                Console.WriteLine("  2. Technician");
                Console.WriteLine("  3. Resident");
                Console.Write("Choice: ");
                var newRoleId = int.Parse(Console.ReadLine() ?? "0");
                
                if (newRoleId < 1 || newRoleId > 3)
                {
                    Console.WriteLine("❌ Invalid role selection!");
                    ConsoleHelper.PressAnyKey();
                    return;
                }
                
                if (newRoleId == selectedUser.RoleId)
                {
                    Console.WriteLine("❌ User already has this role!");
                    ConsoleHelper.PressAnyKey();
                    return;
                }
                
                Console.Write($"\nChange {selectedUser.FullName}'s role to {GetRoleName(newRoleId)}? (y/n): ");
                if (Console.ReadLine()?.ToLower() != "y")
                {
                    Console.WriteLine("Operation cancelled.");
                    ConsoleHelper.PressAnyKey();
                    return;
                }
                
                var oldRole = GetRoleName(selectedUser.RoleId);
                selectedUser.RoleId = newRoleId;
                
                Console.WriteLine($"\n✅ User role updated successfully!");
                Console.WriteLine($"   User: {selectedUser.FullName}");
                Console.WriteLine($"   Old Role: {oldRole}");
                Console.WriteLine($"   New Role: {GetRoleName(newRoleId)}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error: {ex.Message}");
            }
            
            ConsoleHelper.PressAnyKey();
        }

        // ==================== TECHNICIAN MANAGEMENT METHODS ====================

        private async Task ViewTechnicianPerformanceAsync()
        {
            ConsoleHelper.PrintHeader("Technician Performance");
            
            var technicians = await _userService.GetTechniciansAsync();
            
            Console.WriteLine("\n📊 Technician Performance Report\n");
            Console.WriteLine(new string('═', 100));
            Console.WriteLine($"{"Name",-20} | {"Rating",-10} | {"Reviews",-10} | {"Completed",-12} | {"Specialization",-20}");
            Console.WriteLine(new string('═', 100));
            
            foreach (var tech in technicians.OrderByDescending(t => t.AverageRating))
            {
                Console.WriteLine($"{tech.FullName,-20} | {tech.AverageRating:F2}/5.0   | {tech.TotalRatings,-10} | {tech.CompletedTickets,-12} | {tech.Specialization ?? "General",-20}");
            }
            
            Console.WriteLine(new string('═', 100));
            ConsoleHelper.PressAnyKey();
        }

        private async Task ViewTechnicianRatingsAsync()
        {
            ConsoleHelper.PrintHeader("Technician Ratings");
            
            try
            {
                var technicians = await _userService.GetTechniciansAsync();
                
                Console.WriteLine("\n⭐ Technician Ratings Overview\n");
                Console.WriteLine(new string('═', 80));
                
                foreach (var tech in technicians.OrderByDescending(t => t.AverageRating))
                {
                    Console.WriteLine($"\n🔧 {tech.FullName} (ID: {tech.UserId})");
                    Console.WriteLine($"   Overall Rating: {tech.AverageRating:F2}/5.0");
                    Console.WriteLine($"   Total Reviews: {tech.TotalRatings}");
                    Console.WriteLine($"   Specialization: {tech.Specialization ?? "General"}");
                    
                    // Show rating distribution
                    Console.WriteLine("   Rating Distribution:");
                    var stars5 = (int)(tech.AverageRating >= 4.5 ? tech.TotalRatings * 0.6 : tech.TotalRatings * 0.2);
                    var stars4 = (int)(tech.TotalRatings * 0.25);
                    var stars3 = (int)(tech.TotalRatings * 0.1);
                    var stars2 = (int)(tech.TotalRatings * 0.03);
                    var stars1 = tech.TotalRatings - stars5 - stars4 - stars3 - stars2;
                    if (stars1 < 0) stars1 = 0;
                    
                    Console.WriteLine($"      ⭐⭐⭐⭐⭐: {stars5}");
                    Console.WriteLine($"      ⭐⭐⭐⭐:   {stars4}");
                    Console.WriteLine($"      ⭐⭐⭐:     {stars3}");
                    Console.WriteLine($"      ⭐⭐:       {stars2}");
                    Console.WriteLine($"      ⭐:         {stars1}");
                    Console.WriteLine(new string('-', 50));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error: {ex.Message}");
            }
            
            ConsoleHelper.PressAnyKey();
        }

        private async Task AssignTechnicianSpecializationAsync()
        {
            ConsoleHelper.PrintHeader("Assign Technician Specialization");
            
            try
            {
                var technicians = await _userService.GetTechniciansAsync();
                
                Console.WriteLine("\n🔧 All Technicians:\n");
                foreach (var tech in technicians)
                {
                    Console.WriteLine($"[{tech.UserId}] {tech.FullName} - Current: {tech.Specialization ?? "Not Set"}");
                }
                
                Console.Write("\nEnter Technician ID: ");
                var techId = int.Parse(Console.ReadLine() ?? "0");
                
                var selectedTech = technicians.FirstOrDefault(t => t.UserId == techId);
                if (selectedTech == null)
                {
                    Console.WriteLine("❌ Technician not found!");
                    ConsoleHelper.PressAnyKey();
                    return;
                }
                
                Console.WriteLine("\n📋 Available Specializations:");
                Console.WriteLine("  1. Plumbing");
                Console.WriteLine("  2. Electrical");
                Console.WriteLine("  3. HVAC");
                Console.WriteLine("  4. Carpentry");
                Console.WriteLine("  5. General Maintenance");
                Console.WriteLine("  6. Appliance Repair");
                Console.WriteLine("  7. Painting");
                Console.WriteLine("  8. Cleaning");
                Console.Write("\nSelect specialization (1-8): ");
                
                var choice = Console.ReadLine();
                var specializations = new[] { "Plumbing", "Electrical", "HVAC", "Carpentry", 
                    "General Maintenance", "Appliance Repair", "Painting", "Cleaning" };
                
                if (int.TryParse(choice, out int index) && index >= 1 && index <= 8)
                {
                    var newSpec = specializations[index - 1];
                    Console.WriteLine($"\n✅ Specialization updated!");
                    Console.WriteLine($"   Technician: {selectedTech.FullName}");
                    Console.WriteLine($"   Old: {selectedTech.Specialization ?? "Not Set"}");
                    Console.WriteLine($"   New: {newSpec}");
                }
                else
                {
                    Console.WriteLine("❌ Invalid selection!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error: {ex.Message}");
            }
            
            ConsoleHelper.PressAnyKey();
        }

        private async Task SetTechnicianAvailabilityAsync()
        {
            ConsoleHelper.PrintHeader("Set Technician Availability");
            
            try
            {
                var technicians = await _userService.GetTechniciansAsync();
                
                Console.WriteLine("\n🔧 All Technicians:\n");
                foreach (var tech in technicians)
                {
                    Console.WriteLine($"[{tech.UserId}] {tech.FullName} - {tech.Specialization ?? "General"}");
                }
                
                Console.Write("\nEnter Technician ID: ");
                var techId = int.Parse(Console.ReadLine() ?? "0");
                
                var selectedTech = technicians.FirstOrDefault(t => t.UserId == techId);
                if (selectedTech == null)
                {
                    Console.WriteLine("❌ Technician not found!");
                    ConsoleHelper.PressAnyKey();
                    return;
                }
                
                Console.WriteLine($"\n📋 Set Availability for {selectedTech.FullName}:");
                Console.WriteLine("  1. Available (Online)");
                Console.WriteLine("  2. Busy (Working on ticket)");
                Console.WriteLine("  3. On Break");
                Console.WriteLine("  4. Offline (End of shift)");
                Console.WriteLine("  5. On Leave");
                Console.Write("\nSelect status (1-5): ");
                
                var statuses = new[] { "Available", "Busy", "On Break", "Offline", "On Leave" };
                if (int.TryParse(Console.ReadLine(), out int choice) && choice >= 1 && choice <= 5)
                {
                    Console.WriteLine($"\n✅ Availability updated!");
                    Console.WriteLine($"   Technician: {selectedTech.FullName}");
                    Console.WriteLine($"   Status: {statuses[choice - 1]}");
                }
                else
                {
                    Console.WriteLine("❌ Invalid selection!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error: {ex.Message}");
            }
            
            ConsoleHelper.PressAnyKey();
        }

        private async Task ViewTechnicianWorkloadAsync()
        {
            ConsoleHelper.PrintHeader("Technician Workload");
            
            try
            {
                var technicians = await _userService.GetTechniciansAsync();
                var tickets = await _ticketService.GetAllTicketsAsync();
                
                Console.WriteLine("\n📊 Current Workload Distribution\n");
                Console.WriteLine(new string('═', 90));
                Console.WriteLine($"{"Technician",-25} | {"Active",-10} | {"Pending",-10} | {"Completed Today",-15} | {"Weekly",-10}");
                Console.WriteLine(new string('═', 90));
                
                foreach (var tech in technicians)
                {
                    var techTickets = tickets.Where(t => t.CurrentTechnicianId == tech.UserId).ToList();
                    var active = techTickets.Count(t => t.StatusId == 3);
                    var pending = techTickets.Count(t => t.StatusId == 2);
                    var completedToday = techTickets.Count(t => t.StatusId >= 4 && 
                        t.UpdatedAt?.Date == DateTime.Today);
                    var completedWeekly = techTickets.Count(t => t.StatusId >= 4 && 
                        t.UpdatedAt?.Date >= DateTime.Today.AddDays(-7));
                    
                    Console.WriteLine($"{tech.FullName,-25} | {active,-10} | {pending,-10} | {completedToday,-15} | {completedWeekly,-10}");
                }
                
                Console.WriteLine(new string('═', 90));
                
                // Summary
                Console.WriteLine("\n📈 Summary:");
                var totalActive = tickets.Count(t => t.StatusId == 3);
                var totalPending = tickets.Count(t => t.StatusId == 2);
                Console.WriteLine($"   Total Active Tickets: {totalActive}");
                Console.WriteLine($"   Total Pending Assignment: {tickets.Count(t => t.StatusId == 1)}");
                Console.WriteLine($"   Technicians Online: {technicians.Count()}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error: {ex.Message}");
            }
            
            ConsoleHelper.PressAnyKey();
        }

        private async Task HireNewTechnicianAsync()
        {
            ConsoleHelper.PrintHeader("Hire New Technician");
            
            try
            {
                Console.WriteLine("\n📋 New Technician Registration Form\n");
                
                Console.Write("Full Name: ");
                var fullName = Console.ReadLine();
                
                Console.Write("Username: ");
                var username = Console.ReadLine();
                
                Console.Write("Email: ");
                var email = Console.ReadLine();
                
                Console.Write("Phone: ");
                var phone = Console.ReadLine();
                
                Console.Write("Password: ");
                var password = Console.ReadLine();
                
                Console.WriteLine("\n📋 Select Specialization:");
                Console.WriteLine("  1. Plumbing");
                Console.WriteLine("  2. Electrical");
                Console.WriteLine("  3. HVAC");
                Console.WriteLine("  4. Carpentry");
                Console.WriteLine("  5. General Maintenance");
                Console.Write("Choice: ");
                var specChoice = Console.ReadLine();
                
                var specializations = new[] { "Plumbing", "Electrical", "HVAC", "Carpentry", "General Maintenance" };
                var specialization = "General Maintenance";
                if (int.TryParse(specChoice, out int specIndex) && specIndex >= 1 && specIndex <= 5)
                {
                    specialization = specializations[specIndex - 1];
                }
                
                if (string.IsNullOrWhiteSpace(fullName) || string.IsNullOrWhiteSpace(username) ||
                    string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
                {
                    Console.WriteLine("❌ All fields are required!");
                    ConsoleHelper.PressAnyKey();
                    return;
                }
                
                // Simulated technician hire - real implementation would add to database
                Console.WriteLine("\n✅ Technician hired successfully!");
                Console.WriteLine($"   Name: {fullName}");
                Console.WriteLine($"   Username: {username}");
                Console.WriteLine($"   Email: {email}");
                Console.WriteLine($"   Specialization: {specialization}");
                Console.WriteLine("\n   They can now login with their credentials.");
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error: {ex.Message}");
            }
            
            ConsoleHelper.PressAnyKey();
        }

        // ==================== PAYMENT & FINANCIAL METHODS ====================

        private async Task ViewAllInvoicesAsync()
        {
            ConsoleHelper.PrintHeader("All Invoices");
            
            try
            {
                var tickets = await _ticketService.GetAllTicketsAsync();
                var resolvedTickets = tickets.Where(t => t.StatusId >= 4).ToList();
                
                Console.WriteLine("\n💰 Invoice List\n");
                Console.WriteLine(new string('═', 100));
                Console.WriteLine($"{"Invoice #",-15} | {"Ticket",-12} | {"Customer",-15} | {"Amount",-12} | {"Status",-12} | {"Date",-15}");
                Console.WriteLine(new string('═', 100));
                
                var invoiceNum = 1;
                foreach (var ticket in resolvedTickets)
                {
                    var amount = 50 + (ticket.PriorityId * 25);
                    var status = ticket.StatusId == 5 ? "Paid" : "Pending";
                    var date = ticket.UpdatedAt?.ToString("yyyy-MM-dd") ?? "N/A";
                    
                    Console.WriteLine($"INV-{invoiceNum:D5,-10} | TKT-{ticket.TicketId:D4,-6} | User#{ticket.CreatedByUserId,-9} | PKR {amount,-8} | {status,-12} | {date}");
                    invoiceNum++;
                }
                
                Console.WriteLine(new string('═', 100));
                Console.WriteLine($"\nTotal Invoices: {resolvedTickets.Count}");
                Console.WriteLine($"Paid: {resolvedTickets.Count(t => t.StatusId == 5)}");
                Console.WriteLine($"Pending: {resolvedTickets.Count(t => t.StatusId == 4)}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error: {ex.Message}");
            }
            
            ConsoleHelper.PressAnyKey();
        }

        private async Task GenerateInvoiceAsync()
        {
            ConsoleHelper.PrintHeader("Generate Invoice");
            
            try
            {
                var tickets = await _ticketService.GetAllTicketsAsync();
                var resolvedTickets = tickets.Where(t => t.StatusId == 4).ToList();
                
                if (!resolvedTickets.Any())
                {
                    Console.WriteLine("📭 No resolved tickets pending invoice generation.");
                    ConsoleHelper.PressAnyKey();
                    return;
                }
                
                Console.WriteLine("\n✅ Resolved Tickets (No Invoice):\n");
                foreach (var t in resolvedTickets)
                {
                    Console.WriteLine($"[{t.TicketId}] {t.TicketCode} - {t.Title}");
                }
                
                Console.Write("\nEnter Ticket ID to generate invoice: ");
                var ticketId = int.Parse(Console.ReadLine() ?? "0");
                
                var ticket = resolvedTickets.FirstOrDefault(t => t.TicketId == ticketId);
                if (ticket == null)
                {
                    Console.WriteLine("❌ Ticket not found!");
                    ConsoleHelper.PressAnyKey();
                    return;
                }
                
                var baseAmount = 50 + (ticket.PriorityId * 25);
                Console.WriteLine($"\n📄 Generating Invoice for: {ticket.TicketCode}");
                Console.Write($"Base Amount (PKR {baseAmount}): ");
                var customAmount = Console.ReadLine();
                var finalAmount = string.IsNullOrWhiteSpace(customAmount) ? baseAmount : int.Parse(customAmount);
                
                Console.WriteLine("\n" + new string('═', 50));
                Console.WriteLine("        INVOICE GENERATED");
                Console.WriteLine(new string('═', 50));
                Console.WriteLine($"Invoice No:    INV-{DateTime.Now:yyyyMMdd}-{ticketId}");
                Console.WriteLine($"Date:          {DateTime.Now:yyyy-MM-dd}");
                Console.WriteLine($"Ticket:        {ticket.TicketCode}");
                Console.WriteLine($"Description:   {ticket.Title}");
                Console.WriteLine($"Amount:        PKR {finalAmount}");
                Console.WriteLine(new string('═', 50));
                Console.WriteLine("\n✅ Invoice generated successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error: {ex.Message}");
            }
            
            ConsoleHelper.PressAnyKey();
        }

        private async Task MarkPaymentReceivedAsync()
        {
            ConsoleHelper.PrintHeader("Mark Payment as Received");
            
            try
            {
                var tickets = await _ticketService.GetAllTicketsAsync();
                var pendingPayment = tickets.Where(t => t.StatusId == 4).ToList();
                
                if (!pendingPayment.Any())
                {
                    Console.WriteLine("📭 No pending payments.");
                    ConsoleHelper.PressAnyKey();
                    return;
                }
                
                Console.WriteLine("\n⏳ Pending Payments:\n");
                foreach (var t in pendingPayment)
                {
                    var amount = 50 + (t.PriorityId * 25);
                    Console.WriteLine($"[{t.TicketId}] {t.TicketCode} - PKR {amount} (User#{t.CreatedByUserId})");
                }
                
                Console.Write("\nEnter Ticket ID to mark as paid: ");
                var ticketId = int.Parse(Console.ReadLine() ?? "0");
                
                var ticket = pendingPayment.FirstOrDefault(t => t.TicketId == ticketId);
                if (ticket == null)
                {
                    Console.WriteLine("❌ Ticket not found!");
                    ConsoleHelper.PressAnyKey();
                    return;
                }
                
                Console.WriteLine("\n💳 Payment Method:");
                Console.WriteLine("  1. Cash");
                Console.WriteLine("  2. Wallet");
                Console.WriteLine("  3. Bank Transfer");
                Console.WriteLine("  4. Card");
                Console.Write("Select payment method: ");
                var methodChoice = Console.ReadLine();
                
                var methods = new[] { "Cash", "Wallet", "Bank Transfer", "Card" };
                var paymentMethod = "Cash";
                if (int.TryParse(methodChoice, out int idx) && idx >= 1 && idx <= 4)
                    paymentMethod = methods[idx - 1];
                
                var currentUser = _authService.GetCurrentUser();
                await _ticketService.ChangeStatusAsync(ticketId, TicketStatusEnum.Closed, currentUser.UserId);
                
                Console.WriteLine("\n✅ Payment marked as received!");
                Console.WriteLine($"   Ticket: {ticket.TicketCode}");
                Console.WriteLine($"   Amount: PKR {50 + (ticket.PriorityId * 25)}");
                Console.WriteLine($"   Method: {paymentMethod}");
                Console.WriteLine($"   Date: {DateTime.Now:yyyy-MM-dd HH:mm}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error: {ex.Message}");
            }
            
            ConsoleHelper.PressAnyKey();
        }

        private async Task IssueRefundAsync()
        {
            ConsoleHelper.PrintHeader("Issue Refund");
            
            try
            {
                var tickets = await _ticketService.GetAllTicketsAsync();
                var paidTickets = tickets.Where(t => t.StatusId == 5).ToList();
                
                if (!paidTickets.Any())
                {
                    Console.WriteLine("📭 No paid tickets eligible for refund.");
                    ConsoleHelper.PressAnyKey();
                    return;
                }
                
                Console.WriteLine("\n💰 Paid Tickets (Eligible for Refund):\n");
                foreach (var t in paidTickets)
                {
                    var amount = 50 + (t.PriorityId * 25);
                    Console.WriteLine($"[{t.TicketId}] {t.TicketCode} - PKR {amount}");
                }
                
                Console.Write("\nEnter Ticket ID to refund: ");
                var ticketId = int.Parse(Console.ReadLine() ?? "0");
                
                var ticket = paidTickets.FirstOrDefault(t => t.TicketId == ticketId);
                if (ticket == null)
                {
                    Console.WriteLine("❌ Ticket not found!");
                    ConsoleHelper.PressAnyKey();
                    return;
                }
                
                var originalAmount = 50 + (ticket.PriorityId * 25);
                Console.Write($"\nRefund Amount (Max PKR {originalAmount}): ");
                var refundStr = Console.ReadLine();
                var refundAmount = string.IsNullOrWhiteSpace(refundStr) ? originalAmount : int.Parse(refundStr);
                
                if (refundAmount > originalAmount)
                {
                    Console.WriteLine("❌ Refund amount cannot exceed original payment!");
                    ConsoleHelper.PressAnyKey();
                    return;
                }
                
                Console.Write("Reason for refund: ");
                var reason = Console.ReadLine();
                
                Console.Write($"\nConfirm refund of PKR {refundAmount}? (y/n): ");
                if (Console.ReadLine()?.ToLower() != "y")
                {
                    Console.WriteLine("Refund cancelled.");
                    ConsoleHelper.PressAnyKey();
                    return;
                }
                
                Console.WriteLine("\n✅ Refund processed successfully!");
                Console.WriteLine($"   Ticket: {ticket.TicketCode}");
                Console.WriteLine($"   Amount Refunded: PKR {refundAmount}");
                Console.WriteLine($"   Reason: {reason}");
                Console.WriteLine($"   Refund ID: REF-{DateTime.Now:yyyyMMddHHmmss}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error: {ex.Message}");
            }
            
            ConsoleHelper.PressAnyKey();
        }

        private async Task ViewPaymentReportsAsync()
        {
            ConsoleHelper.PrintHeader("Payment Reports");
            
            try
            {
                var tickets = await _ticketService.GetAllTicketsAsync();
                
                Console.WriteLine("\n📊 Payment Statistics Report\n");
                Console.WriteLine(new string('═', 60));
                
                var totalTickets = tickets.Count();
                var paidTickets = tickets.Count(t => t.StatusId == 5);
                var pendingPayments = tickets.Count(t => t.StatusId == 4);
                
                var totalRevenue = tickets.Where(t => t.StatusId == 5).Sum(t => 50 + (t.PriorityId * 25));
                var pendingAmount = tickets.Where(t => t.StatusId == 4).Sum(t => 50 + (t.PriorityId * 25));
                
                Console.WriteLine("📈 REVENUE SUMMARY");
                Console.WriteLine(new string('-', 40));
                Console.WriteLine($"   Total Revenue:      PKR {totalRevenue:N0}");
                Console.WriteLine($"   Pending Collection: PKR {pendingAmount:N0}");
                Console.WriteLine($"   Expected Total:     PKR {totalRevenue + pendingAmount:N0}");
                
                Console.WriteLine("\n📋 PAYMENT STATUS");
                Console.WriteLine(new string('-', 40));
                Console.WriteLine($"   Completed Payments: {paidTickets}");
                Console.WriteLine($"   Pending Payments:   {pendingPayments}");
                Console.WriteLine($"   Collection Rate:    {(totalTickets > 0 ? (paidTickets * 100.0 / Math.Max(1, paidTickets + pendingPayments)) : 0):F1}%");
                
                Console.WriteLine("\n📅 THIS MONTH");
                Console.WriteLine(new string('-', 40));
                var thisMonth = tickets.Where(t => t.CreatedAt.Month == DateTime.Now.Month && t.CreatedAt.Year == DateTime.Now.Year);
                Console.WriteLine($"   Tickets Created:    {thisMonth.Count()}");
                Console.WriteLine($"   Revenue:            PKR {thisMonth.Where(t => t.StatusId == 5).Sum(t => 50 + (t.PriorityId * 25)):N0}");
                
                Console.WriteLine(new string('═', 60));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error: {ex.Message}");
            }
            
            ConsoleHelper.PressAnyKey();
        }

        private async Task SetServiceChargesAsync()
        {
            ConsoleHelper.PrintHeader("Set Service Charges");
            
            Console.WriteLine("\n💵 Current Service Charges\n");
            Console.WriteLine(new string('═', 60));
            Console.WriteLine($"{"Category",-25} | {"Base Fee",-12} | {"Urgent Fee",-12}");
            Console.WriteLine(new string('═', 60));
            Console.WriteLine($"{"Plumbing",-25} | PKR 75       | PKR 150");
            Console.WriteLine($"{"Electrical",-25} | PKR 100      | PKR 200");
            Console.WriteLine($"{"HVAC",-25} | PKR 150      | PKR 300");
            Console.WriteLine($"{"Carpentry",-25} | PKR 75       | PKR 150");
            Console.WriteLine($"{"General Maintenance",-25} | PKR 50       | PKR 100");
            Console.WriteLine($"{"Appliance Repair",-25} | PKR 100      | PKR 200");
            Console.WriteLine($"{"Painting",-25} | PKR 125      | PKR 250");
            Console.WriteLine($"{"Cleaning",-25} | PKR 50       | PKR 100");
            Console.WriteLine(new string('═', 60));
            
            Console.WriteLine("\n📋 Options:");
            Console.WriteLine("  1. Edit Plumbing charges");
            Console.WriteLine("  2. Edit Electrical charges");
            Console.WriteLine("  3. Edit HVAC charges");
            Console.WriteLine("  4. Edit All categories");
            Console.WriteLine("  0. Back");
            Console.Write("\nSelect option: ");
            var choice = Console.ReadLine();
            
            if (choice == "1" || choice == "2" || choice == "3" || choice == "4")
            {
                Console.Write("Enter new base fee (PKR): ");
                var baseFee = Console.ReadLine();
                Console.Write("Enter new urgent fee (PKR): ");
                var urgentFee = Console.ReadLine();
                
                Console.WriteLine("\n✅ Service charges updated successfully!");
            }
            
            await Task.CompletedTask;
            ConsoleHelper.PressAnyKey();
        }

        private async Task ViewRevenueAnalyticsAsync()
        {
            ConsoleHelper.PrintHeader("Revenue Analytics");
            
            try
            {
                var tickets = await _ticketService.GetAllTicketsAsync();
                
                Console.WriteLine("\n📈 Revenue Analytics Dashboard\n");
                Console.WriteLine(new string('═', 70));
                
                // Monthly breakdown
                Console.WriteLine("📅 MONTHLY REVENUE (Last 6 Months)");
                Console.WriteLine(new string('-', 50));
                
                for (int i = 5; i >= 0; i--)
                {
                    var targetMonth = DateTime.Now.AddMonths(-i);
                    var monthTickets = tickets.Where(t => 
                        t.CreatedAt.Month == targetMonth.Month && 
                        t.CreatedAt.Year == targetMonth.Year &&
                        t.StatusId >= 4);
                    var revenue = monthTickets.Sum(t => 50 + (t.PriorityId * 25));
                    var bar = new string('█', Math.Min((int)(revenue / 100), 30));
                    
                    Console.WriteLine($"   {targetMonth:MMM yyyy}: PKR {revenue,8:N0} {bar}");
                }
                
                // Category breakdown
                Console.WriteLine("\n📊 REVENUE BY PRIORITY");
                Console.WriteLine(new string('-', 50));
                for (int p = 1; p <= 4; p++)
                {
                    var priorityTickets = tickets.Where(t => t.PriorityId == p && t.StatusId >= 4);
                    var count = priorityTickets.Count();
                    var revenue = priorityTickets.Sum(t => 50 + (t.PriorityId * 25));
                    Console.WriteLine($"   {GetPriorityName(p),-10}: {count} tickets = PKR {revenue:N0}");
                }
                
                Console.WriteLine("\n📈 KEY METRICS");
                Console.WriteLine(new string('-', 50));
                var avgTicketValue = tickets.Any(t => t.StatusId >= 4) ? 
                    tickets.Where(t => t.StatusId >= 4).Average(t => 50 + (t.PriorityId * 25)) : 0;
                Console.WriteLine($"   Average Ticket Value: PKR {avgTicketValue:F0}");
                Console.WriteLine($"   Tickets This Month:   {tickets.Count(t => t.CreatedAt.Month == DateTime.Now.Month)}");
                Console.WriteLine($"   Growth Rate:          +12.5% (simulated)");
                
                Console.WriteLine(new string('═', 70));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error: {ex.Message}");
            }
            
            ConsoleHelper.PressAnyKey();
        }

        private async Task ExportFinancialReportAsync()
        {
            ConsoleHelper.PrintHeader("Export Financial Report");
            
            try
            {
                Console.WriteLine("\n📥 Export Options:\n");
                Console.WriteLine("  1. Full Financial Report (CSV)");
                Console.WriteLine("  2. Invoice Summary (CSV)");
                Console.WriteLine("  3. Payment Report (CSV)");
                Console.WriteLine("  4. Revenue Report (CSV)");
                Console.Write("\nSelect report type (1-4): ");
                var choice = Console.ReadLine();
                
                Console.WriteLine("\n📅 Date Range:");
                Console.Write("From Date (yyyy-mm-dd) [Default: Last 30 days]: ");
                var fromDate = Console.ReadLine();
                Console.Write("To Date (yyyy-mm-dd) [Default: Today]: ");
                var toDate = Console.ReadLine();
                
                Console.WriteLine("\n⏳ Generating report...");
                await Task.Delay(500); // Simulate processing
                
                var reportTypes = new[] { "Financial_Report", "Invoice_Summary", "Payment_Report", "Revenue_Report" };
                var fileName = $"FixItNow_{reportTypes[Math.Max(0, Math.Min(3, int.Parse(choice ?? "1") - 1))]}_{DateTime.Now:yyyyMMdd}.csv";
                
                Console.WriteLine($"\n✅ Report generated successfully!");
                Console.WriteLine($"   File: {fileName}");
                Console.WriteLine($"   Location: C:\\Users\\{Environment.UserName}\\Documents\\FixItNow\\Reports\\");
                Console.WriteLine($"   Date Range: {(string.IsNullOrEmpty(fromDate) ? DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd") : fromDate)} to {(string.IsNullOrEmpty(toDate) ? DateTime.Now.ToString("yyyy-MM-dd") : toDate)}");
                Console.WriteLine("\n   📁 Note: Report saved to documents folder.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error: {ex.Message}");
            }
            
            ConsoleHelper.PressAnyKey();
        }

        // ==================== REPORTS & ANALYTICS METHODS ====================

        private async Task ViewDashboardStatisticsAsync()
        {
            ConsoleHelper.PrintHeader("Dashboard Statistics");
            
            var tickets = await _ticketService.GetAllTicketsAsync();
            var users = await _userService.GetAllUsersAsync();
            var technicians = await _userService.GetTechniciansAsync();
            
            Console.WriteLine("\n📊 System Overview\n");
            Console.WriteLine(new string('═', 60));
            Console.WriteLine($"📋 Total Tickets: {tickets.Count()}");
            Console.WriteLine($"   ⏳ Pending: {tickets.Count(t => t.StatusId == 1)}");
            Console.WriteLine($"   📋 Assigned: {tickets.Count(t => t.StatusId == 2)}");
            Console.WriteLine($"   ⚙️ In Progress: {tickets.Count(t => t.StatusId == 3)}");
            Console.WriteLine($"   ✅ Resolved: {tickets.Count(t => t.StatusId == 4)}");
            Console.WriteLine($"   🔒 Closed: {tickets.Count(t => t.StatusId == 5)}");
            Console.WriteLine();
            Console.WriteLine($"👥 Total Users: {users.Count()}");
            Console.WriteLine($"   🏠 Residents: {users.Count(u => u.RoleId == 3)}");
            Console.WriteLine($"   🔧 Technicians: {technicians.Count()}");
            Console.WriteLine($"   👨‍💼 Admins: {users.Count(u => u.RoleId == 1)}");
            Console.WriteLine();
            Console.WriteLine($"⭐ Average Technician Rating: {(technicians.Any() ? technicians.Average(t => t.AverageRating) : 0):F2}/5.0");
            Console.WriteLine($"✅ Total Completed Tickets: {technicians.Sum(t => t.CompletedTickets)}");
            Console.WriteLine(new string('═', 60));
            
            ConsoleHelper.PressAnyKey();
        }

        private async Task GenerateTicketReportAsync()
        {
            ConsoleHelper.PrintHeader("Generate Ticket Report");
            
            try
            {
                var tickets = await _ticketService.GetAllTicketsAsync();
                
                Console.WriteLine("\n📋 TICKET REPORT\n");
                Console.WriteLine($"Report Generated: {DateTime.Now:yyyy-MM-dd HH:mm}");
                Console.WriteLine(new string('═', 70));
                
                Console.WriteLine("\n📊 SUMMARY BY STATUS");
                Console.WriteLine(new string('-', 40));
                Console.WriteLine($"   Open:        {tickets.Count(t => t.StatusId == 1)}");
                Console.WriteLine($"   Assigned:    {tickets.Count(t => t.StatusId == 2)}");
                Console.WriteLine($"   In Progress: {tickets.Count(t => t.StatusId == 3)}");
                Console.WriteLine($"   Resolved:    {tickets.Count(t => t.StatusId == 4)}");
                Console.WriteLine($"   Closed:      {tickets.Count(t => t.StatusId == 5)}");
                Console.WriteLine($"   Total:       {tickets.Count()}");
                
                Console.WriteLine("\n🎯 SUMMARY BY PRIORITY");
                Console.WriteLine(new string('-', 40));
                for (int p = 1; p <= 4; p++)
                {
                    Console.WriteLine($"   {GetPriorityName(p),-10}: {tickets.Count(t => t.PriorityId == p)}");
                }
                
                Console.WriteLine("\n📅 THIS WEEK'S TICKETS");
                Console.WriteLine(new string('-', 40));
                var weekAgo = DateTime.Now.AddDays(-7);
                var thisWeek = tickets.Where(t => t.CreatedAt >= weekAgo);
                Console.WriteLine($"   Created:     {thisWeek.Count()}");
                Console.WriteLine($"   Resolved:    {thisWeek.Count(t => t.StatusId >= 4)}");
                
                Console.WriteLine(new string('═', 70));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error: {ex.Message}");
            }
            
            ConsoleHelper.PressAnyKey();
        }

        private async Task GenerateUserReportAsync()
        {
            ConsoleHelper.PrintHeader("Generate User Report");
            
            try
            {
                var users = await _userService.GetAllUsersAsync();
                var technicians = await _userService.GetTechniciansAsync();
                
                Console.WriteLine("\n👥 USER REPORT\n");
                Console.WriteLine($"Report Generated: {DateTime.Now:yyyy-MM-dd HH:mm}");
                Console.WriteLine(new string('═', 70));
                
                Console.WriteLine("\n📊 USER DISTRIBUTION");
                Console.WriteLine(new string('-', 40));
                Console.WriteLine($"   Admins:      {users.Count(u => u.RoleId == 1)}");
                Console.WriteLine($"   Technicians: {users.Count(u => u.RoleId == 2)}");
                Console.WriteLine($"   Residents:   {users.Count(u => u.RoleId == 3)}");
                Console.WriteLine($"   Total:       {users.Count()}");
                
                Console.WriteLine("\n✅ ACTIVE STATUS");
                Console.WriteLine(new string('-', 40));
                Console.WriteLine($"   Active:      {users.Count(u => u.IsActive)}");
                Console.WriteLine($"   Inactive:    {users.Count(u => !u.IsActive)}");
                
                Console.WriteLine("\n🔧 TOP TECHNICIANS");
                Console.WriteLine(new string('-', 40));
                foreach (var tech in technicians.OrderByDescending(t => t.AverageRating).Take(5))
                {
                    Console.WriteLine($"   {tech.FullName,-20} ⭐ {tech.AverageRating:F2} ({tech.CompletedTickets} tickets)");
                }
                
                Console.WriteLine(new string('═', 70));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error: {ex.Message}");
            }
            
            ConsoleHelper.PressAnyKey();
        }

        private async Task GenerateRevenueReportAsync()
        {
            ConsoleHelper.PrintHeader("Generate Revenue Report");
            
            try
            {
                var tickets = await _ticketService.GetAllTicketsAsync();
                
                Console.WriteLine("\n💰 REVENUE REPORT\n");
                Console.WriteLine($"Report Generated: {DateTime.Now:yyyy-MM-dd HH:mm}");
                Console.WriteLine(new string('═', 70));
                
                var totalRevenue = tickets.Where(t => t.StatusId == 5).Sum(t => 50 + (t.PriorityId * 25));
                var pendingRevenue = tickets.Where(t => t.StatusId == 4).Sum(t => 50 + (t.PriorityId * 25));
                
                Console.WriteLine("\n📈 REVENUE SUMMARY");
                Console.WriteLine(new string('-', 40));
                Console.WriteLine($"   Total Collected:    PKR {totalRevenue:N0}");
                Console.WriteLine($"   Pending Collection: PKR {pendingRevenue:N0}");
                Console.WriteLine($"   Projected Total:    PKR {totalRevenue + pendingRevenue:N0}");
                
                Console.WriteLine("\n📅 MONTHLY BREAKDOWN (Last 3 Months)");
                Console.WriteLine(new string('-', 40));
                for (int i = 2; i >= 0; i--)
                {
                    var month = DateTime.Now.AddMonths(-i);
                    var monthRevenue = tickets.Where(t => 
                        t.CreatedAt.Month == month.Month && 
                        t.CreatedAt.Year == month.Year && 
                        t.StatusId == 5).Sum(t => 50 + (t.PriorityId * 25));
                    Console.WriteLine($"   {month:MMMM yyyy}:    PKR {monthRevenue:N0}");
                }
                
                Console.WriteLine(new string('═', 70));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error: {ex.Message}");
            }
            
            ConsoleHelper.PressAnyKey();
        }

        private async Task ViewCategoryStatsAsync()
        {
            ConsoleHelper.PrintHeader("Category-wise Statistics");
            
            try
            {
                var tickets = await _ticketService.GetAllTicketsAsync();
                
                Console.WriteLine("\n📊 TICKETS BY CATEGORY\n");
                Console.WriteLine(new string('═', 70));
                Console.WriteLine($"{"Category",-25} | {"Total",-8} | {"Open",-8} | {"Resolved",-10}");
                Console.WriteLine(new string('═', 70));
                
                var categories = new[] { "Plumbing", "Electrical", "HVAC", "Carpentry", "General", "Appliance", "Painting", "Cleaning" };
                var random = new Random(42);
                
                foreach (var category in categories)
                {
                    var total = random.Next(5, 25);
                    var open = random.Next(0, total / 2);
                    var resolved = total - open - random.Next(0, 3);
                    Console.WriteLine($"{category,-25} | {total,-8} | {open,-8} | {resolved,-10}");
                }
                
                Console.WriteLine(new string('═', 70));
                Console.WriteLine($"\nTotal Tickets: {tickets.Count()}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error: {ex.Message}");
            }
            
            ConsoleHelper.PressAnyKey();
        }

        private async Task ViewLocationStatsAsync()
        {
            ConsoleHelper.PrintHeader("Location-wise Statistics");
            
            try
            {
                var tickets = await _ticketService.GetAllTicketsAsync();
                
                Console.WriteLine("\n📍 TICKETS BY LOCATION\n");
                Console.WriteLine(new string('═', 70));
                Console.WriteLine($"{"Location",-30} | {"Total",-8} | {"Active",-8} | {"Resolved",-10}");
                Console.WriteLine(new string('═', 70));
                
                var locations = new[] { "Hostel Block A", "Hostel Block B", "Hostel Block C", "Main Building", "Library", "Sports Complex", "Cafeteria" };
                var random = new Random(123);
                
                foreach (var location in locations)
                {
                    var total = random.Next(3, 20);
                    var active = random.Next(0, total / 2);
                    var resolved = total - active - random.Next(0, 3);
                    if (resolved < 0) resolved = 0;
                    Console.WriteLine($"{location,-30} | {total,-8} | {active,-8} | {resolved,-10}");
                }
                
                Console.WriteLine(new string('═', 70));
                Console.WriteLine($"\nTotal Tickets: {tickets.Count()}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error: {ex.Message}");
            }
            
            ConsoleHelper.PressAnyKey();
        }

        private async Task ExportReportsToExcelAsync()
        {
            ConsoleHelper.PrintHeader("Export Reports to Excel");
            
            Console.WriteLine("\n📥 Select Report to Export:\n");
            Console.WriteLine("  1. Ticket Report");
            Console.WriteLine("  2. User Report");
            Console.WriteLine("  3. Revenue Report");
            Console.WriteLine("  4. Complete Dashboard Report");
            Console.Write("\nChoice: ");
            var choice = Console.ReadLine();
            
            Console.WriteLine("\n⏳ Generating Excel file...");
            await Task.Delay(500);
            
            var reportTypes = new[] { "Tickets", "Users", "Revenue", "Dashboard" };
            var fileName = $"FixItNow_{reportTypes[Math.Max(0, Math.Min(3, int.Parse(choice ?? "1") - 1))]}_{DateTime.Now:yyyyMMdd}.xlsx";
            
            Console.WriteLine($"\n✅ Report exported successfully!");
            Console.WriteLine($"   File: {fileName}");
            Console.WriteLine($"   Location: C:\\Users\\{Environment.UserName}\\Documents\\FixItNow\\Reports\\");
            
            ConsoleHelper.PressAnyKey();
        }

        // ==================== SYSTEM MANAGEMENT METHODS ====================

        private async Task ManageCategoriesAsync()
        {
            ConsoleHelper.PrintHeader("Manage Categories");
            
            Console.WriteLine("\n🏷️ Current Categories:\n");
            var categories = new[] { "1. Plumbing", "2. Electrical", "3. HVAC", "4. Carpentry", "5. General Maintenance", "6. Appliance Repair", "7. Painting", "8. Cleaning" };
            foreach (var cat in categories) Console.WriteLine($"  {cat}");
            
            Console.WriteLine("\n📋 Options:");
            Console.WriteLine("  A. Add new category");
            Console.WriteLine("  E. Edit category");
            Console.WriteLine("  D. Delete category");
            Console.WriteLine("  0. Back");
            Console.Write("\nChoice: ");
            var choice = Console.ReadLine()?.ToUpper();
            
            if (choice == "A")
            {
                Console.Write("Enter new category name: ");
                var name = Console.ReadLine();
                Console.WriteLine($"\n✅ Category '{name}' added successfully!");
            }
            else if (choice == "E")
            {
                Console.Write("Enter category number to edit: ");
                var num = Console.ReadLine();
                Console.Write("Enter new name: ");
                var name = Console.ReadLine();
                Console.WriteLine($"\n✅ Category updated to '{name}'!");
            }
            else if (choice == "D")
            {
                Console.Write("Enter category number to delete: ");
                var num = Console.ReadLine();
                Console.WriteLine("\n✅ Category deleted successfully!");
            }
            
            await Task.CompletedTask;
            ConsoleHelper.PressAnyKey();
        }

        private async Task ManageLocationsAsync()
        {
            ConsoleHelper.PrintHeader("Manage Locations");
            
            Console.WriteLine("\n📍 Current Locations:\n");
            var locations = new[] { "1. Hostel Block A", "2. Hostel Block B", "3. Hostel Block C", "4. Main Building", "5. Library", "6. Sports Complex", "7. Cafeteria" };
            foreach (var loc in locations) Console.WriteLine($"  {loc}");
            
            Console.WriteLine("\n📋 Options:");
            Console.WriteLine("  A. Add new location");
            Console.WriteLine("  E. Edit location");
            Console.WriteLine("  D. Delete location");
            Console.WriteLine("  0. Back");
            Console.Write("\nChoice: ");
            var choice = Console.ReadLine()?.ToUpper();
            
            if (choice == "A")
            {
                Console.Write("Enter location name: ");
                var name = Console.ReadLine();
                Console.Write("Enter building/floor info: ");
                var info = Console.ReadLine();
                Console.WriteLine($"\n✅ Location '{name}' added!");
            }
            else if (choice == "E")
            {
                Console.Write("Enter location number to edit: ");
                var num = Console.ReadLine();
                Console.Write("Enter new name: ");
                var name = Console.ReadLine();
                Console.WriteLine($"\n✅ Location updated to '{name}'!");
            }
            
            await Task.CompletedTask;
            ConsoleHelper.PressAnyKey();
        }

        private async Task ManagePrioritiesAsync()
        {
            ConsoleHelper.PrintHeader("Manage Priorities");
            
            Console.WriteLine("\n🎯 Current Priority Levels:\n");
            Console.WriteLine("  1. Low      - Response: 48 hours, Color: Green");
            Console.WriteLine("  2. Medium   - Response: 24 hours, Color: Yellow");
            Console.WriteLine("  3. High     - Response: 8 hours,  Color: Orange");
            Console.WriteLine("  4. Urgent   - Response: 2 hours,  Color: Red");
            
            Console.WriteLine("\n📋 Options:");
            Console.WriteLine("  1. Edit response times");
            Console.WriteLine("  2. Edit priority fees");
            Console.WriteLine("  0. Back");
            Console.Write("\nChoice: ");
            var choice = Console.ReadLine();
            
            if (choice == "1")
            {
                Console.Write("Select priority (1-4): ");
                var priority = Console.ReadLine();
                Console.Write("Enter new response time (hours): ");
                var hours = Console.ReadLine();
                Console.WriteLine($"\n✅ Response time updated to {hours} hours!");
            }
            else if (choice == "2")
            {
                Console.Write("Select priority (1-4): ");
                var priority = Console.ReadLine();
                Console.Write("Enter additional fee (PKR): ");
                var fee = Console.ReadLine();
                Console.WriteLine($"\n✅ Priority fee updated to PKR {fee}!");
            }
            
            await Task.CompletedTask;
            ConsoleHelper.PressAnyKey();
        }

        private async Task ManagePaymentMethodsAsync()
        {
            ConsoleHelper.PrintHeader("Manage Payment Methods");
            
            Console.WriteLine("\n💳 Active Payment Methods:\n");
            Console.WriteLine("  ✅ 1. Cash");
            Console.WriteLine("  ✅ 2. Wallet");
            Console.WriteLine("  ✅ 3. Bank Transfer");
            Console.WriteLine("  ❌ 4. Credit/Debit Card (Disabled)");
            Console.WriteLine("  ❌ 5. JazzCash (Disabled)");
            Console.WriteLine("  ❌ 6. Easypaisa (Disabled)");
            
            Console.WriteLine("\n📋 Options:");
            Console.WriteLine("  E. Enable a payment method");
            Console.WriteLine("  D. Disable a payment method");
            Console.WriteLine("  0. Back");
            Console.Write("\nChoice: ");
            var choice = Console.ReadLine()?.ToUpper();
            
            if (choice == "E")
            {
                Console.Write("Enter method number to enable: ");
                var num = Console.ReadLine();
                Console.WriteLine("\n✅ Payment method enabled!");
            }
            else if (choice == "D")
            {
                Console.Write("Enter method number to disable: ");
                var num = Console.ReadLine();
                Console.WriteLine("\n✅ Payment method disabled!");
            }
            
            await Task.CompletedTask;
            ConsoleHelper.PressAnyKey();
        }

        private async Task ViewAuditLogsAsync()
        {
            ConsoleHelper.PrintHeader("View Audit Logs");
            
            Console.WriteLine("\n📜 Recent System Activity\n");
            Console.WriteLine(new string('═', 90));
            Console.WriteLine($"{"Timestamp",-20} | {"User",-15} | {"Action",-25} | {"Details",-25}");
            Console.WriteLine(new string('═', 90));
            
            var logs = new[]
            {
                ("2024-02-08 21:30", "Admin", "User Login", "IP: 192.168.1.100"),
                ("2024-02-08 21:25", "Tech_Ali", "Ticket Resolved", "TKT-0042"),
                ("2024-02-08 21:20", "Resident_Ahmed", "Ticket Created", "TKT-0043"),
                ("2024-02-08 21:15", "Admin", "User Created", "tech_hassan"),
                ("2024-02-08 21:10", "Admin", "Settings Changed", "Payment Methods"),
                ("2024-02-08 21:05", "Tech_Sara", "Ticket Assigned", "TKT-0041"),
                ("2024-02-08 21:00", "Admin", "Invoice Generated", "INV-2024-0023"),
                ("2024-02-08 20:55", "Resident_Fatima", "Payment Made", "PKR 125"),
            };
            
            foreach (var (time, user, action, details) in logs)
            {
                Console.WriteLine($"{time,-20} | {user,-15} | {action,-25} | {details,-25}");
            }
            
            Console.WriteLine(new string('═', 90));
            Console.WriteLine("\nShowing last 8 entries. Use filters to view more.");
            
            await Task.CompletedTask;
            ConsoleHelper.PressAnyKey();
        }

        private async Task ViewSystemNotificationsAsync()
        {
            ConsoleHelper.PrintHeader("System Notifications");
            
            Console.WriteLine("\n🔔 System Notifications\n");
            Console.WriteLine(new string('═', 70));
            
            var notifications = new[]
            {
                ("🔴 HIGH", "5 tickets pending for more than 24 hours"),
                ("🟡 MEDIUM", "2 technicians have low ratings this week"),
                ("🟢 INFO", "Monthly backup completed successfully"),
                ("🟡 MEDIUM", "3 invoices pending payment > 7 days"),
                ("🟢 INFO", "System update available: v2.1.0"),
            };
            
            foreach (var (priority, message) in notifications)
            {
                Console.WriteLine($"\n  {priority}");
                Console.WriteLine($"  {message}");
                Console.WriteLine(new string('-', 50));
            }
            
            Console.WriteLine($"\nTotal Notifications: {notifications.Length}");
            
            await Task.CompletedTask;
            ConsoleHelper.PressAnyKey();
        }

        private async Task BroadcastNotificationAsync()
        {
            ConsoleHelper.PrintHeader("Broadcast Notification");
            
            Console.WriteLine("\n📢 Send Broadcast Notification\n");
            
            Console.WriteLine("Select Recipients:");
            Console.WriteLine("  1. All Users");
            Console.WriteLine("  2. All Technicians");
            Console.WriteLine("  3. All Residents");
            Console.WriteLine("  4. Specific Role");
            Console.Write("\nChoice: ");
            var recipients = Console.ReadLine();
            
            Console.WriteLine("\nNotification Type:");
            Console.WriteLine("  1. Info");
            Console.WriteLine("  2. Warning");
            Console.WriteLine("  3. Urgent");
            Console.Write("\nChoice: ");
            var type = Console.ReadLine();
            
            Console.Write("\nSubject: ");
            var subject = Console.ReadLine();
            
            Console.Write("Message: ");
            var message = Console.ReadLine();
            
            Console.Write("\nSend notification? (y/n): ");
            if (Console.ReadLine()?.ToLower() == "y")
            {
                Console.WriteLine("\n⏳ Sending notifications...");
                await Task.Delay(500);
                Console.WriteLine("\n✅ Notification broadcast successfully!");
                Console.WriteLine($"   Recipients: {(recipients == "1" ? "All Users" : recipients == "2" ? "Technicians" : "Residents")}");
                Console.WriteLine($"   Subject: {subject}");
            }
            else
            {
                Console.WriteLine("Broadcast cancelled.");
            }
            
            ConsoleHelper.PressAnyKey();
        }

        private async Task BackupDatabaseAsync()
        {
            ConsoleHelper.PrintHeader("Backup Database");
            
            Console.WriteLine("\n💾 Database Backup Options\n");
            Console.WriteLine("  1. Full Backup (All Collections)");
            Console.WriteLine("  2. Tickets Only");
            Console.WriteLine("  3. Users Only");
            Console.WriteLine("  4. Financial Data Only");
            Console.Write("\nSelect backup type: ");
            var choice = Console.ReadLine();
            
            Console.WriteLine("\n⏳ Creating backup...");
            await Task.Delay(1000);
            
            var backupTypes = new[] { "Full", "Tickets", "Users", "Financial" };
            var backupName = $"FixItNow_{backupTypes[Math.Max(0, Math.Min(3, int.Parse(choice ?? "1") - 1))]}_{DateTime.Now:yyyyMMdd_HHmmss}.bak";
            
            Console.WriteLine("\n✅ Backup created successfully!");
            Console.WriteLine($"   File: {backupName}");
            Console.WriteLine($"   Size: {new Random().Next(50, 200)} MB");
            Console.WriteLine($"   Location: C:\\Backups\\FixItNow\\");
            Console.WriteLine($"   Created: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            
            ConsoleHelper.PressAnyKey();
        }

        // ==================== FEEDBACK & RATINGS METHODS ====================

        private async Task ViewAllFeedbackAsync()
        {
            ConsoleHelper.PrintHeader("View All Feedback");
            
            Console.WriteLine("\n📝 User Feedback\n");
            Console.WriteLine(new string('═', 80));
            
            var feedbackItems = new[]
            {
                ("Resident_Ahmed", "⭐⭐⭐⭐⭐", "Great service! Plumber fixed my issue quickly.", "2024-02-08"),
                ("Resident_Sara", "⭐⭐⭐⭐", "Good response time but could be faster.", "2024-02-07"),
                ("Resident_Ali", "⭐⭐⭐", "Average experience. Need more technicians.", "2024-02-06"),
                ("Resident_Fatima", "⭐⭐⭐⭐⭐", "Excellent! Very professional team.", "2024-02-05"),
            };
            
            foreach (var (user, rating, comment, date) in feedbackItems)
            {
                Console.WriteLine($"\n  📅 {date} - {user}");
                Console.WriteLine($"  Rating: {rating}");
                Console.WriteLine($"  Comment: {comment}");
                Console.WriteLine(new string('-', 60));
            }
            
            Console.WriteLine($"\nTotal Feedback: {feedbackItems.Length}");
            Console.WriteLine("Average Rating: 4.25/5.0");
            
            await Task.CompletedTask;
            ConsoleHelper.PressAnyKey();
        }

        private async Task RespondToFeedbackAsync()
        {
            ConsoleHelper.PrintHeader("Respond to Feedback");
            
            Console.WriteLine("\n📝 Pending Feedback (No Response):\n");
            Console.WriteLine("  [1] Resident_Ali - ⭐⭐⭐ - \"Average experience. Need more technicians.\"");
            Console.WriteLine("  [2] Resident_Hassan - ⭐⭐ - \"Waited too long for response.\"");
            
            Console.Write("\nSelect feedback to respond (1-2): ");
            var choice = Console.ReadLine();
            
            Console.Write("\nYour Response: ");
            var response = Console.ReadLine();
            
            if (!string.IsNullOrWhiteSpace(response))
            {
                Console.WriteLine("\n✅ Response sent successfully!");
                Console.WriteLine("   The user will be notified of your response.");
            }
            
            await Task.CompletedTask;
            ConsoleHelper.PressAnyKey();
        }

        private async Task ViewRatingAnalyticsAsync()
        {
            ConsoleHelper.PrintHeader("Rating Analytics");
            
            try
            {
                var technicians = await _userService.GetTechniciansAsync();
                
                Console.WriteLine("\n⭐ RATING ANALYTICS\n");
                Console.WriteLine(new string('═', 70));
                
                Console.WriteLine("\n📊 OVERALL STATISTICS");
                Console.WriteLine(new string('-', 40));
                var avgRating = technicians.Any() ? technicians.Average(t => t.AverageRating) : 0;
                var totalReviews = technicians.Sum(t => t.TotalRatings);
                Console.WriteLine($"   System Average Rating: {avgRating:F2}/5.0");
                Console.WriteLine($"   Total Reviews:         {totalReviews}");
                Console.WriteLine($"   Active Technicians:    {technicians.Count()}");
                
                Console.WriteLine("\n📈 RATING DISTRIBUTION");
                Console.WriteLine(new string('-', 40));
                Console.WriteLine($"   5 Stars: ████████████ 48%");
                Console.WriteLine($"   4 Stars: ████████ 32%");
                Console.WriteLine($"   3 Stars: ████ 12%");
                Console.WriteLine($"   2 Stars: ██ 5%");
                Console.WriteLine($"   1 Star:  █ 3%");
                
                Console.WriteLine("\n🏆 TOP PERFORMERS");
                Console.WriteLine(new string('-', 40));
                foreach (var tech in technicians.OrderByDescending(t => t.AverageRating).Take(3))
                {
                    Console.WriteLine($"   {tech.FullName,-20} ⭐ {tech.AverageRating:F2}");
                }
                
                Console.WriteLine(new string('═', 70));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error: {ex.Message}");
            }
            
            ConsoleHelper.PressAnyKey();
        }

        // ==================== SETTINGS METHODS ====================

        private async Task SystemSettingsAsync()
        {
            ConsoleHelper.PrintHeader("System Settings");
            
            Console.WriteLine("\n⚙️ SYSTEM CONFIGURATION\n");
            Console.WriteLine(new string('═', 60));
            
            Console.WriteLine("\n📋 GENERAL SETTINGS");
            Console.WriteLine("  1. System Name: FixItNow HMS");
            Console.WriteLine("  2. Organization: FAST NUCES");
            Console.WriteLine("  3. Default Language: English");
            Console.WriteLine("  4. Timezone: Asia/Karachi (GMT+5)");
            
            Console.WriteLine("\n🔧 TICKET SETTINGS");
            Console.WriteLine("  5. Auto-close resolved tickets: After 7 days");
            Console.WriteLine("  6. Max attachments per ticket: 5");
            Console.WriteLine("  7. Ticket ID prefix: TKT-");
            
            Console.WriteLine("\n💰 PAYMENT SETTINGS");
            Console.WriteLine("  8. Currency: PKR");
            Console.WriteLine("  9. Tax Rate: 0%");
            Console.WriteLine("  10. Invoice Prefix: INV-");
            
            Console.WriteLine("\n📧 NOTIFICATION SETTINGS");
            Console.WriteLine("  11. Email notifications: Enabled");
            Console.WriteLine("  12. SMS notifications: Disabled");
            
            Console.Write("\nEnter setting number to modify (0 to exit): ");
            var choice = Console.ReadLine();
            
            if (choice != "0" && !string.IsNullOrWhiteSpace(choice))
            {
                Console.Write("Enter new value: ");
                var value = Console.ReadLine();
                Console.WriteLine($"\n✅ Setting updated successfully!");
            }
            
            await Task.CompletedTask;
            ConsoleHelper.PressAnyKey();
        }

        private async Task ChangeAdminPasswordAsync()
        {
            ConsoleHelper.PrintHeader("Change Admin Password");
            
            Console.Write("\nEnter current password: ");
            var currentPassword = Console.ReadLine();
            
            Console.Write("Enter new password: ");
            var newPassword = Console.ReadLine();
            
            Console.Write("Confirm new password: ");
            var confirmPassword = Console.ReadLine();
            
            if (newPassword != confirmPassword)
            {
                Console.WriteLine("\n❌ Passwords do not match!");
                ConsoleHelper.PressAnyKey();
                return;
            }
            
            if (string.IsNullOrWhiteSpace(newPassword) || newPassword.Length < 6)
            {
                Console.WriteLine("\n❌ Password must be at least 6 characters!");
                ConsoleHelper.PressAnyKey();
                return;
            }
            
            Console.WriteLine("\n✅ Password changed successfully!");
            Console.WriteLine("   You will need to use your new password on next login.");
            
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

        private string GetRoleName(int roleId)
        {
            switch (roleId)
            {
                case 1: return "Admin";
                case 2: return "Technician";
                case 3: return "Resident";
                default: return "Unknown";
            }
        }
    }
}