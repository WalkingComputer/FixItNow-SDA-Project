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
            Console.WriteLine("🔄 Feature Coming Soon!");
            Console.WriteLine("Reassign tickets to different technicians.");
            ConsoleHelper.PressAnyKey();
            await Task.CompletedTask;
        }

        private async Task CloseTicketAsync()
        {
            ConsoleHelper.PrintHeader("Close Ticket");
            Console.WriteLine("🔒 Feature Coming Soon!");
            Console.WriteLine("Close resolved tickets.");
            ConsoleHelper.PressAnyKey();
            await Task.CompletedTask;
        }

        private async Task CancelTicketAsync()
        {
            ConsoleHelper.PrintHeader("Cancel Ticket");
            Console.WriteLine("❌ Feature Coming Soon!");
            Console.WriteLine("Cancel tickets with reason.");
            ConsoleHelper.PressAnyKey();
            await Task.CompletedTask;
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
            Console.WriteLine("💬 Feature Coming Soon!");
            Console.WriteLine("Add administrative comments to tickets.");
            ConsoleHelper.PressAnyKey();
            await Task.CompletedTask;
        }

        private async Task EscalateTicketPriorityAsync()
        {
            ConsoleHelper.PrintHeader("Escalate Ticket Priority");
            Console.WriteLine("🆙 Feature Coming Soon!");
            Console.WriteLine("Increase ticket priority.");
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
            Console.WriteLine("➕ Feature Coming Soon!");
            Console.WriteLine("Register new residents, technicians, or admins.");
            ConsoleHelper.PressAnyKey();
            await Task.CompletedTask;
        }

        private async Task ActivateDeactivateUserAsync()
        {
            ConsoleHelper.PrintHeader("Activate/Deactivate User");
            Console.WriteLine("🔄 Feature Coming Soon!");
            Console.WriteLine("Enable or disable user accounts.");
            ConsoleHelper.PressAnyKey();
            await Task.CompletedTask;
        }

        private async Task ResetUserPasswordAsync()
        {
            ConsoleHelper.PrintHeader("Reset User Password");
            Console.WriteLine("🔑 Feature Coming Soon!");
            Console.WriteLine("Reset passwords for users.");
            ConsoleHelper.PressAnyKey();
            await Task.CompletedTask;
        }

        private async Task ViewUserDetailsAsync()
        {
            ConsoleHelper.PrintHeader("View User Details");
            Console.WriteLine("👤 Feature Coming Soon!");
            Console.WriteLine("View detailed user information.");
            ConsoleHelper.PressAnyKey();
            await Task.CompletedTask;
        }

        private async Task UpdateUserRoleAsync()
        {
            ConsoleHelper.PrintHeader("Update User Role");
            Console.WriteLine("🔄 Feature Coming Soon!");
            Console.WriteLine("Change user roles (Resident/Technician/Admin).");
            ConsoleHelper.PressAnyKey();
            await Task.CompletedTask;
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
            Console.WriteLine("⭐ Feature Coming Soon!");
            Console.WriteLine("View detailed ratings and reviews.");
            ConsoleHelper.PressAnyKey();
            await Task.CompletedTask;
        }

        private async Task AssignTechnicianSpecializationAsync()
        {
            ConsoleHelper.PrintHeader("Assign Technician Specialization");
            Console.WriteLine("🔧 Feature Coming Soon!");
            Console.WriteLine("Set technician specializations.");
            ConsoleHelper.PressAnyKey();
            await Task.CompletedTask;
        }

        private async Task SetTechnicianAvailabilityAsync()
        {
            ConsoleHelper.PrintHeader("Set Technician Availability");
            Console.WriteLine("📍 Feature Coming Soon!");
            Console.WriteLine("Manage technician availability.");
            ConsoleHelper.PressAnyKey();
            await Task.CompletedTask;
        }

        private async Task ViewTechnicianWorkloadAsync()
        {
            ConsoleHelper.PrintHeader("Technician Workload");
            Console.WriteLine("📊 Feature Coming Soon!");
            Console.WriteLine("View current workload distribution.");
            ConsoleHelper.PressAnyKey();
            await Task.CompletedTask;
        }

        private async Task HireNewTechnicianAsync()
        {
            ConsoleHelper.PrintHeader("Hire New Technician");
            Console.WriteLine("➕ Feature Coming Soon!");
            Console.WriteLine("Register new technicians.");
            ConsoleHelper.PressAnyKey();
            await Task.CompletedTask;
        }

        // ==================== PAYMENT & FINANCIAL METHODS ====================

        private async Task ViewAllInvoicesAsync()
        {
            ConsoleHelper.PrintHeader("All Invoices");
            Console.WriteLine("💰 Feature Coming Soon!");
            Console.WriteLine("View all system invoices.");
            ConsoleHelper.PressAnyKey();
            await Task.CompletedTask;
        }

        private async Task GenerateInvoiceAsync()
        {
            ConsoleHelper.PrintHeader("Generate Invoice");
            Console.WriteLine("📄 Feature Coming Soon!");
            Console.WriteLine("Create invoices for tickets.");
            ConsoleHelper.PressAnyKey();
            await Task.CompletedTask;
        }

        private async Task MarkPaymentReceivedAsync()
        {
            ConsoleHelper.PrintHeader("Mark Payment as Received");
            Console.WriteLine("✅ Feature Coming Soon!");
            Console.WriteLine("Confirm payment receipt.");
            ConsoleHelper.PressAnyKey();
            await Task.CompletedTask;
        }

        private async Task IssueRefundAsync()
        {
            ConsoleHelper.PrintHeader("Issue Refund");
            Console.WriteLine("💸 Feature Coming Soon!");
            Console.WriteLine("Process refunds for customers.");
            ConsoleHelper.PressAnyKey();
            await Task.CompletedTask;
        }

        private async Task ViewPaymentReportsAsync()
        {
            ConsoleHelper.PrintHeader("Payment Reports");
            Console.WriteLine("📊 Feature Coming Soon!");
            Console.WriteLine("View payment statistics.");
            ConsoleHelper.PressAnyKey();
            await Task.CompletedTask;
        }

        private async Task SetServiceChargesAsync()
        {
            ConsoleHelper.PrintHeader("Set Service Charges");
            Console.WriteLine("💵 Feature Coming Soon!");
            Console.WriteLine("Configure pricing for services.");
            ConsoleHelper.PressAnyKey();
            await Task.CompletedTask;
        }

        private async Task ViewRevenueAnalyticsAsync()
        {
            ConsoleHelper.PrintHeader("Revenue Analytics");
            Console.WriteLine("📈 Feature Coming Soon!");
            Console.WriteLine("View revenue trends and analytics.");
            ConsoleHelper.PressAnyKey();
            await Task.CompletedTask;
        }

        private async Task ExportFinancialReportAsync()
        {
            ConsoleHelper.PrintHeader("Export Financial Report");
            Console.WriteLine("📥 Feature Coming Soon!");
            Console.WriteLine("Export financial data to Excel/PDF.");
            ConsoleHelper.PressAnyKey();
            await Task.CompletedTask;
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
            Console.WriteLine("📋 Feature Coming Soon!");
            Console.WriteLine("Generate comprehensive ticket reports.");
            ConsoleHelper.PressAnyKey();
            await Task.CompletedTask;
        }

        private async Task GenerateUserReportAsync()
        {
            ConsoleHelper.PrintHeader("Generate User Report");
            Console.WriteLine("👥 Feature Coming Soon!");
            Console.WriteLine("Generate user activity reports.");
            ConsoleHelper.PressAnyKey();
            await Task.CompletedTask;
        }

        private async Task GenerateRevenueReportAsync()
        {
            ConsoleHelper.PrintHeader("Generate Revenue Report");
            Console.WriteLine("💰 Feature Coming Soon!");
            Console.WriteLine("Generate revenue and financial reports.");
            ConsoleHelper.PressAnyKey();
            await Task.CompletedTask;
        }

        private async Task ViewCategoryStatsAsync()
        {
            ConsoleHelper.PrintHeader("Category-wise Statistics");
            Console.WriteLine("📊 Feature Coming Soon!");
            Console.WriteLine("View tickets by category.");
            ConsoleHelper.PressAnyKey();
            await Task.CompletedTask;
        }

        private async Task ViewLocationStatsAsync()
        {
            ConsoleHelper.PrintHeader("Location-wise Statistics");
            Console.WriteLine("📍 Feature Coming Soon!");
            Console.WriteLine("View tickets by location.");
            ConsoleHelper.PressAnyKey();
            await Task.CompletedTask;
        }

        private async Task ExportReportsToExcelAsync()
        {
            ConsoleHelper.PrintHeader("Export Reports to Excel");
            Console.WriteLine("📥 Feature Coming Soon!");
            Console.WriteLine("Export all reports to Excel format.");
            ConsoleHelper.PressAnyKey();
            await Task.CompletedTask;
        }

        // ==================== SYSTEM MANAGEMENT METHODS ====================

        private async Task ManageCategoriesAsync()
        {
            ConsoleHelper.PrintHeader("Manage Categories");
            Console.WriteLine("🏷️ Feature Coming Soon!");
            Console.WriteLine("Add, edit, or remove ticket categories.");
            ConsoleHelper.PressAnyKey();
            await Task.CompletedTask;
        }

        private async Task ManageLocationsAsync()
        {
            ConsoleHelper.PrintHeader("Manage Locations");
            Console.WriteLine("📍 Feature Coming Soon!");
            Console.WriteLine("Manage hostel locations.");
            ConsoleHelper.PressAnyKey();
            await Task.CompletedTask;
        }

        private async Task ManagePrioritiesAsync()
        {
            ConsoleHelper.PrintHeader("Manage Priorities");
            Console.WriteLine("🎯 Feature Coming Soon!");
            Console.WriteLine("Configure priority levels.");
            ConsoleHelper.PressAnyKey();
            await Task.CompletedTask;
        }

        private async Task ManagePaymentMethodsAsync()
        {
            ConsoleHelper.PrintHeader("Manage Payment Methods");
            Console.WriteLine("💳 Feature Coming Soon!");
            Console.WriteLine("Configure payment options.");
            ConsoleHelper.PressAnyKey();
            await Task.CompletedTask;
        }

        private async Task ViewAuditLogsAsync()
        {
            ConsoleHelper.PrintHeader("View Audit Logs");
            Console.WriteLine("📜 Feature Coming Soon!");
            Console.WriteLine("View system activity logs.");
            ConsoleHelper.PressAnyKey();
            await Task.CompletedTask;
        }

        private async Task ViewSystemNotificationsAsync()
        {
            ConsoleHelper.PrintHeader("System Notifications");
            Console.WriteLine("🔔 Feature Coming Soon!");
            Console.WriteLine("View all system notifications.");
            ConsoleHelper.PressAnyKey();
            await Task.CompletedTask;
        }

        private async Task BroadcastNotificationAsync()
        {
            ConsoleHelper.PrintHeader("Broadcast Notification");
            Console.WriteLine("📢 Feature Coming Soon!");
            Console.WriteLine("Send notifications to all users.");
            ConsoleHelper.PressAnyKey();
            await Task.CompletedTask;
        }

        private async Task BackupDatabaseAsync()
        {
            ConsoleHelper.PrintHeader("Backup Database");
            Console.WriteLine("💾 Feature Coming Soon!");
            Console.WriteLine("Create database backup.");
            ConsoleHelper.PressAnyKey();
            await Task.CompletedTask;
        }

        // ==================== FEEDBACK & RATINGS METHODS ====================

        private async Task ViewAllFeedbackAsync()
        {
            ConsoleHelper.PrintHeader("View All Feedback");
            Console.WriteLine("📝 Feature Coming Soon!");
            Console.WriteLine("View user feedback and suggestions.");
            ConsoleHelper.PressAnyKey();
            await Task.CompletedTask;
        }

        private async Task RespondToFeedbackAsync()
        {
            ConsoleHelper.PrintHeader("Respond to Feedback");
            Console.WriteLine("💬 Feature Coming Soon!");
            Console.WriteLine("Reply to user feedback.");
            ConsoleHelper.PressAnyKey();
            await Task.CompletedTask;
        }

        private async Task ViewRatingAnalyticsAsync()
        {
            ConsoleHelper.PrintHeader("Rating Analytics");
            Console.WriteLine("⭐ Feature Coming Soon!");
            Console.WriteLine("Analyze rating trends.");
            ConsoleHelper.PressAnyKey();
            await Task.CompletedTask;
        }

        // ==================== SETTINGS METHODS ====================

        private async Task SystemSettingsAsync()
        {
            ConsoleHelper.PrintHeader("System Settings");
            Console.WriteLine("⚙️ Feature Coming Soon!");
            Console.WriteLine("Configure system-wide settings.");
            ConsoleHelper.PressAnyKey();
            await Task.CompletedTask;
        }

        private async Task ChangeAdminPasswordAsync()
        {
            ConsoleHelper.PrintHeader("Change Admin Password");
            Console.WriteLine("🔒 Feature Coming Soon!");
            Console.WriteLine("Change your admin password.");
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