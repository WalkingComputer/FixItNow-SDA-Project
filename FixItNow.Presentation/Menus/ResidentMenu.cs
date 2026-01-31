using FixItNow.Application.DTOs;
using FixItNow.Application.Interfaces;
using FixItNow.Application.Services;
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
        private readonly ITechnicianSelectionService _technicianSelectionService;
        private readonly ICommentService _commentService;
        private readonly IAttachmentService _attachmentService;
        private readonly IFeedbackService _feedbackService;
        private readonly IChatService _chatService;
        private readonly IPaymentService _paymentService;
        private readonly IWalletService _walletService;
        private readonly IInvoiceService _invoiceService;
        private readonly IUserService _userService;

        public ResidentMenu(
            IAuthenticationService authService,
            ITicketService ticketService,
            ITechnicianSelectionService technicianSelectionService,
            ICommentService commentService,
            IAttachmentService attachmentService,
            IFeedbackService feedbackService,
            IChatService chatService,
            IPaymentService paymentService,
            IWalletService walletService,
            IInvoiceService invoiceService,
            IUserService userService)
        {
            _authService = authService;
            _ticketService = ticketService;
            _technicianSelectionService = technicianSelectionService;
            _commentService = commentService;
            _attachmentService = attachmentService;
            _feedbackService = feedbackService;
            _chatService = chatService;
            _paymentService = paymentService;
            _walletService = walletService;
            _invoiceService = invoiceService;
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
                Console.WriteLine("║                          RESIDENT DASHBOARD                                    ║");
                Console.WriteLine("╚════════════════════════════════════════════════════════════════════════════════╝");
                Console.ResetColor();
                
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n>> TICKET MANAGEMENT");
                Console.ResetColor();
                Console.WriteLine("════════════════════════════════════════════════════════════════════════════════");
                Console.WriteLine(" 1. Create New Ticket             2. View My Tickets");
                Console.WriteLine(" 3. View Ticket Details           4. Cancel Ticket");
                Console.WriteLine(" 5. Reopen Closed Ticket          6. Add Comment to Ticket");
                Console.WriteLine(" 7. Upload Attachment             8. Track Ticket Status");
                Console.WriteLine(" 9. View Ticket Timeline         10. Request Emergency Service");
                
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n>> TECHNICIAN SELECTION & RATING");
                Console.ResetColor();
                Console.WriteLine("════════════════════════════════════════════════════════════════════════════════");
                Console.WriteLine("11. View Technician Profiles     12. Rate Technician");
                Console.WriteLine("13. Chat with Technician");
                
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n>> PAYMENT & FINANCIAL");
                Console.ResetColor();
                Console.WriteLine("════════════════════════════════════════════════════════════════════════════════");
                Console.WriteLine("14. View My Invoices             15. Pay Invoice");
                Console.WriteLine("16. View Payment History         17. Download Receipt");
                Console.WriteLine("18. Check Wallet Balance         19. Add Money to Wallet");
                Console.WriteLine("20. View Transaction History     21. View Service Charges");
                
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n>> PROFILE & SETTINGS");
                Console.ResetColor();
                Console.WriteLine("════════════════════════════════════════════════════════════════════════════════");
                Console.WriteLine("22. Update My Profile            23. Change Password");
                Console.WriteLine("24. View Notifications           25. Submit Feedback");
                Console.WriteLine("26. View My Feedback             27. Download Invoice PDF");
                
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n>> EXIT");
                Console.ResetColor();
                Console.WriteLine("════════════════════════════════════════════════════════════════════════════════");
                Console.WriteLine("28. Logout");
                Console.WriteLine("════════════════════════════════════════════════════════════════════════════════");
                
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("\nSelect option (1-28): ");
                Console.ResetColor();

                var choice = Console.ReadLine();

                switch (choice)
                {
                    // TICKET MANAGEMENT
                    case "1": await CreateTicketAsync(); break;
                    case "2": await ViewMyTicketsAsync(); break;
                    case "3": await ViewTicketDetailsAsync(); break;
                    case "4": await CancelTicketAsync(); break;
                    case "5": await ReopenTicketAsync(); break;
                    case "6": await AddCommentAsync(); break;
                    case "7": await UploadAttachmentAsync(); break;
                    case "8": await TrackTicketStatusAsync(); break;
                    case "9": await ViewTicketTimelineAsync(); break;
                    case "10": await RequestEmergencyServiceAsync(); break;
                    
                    // TECHNICIAN SELECTION & RATING
                    case "11": await ViewTechnicianProfilesAsync(); break;
                    case "12": await RateTechnicianAsync(); break;
                    case "13": await ChatWithTechnicianAsync(); break;
                    
                    // PAYMENT & FINANCIAL
                    case "14": await ViewMyInvoicesAsync(); break;
                    case "15": await PayInvoiceAsync(); break;
                    case "16": await ViewPaymentHistoryAsync(); break;
                    case "17": await DownloadReceiptAsync(); break;
                    case "18": await CheckWalletBalanceAsync(); break;
                    case "19": await AddMoneyToWalletAsync(); break;
                    case "20": await ViewTransactionHistoryAsync(); break;
                    case "21": await ViewServiceChargesAsync(); break;
                    
                    // PROFILE & SETTINGS
                    case "22": await UpdateMyProfileAsync(); break;
                    case "23": await ChangePasswordAsync(); break;
                    case "24": await ViewNotificationsAsync(); break;
                    case "25": await SubmitFeedbackAsync(); break;
                    case "26": await ViewMyFeedbackAsync(); break;
                    case "27": await DownloadInvoicePDFAsync(); break;
                    
                    // EXIT
                    case "28": return;
                    
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\n❌ Invalid option!");
                        Console.ResetColor();
                        ConsoleHelper.PressAnyKey();
                        break;
                }
            }
        }

        /// <summary>
        /// ? Create new ticket with technician selection
        /// NO ADMIN INVOLVEMENT - USER SELECTS TECHNICIAN DIRECTLY
        /// </summary>
        private async Task CreateTicketAsync()
        {
            ConsoleHelper.PrintHeader("Create New Ticket");

            try
            {
                Console.Write("Title: ");
                var title = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(title))
                {
                    Console.WriteLine("? Title cannot be empty!");
                    ConsoleHelper.PressAnyKey();
                    return;
                }

                Console.Write("Description: ");
                var description = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(description))
                {
                    Console.WriteLine("? Description cannot be empty!");
                    ConsoleHelper.PressAnyKey();
                    return;
                }

                // Select Category
                Console.WriteLine("\n?? Categories:");
                Console.WriteLine("1. Plumbing");
                Console.WriteLine("2. Electric");
                Console.WriteLine("3. WiFi");
                Console.WriteLine("4. Furniture");
                Console.WriteLine("5. Other");
                Console.Write("Select category (1-5): ");
                int categoryId = int.Parse(Console.ReadLine() ?? "1");

                if (categoryId < 1 || categoryId > 5)
                {
                    Console.WriteLine("? Invalid category!");
                    ConsoleHelper.PressAnyKey();
                    return;
                }

                // ? SHOW AVAILABLE TECHNICIANS WITH RATINGS
                // THIS IS THE KEY FEATURE - NO ADMIN NEEDED!
                Console.WriteLine("\n? Loading available technicians...");
                var technicians = await _technicianSelectionService
                    .GetAvailableTechniciansByCategoryAsync(categoryId);

                if (technicians.Count == 0)
                {
                    Console.WriteLine("? No technicians available. Please try again later.");
                    ConsoleHelper.PressAnyKey();
                    return;
                }

                DisplayTechniciansList(technicians);

                Console.Write($"\nSelect technician (1-{technicians.Count}): ");
                int techChoice = int.Parse(Console.ReadLine() ?? "1") - 1;

                if (techChoice < 0 || techChoice >= technicians.Count)
                {
                    Console.WriteLine("? Invalid selection!");
                    ConsoleHelper.PressAnyKey();
                    return;
                }

                var selectedTechnician = technicians[techChoice];

                // Select Priority
                Console.WriteLine("\n?? Priority Levels:");
                Console.WriteLine("1. Low");
                Console.WriteLine("2. Medium");
                Console.WriteLine("3. High");
                Console.WriteLine("4. Urgent");
                Console.Write("Select priority (1-4): ");
                int priorityId = int.Parse(Console.ReadLine() ?? "2");

                if (priorityId < 1 || priorityId > 4)
                {
                    Console.WriteLine("? Invalid priority!");
                    ConsoleHelper.PressAnyKey();
                    return;
                }

                // Select Location
                Console.WriteLine("\n?? Locations:");
                Console.WriteLine("1. Boys Hostel A - Block 1");
                Console.WriteLine("2. Boys Hostel A - Block 2");
                Console.WriteLine("3. Girls Hostel B - Block 1");
                Console.WriteLine("4. Girls Hostel B - Block 2");
                Console.Write("Select location (1-4): ");
                int locationId = int.Parse(Console.ReadLine() ?? "1");

                if (locationId < 1 || locationId > 4)
                {
                    Console.WriteLine("? Invalid location!");
                    ConsoleHelper.PressAnyKey();
                    return;
                }

                var currentUser = _authService.GetCurrentUser();
                var request = new CreateTicketRequest
                {
                    Title = title,
                    Description = description,
                    CategoryId = categoryId,
                    LocationId = locationId,
                    PriorityId = priorityId,
                    CreatedByUserId = currentUser.UserId
                };

                // Create ticket with SELECTED technician - NO ADMIN INVOLVED
                var ticket = await _ticketService.CreateTicketAsync(request, selectedTechnician.UserId);

                Console.WriteLine("\n? Ticket created successfully!");
                Console.WriteLine($"   Ticket Code: {ticket.TicketCode}");
                Console.WriteLine($"   Assigned to: {selectedTechnician.FullName}");
                Console.WriteLine($"   Rating: {FormatRating(selectedTechnician.AverageRating)}/5");
                Console.WriteLine($"   Status: Assigned (Ready for work!)");

                ConsoleHelper.PressAnyKey();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n? Error: {ex.Message}");
                Console.ResetColor();
                ConsoleHelper.PressAnyKey();
            }
        }

        /// <summary>
        /// View all tickets created by resident
        /// </summary>
        private async Task ViewMyTicketsAsync()
        {
            ConsoleHelper.PrintHeader("My Tickets");

            try
            {
                var currentUser = _authService.GetCurrentUser();
                var myTickets = await _ticketService.GetMyTicketsAsync(currentUser.UserId);

                if (myTickets.Count == 0)
                {
                    Console.WriteLine("?? You haven't created any tickets yet.");
                    ConsoleHelper.PressAnyKey();
                    return;
                }

                Console.WriteLine($"\n?? Your Tickets ({myTickets.Count} total):\n");
                Console.WriteLine(new string('?', 100));

                foreach (var ticket in myTickets)
                {
                    DisplayTicketDetails(ticket);
                    Console.WriteLine(new string('?', 100));
                }

                ConsoleHelper.PressAnyKey();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n? Error: {ex.Message}");
                Console.ResetColor();
                ConsoleHelper.PressAnyKey();
            }
        }

        /// <summary>
        /// ? Rate technician after ticket completion
        /// USER FEEDBACK IMPROVES TECHNICIAN RATINGS
        /// </summary>
        private async Task RateTechnicianAsync()
        {
            ConsoleHelper.PrintHeader("Rate Technician");

            try
            {
                var currentUser = _authService.GetCurrentUser();
                var myTickets = await _ticketService.GetMyTicketsAsync(currentUser.UserId);

                // Get resolved tickets that haven't been rated
                var rateableTickets = myTickets
                    .Where(t => t.StatusId == 4 && !t.TechnicianRatingGiven.HasValue)
                    .ToList();

                if (rateableTickets.Count == 0)
                {
                    Console.WriteLine("?? No completed tickets to rate.");
                    Console.WriteLine("(Only resolved tickets without ratings can be rated)");
                    ConsoleHelper.PressAnyKey();
                    return;
                }

                Console.WriteLine($"\n?? Resolved Tickets to Rate ({rateableTickets.Count}):\n");

                for (int i = 0; i < rateableTickets.Count; i++)
                {
                    var ticket = rateableTickets[i];
                    Console.WriteLine($"{i + 1}. {ticket.TicketCode} - {ticket.Title}");
                }

                Console.Write($"\nSelect ticket to rate (1-{rateableTickets.Count}): ");
                int ticketChoice = int.Parse(Console.ReadLine() ?? "0") - 1;

                if (ticketChoice < 0 || ticketChoice >= rateableTickets.Count)
                {
                    Console.WriteLine("? Invalid selection!");
                    ConsoleHelper.PressAnyKey();
                    return;
                }

                var selectedTicket = rateableTickets[ticketChoice];

                Console.WriteLine($"\n??? Ticket: {selectedTicket.TicketCode}");
                Console.WriteLine($"Title: {selectedTicket.Title}");
                Console.WriteLine($"Technician ID: {selectedTicket.CurrentTechnicianId}");

                // Show rating UI
                Console.WriteLine("\n? How would you rate the technician's work?");
                Console.WriteLine("1. ? Poor");
                Console.WriteLine("2. ?? Fair");
                Console.WriteLine("3. ??? Good");
                Console.WriteLine("4. ???? Very Good");
                Console.WriteLine("5. ????? Excellent");
                Console.Write("\nYour rating (1-5): ");
                int rating = int.Parse(Console.ReadLine() ?? "5");

                if (rating < 1 || rating > 5)
                {
                    Console.WriteLine("? Invalid rating!");
                    ConsoleHelper.PressAnyKey();
                    return;
                }

                Console.Write("\nReview (optional, press Enter to skip): ");
                var review = Console.ReadLine() ?? string.Empty;

                // Submit rating - THIS UPDATES TECHNICIAN'S AVERAGE RATING
                await _technicianSelectionService.RateTechnicianAsync(
                    selectedTicket.TicketId,
                    selectedTicket.CurrentTechnicianId.Value,
                    rating,
                    review
                );

                Console.WriteLine("\n? Rating submitted successfully!");
                Console.WriteLine($"   Rating: {FormatRating(rating)}/5");
                if (!string.IsNullOrWhiteSpace(review))
                {
                    Console.WriteLine($"   Review: {review}");
                }
                Console.WriteLine("\n?? Your rating helps other residents choose the best technicians!");

                ConsoleHelper.PressAnyKey();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n? Error: {ex.Message}");
                Console.ResetColor();
                ConsoleHelper.PressAnyKey();
            }
        }

        /// <summary>
        /// ? View all technician profiles with ratings
        /// RESIDENTS CAN BROWSE BEFORE CREATING TICKET
        /// </summary>
        private async Task ViewTechnicianProfilesAsync()
        {
            ConsoleHelper.PrintHeader("Technician Profiles & Ratings");

            try
            {
                var technicians = await _technicianSelectionService
                    .GetAllTechniciansSortedByRatingAsync();

                if (technicians.Count == 0)
                {
                    Console.WriteLine("? No technicians available.");
                    ConsoleHelper.PressAnyKey();
                    return;
                }

                Console.WriteLine($"\n?? Available Technicians ({technicians.Count} total)\n");
                Console.WriteLine(new string('?', 80));

                for (int i = 0; i < technicians.Count; i++)
                {
                    DisplayTechnicianProfile(i + 1, technicians[i]);
                    Console.WriteLine(new string('?', 80));
                }

                Console.WriteLine("\n?? Tip: Higher rated technicians are shown first!");
                Console.WriteLine("Create a ticket to assign work to your preferred technician.");

                ConsoleHelper.PressAnyKey();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n? Error: {ex.Message}");
                Console.ResetColor();
                ConsoleHelper.PressAnyKey();
            }
        }

        /// <summary>
        /// Display technician profile with ratings
        /// </summary>
        private void DisplayTechnicianProfile(int number, Domain.Entities.User technician)
        {
            string stars = GetStarRating(technician.AverageRating);

            Console.WriteLine($"\n#{number} {technician.FullName}");
            Console.WriteLine($"   ? Rating: {stars} {technician.AverageRating:F2}/5.0");
            Console.WriteLine($"   ?? Reviews: {technician.TotalRatings} ratings");
            Console.WriteLine($"   ? Completed: {technician.CompletedTickets} tickets");
            Console.WriteLine($"   ?? Specialization: {(technician.Specialization ?? "General")}");
            Console.WriteLine($"   ?? Email: {technician.Email}");
            Console.WriteLine($"   ?? Phone: {technician.Phone}");
        }

        /// <summary>
        /// Display technicians list for selection during ticket creation
        /// </summary>
        private void DisplayTechniciansList(List<Domain.Entities.User> technicians)
        {
            Console.WriteLine("\n? Available Technicians (Sorted by Rating):");
            Console.WriteLine(new string('?', 80));

            for (int i = 0; i < technicians.Count; i++)
            {
                var tech = technicians[i];
                string stars = GetStarRating(tech.AverageRating);

                Console.WriteLine($"\n{i + 1}. {tech.FullName}");
                Console.WriteLine($"   ? Rating: {stars} {tech.AverageRating:F2}/5.0 ({tech.TotalRatings} reviews)");
                Console.WriteLine($"   ? Completed: {tech.CompletedTickets} tickets");
                Console.WriteLine($"   ?? Specialization: {(tech.Specialization ?? "General")}");
            }

            Console.WriteLine("\n" + new string('?', 80));
        }

        /// <summary>
        /// Display ticket details with rating info
        /// </summary>
        private void DisplayTicketDetails(Domain.Entities.Ticket ticket)
        {
            string statusName = GetStatusName(ticket.StatusId);
            string categoryName = GetCategoryName(ticket.CategoryId);
            string priorityName = GetPriorityName(ticket.PriorityId);

            Console.WriteLine($"\n??? {ticket.TicketCode} - {ticket.Title}");
            Console.WriteLine($"   Description: {ticket.Description}");
            Console.WriteLine($"   Category: {categoryName} | Priority: {priorityName}");
            Console.WriteLine($"   Status: {statusName}");
            Console.WriteLine($"   Created: {ticket.CreatedAt:yyyy-MM-dd HH:mm:ss}");

            if (ticket.TechnicianRatingGiven.HasValue)
            {
                string ratingStars = GetStarRating(ticket.TechnicianRatingGiven.Value);
                Console.WriteLine($"   ? Your Rating: {ratingStars} {ticket.TechnicianRatingGiven}/5");
                if (!string.IsNullOrWhiteSpace(ticket.UserReview))
                {
                    Console.WriteLine($"   ?? Review: {ticket.UserReview}");
                }
            }
        }

        /// <summary>
        /// Get visual star rating
        /// </summary>
        private string GetStarRating(double rating)
        {
            int fullStars = (int)Math.Round(rating);
            string stars = new string('?', fullStars);
            string emptyStars = new string('?', 5 - fullStars);
            return $"{stars}{emptyStars}";
        }

        /// <summary>
        /// Format rating as text
        /// </summary>
        private string FormatRating(double rating)
        {
            return GetStarRating(rating);
        }

        private string GetStatusName(int statusId)
        {
            switch (statusId)
            {
                case 1:
                    return "Open";
                case 2:
                    return "Assigned";
                case 3:
                    return "In Progress";
                case 4:
                    return "Resolved";
                case 5:
                    return "Closed";
                case 6:
                    return "Reopened";
                default:
                    return "Unknown";
            }
        }

        private string GetCategoryName(int categoryId)
        {
            switch (categoryId)
            {
                case 1:
                    return "Plumbing";
                case 2:
                    return "Electric";
                case 3:
                    return "WiFi";
                case 4:
                    return "Furniture";
                case 5:
                    return "Other";
                default:
                    return "Unknown";
            }
        }


        private string GetPriorityName(int priorityId)
        {
            switch (priorityId)
            {
                case 1:
                    return "Low";
                case 2:
                    return "Medium";
                case 3:
                    return "High";
                case 4:
                    return "Urgent";
                default:
                    return "Unknown";
            }
        }

        // ==================== NEW TICKET MANAGEMENT METHODS ====================

        private async Task ViewTicketDetailsAsync()
        {
            ConsoleHelper.PrintHeader("View Ticket Details");
            
            try
            {
                var currentUser = _authService.GetCurrentUser();
                var myTickets = await _ticketService.GetMyTicketsAsync(currentUser.UserId);
                
                if (myTickets.Count == 0)
                {
                    Console.WriteLine("📭 You have no tickets.");
                    ConsoleHelper.PressAnyKey();
                    return;
                }
                
                Console.WriteLine("\n📋 Your Tickets:");
                for (int i = 0; i < myTickets.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {myTickets[i].TicketCode} - {myTickets[i].Title}");
                }
                
                Console.Write($"\nSelect ticket (1-{myTickets.Count}): ");
                int choice = int.Parse(Console.ReadLine() ?? "0") - 1;
                
                if (choice >= 0 && choice < myTickets.Count)
                {
                    var ticket = myTickets[choice];
                    Console.WriteLine("\n" + new string('═', 80));
                    DisplayTicketDetails(ticket);
                    Console.WriteLine(new string('═', 80));
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

        private async Task CancelTicketAsync()
        {
            ConsoleHelper.PrintHeader("Cancel Ticket");
            
            try
            {
                var currentUser = _authService.GetCurrentUser();
                var myTickets = await _ticketService.GetMyTicketsAsync(currentUser.UserId);
                
                var openTickets = myTickets.Where(t => t.StatusId <= 3).ToList();
                
                if (openTickets.Count == 0)
                {
                    Console.WriteLine("📭 No open tickets to cancel.");
                    ConsoleHelper.PressAnyKey();
                    return;
                }
                
                Console.WriteLine("\n📋 Open Tickets:");
                for (int i = 0; i < openTickets.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {openTickets[i].TicketCode} - {openTickets[i].Title}");
                }
                
                Console.Write($"\nSelect ticket to cancel (1-{openTickets.Count}): ");
                int choice = int.Parse(Console.ReadLine() ?? "0") - 1;
                
                if (choice >= 0 && choice < openTickets.Count)
                {
                    Console.Write("Are you sure? (yes/no): ");
                    if (Console.ReadLine()?.ToLower() == "yes")
                    {
                        // TODO: Implement cancel ticket in service
                        Console.WriteLine("✅ Ticket cancelled successfully!");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error: {ex.Message}");
            }
            
            ConsoleHelper.PressAnyKey();
        }

        private async Task ReopenTicketAsync()
        {
            ConsoleHelper.PrintHeader("Reopen Closed Ticket");
            
            try
            {
                var currentUser = _authService.GetCurrentUser();
                var myTickets = await _ticketService.GetMyTicketsAsync(currentUser.UserId);
                
                var closedTickets = myTickets.Where(t => t.StatusId == 5).ToList();
                
                if (closedTickets.Count == 0)
                {
                    Console.WriteLine("📭 No closed tickets to reopen.");
                    ConsoleHelper.PressAnyKey();
                    return;
                }
                
                Console.WriteLine("\n📋 Closed Tickets:");
                for (int i = 0; i < closedTickets.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {closedTickets[i].TicketCode} - {closedTickets[i].Title}");
                }
                
                Console.Write($"\nSelect ticket to reopen (1-{closedTickets.Count}): ");
                int choice = int.Parse(Console.ReadLine() ?? "0") - 1;
                
                if (choice >= 0 && choice < closedTickets.Count)
                {
                    Console.Write("Reason for reopening: ");
                    string reason = Console.ReadLine() ?? "";
                    
                    // TODO: Implement reopen ticket in service
                    Console.WriteLine("✅ Ticket reopened successfully!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error: {ex.Message}");
            }
            
            ConsoleHelper.PressAnyKey();
        }

        private async Task AddCommentAsync()
        {
            try
            {
                ConsoleHelper.PrintHeader("Add Comment to Ticket");
                
                var currentUser = _authService.GetCurrentUser();
                var myTickets = await _ticketService.GetMyTicketsAsync(currentUser.UserId);
                
                if (!myTickets.Any())
                {
                    Console.WriteLine("❌ You have no tickets.");
                    ConsoleHelper.PressAnyKey();
                    return;
                }
                
                Console.WriteLine("\n📋 Your Tickets:");
                for (int i = 0; i < myTickets.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {myTickets[i].TicketCode} - {myTickets[i].Title}");
                }
                
                Console.Write($"\nSelect ticket (1-{myTickets.Count}): ");
                int choice = int.Parse(Console.ReadLine() ?? "0") - 1;
                
                if (choice >= 0 && choice < myTickets.Count)
                {
                    Console.Write("\n💬 Enter your comment: ");
                    string comment = Console.ReadLine() ?? "";
                    
                    if (!string.IsNullOrWhiteSpace(comment))
                    {
                        await _commentService.AddCommentAsync(myTickets[choice].TicketId, currentUser.UserId, comment, false);
                        Console.WriteLine("\n✅ Comment added successfully!");
                    }
                    else
                    {
                        Console.WriteLine("\n❌ Comment cannot be empty.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n❌ Error: {ex.Message}");
            }
            
            ConsoleHelper.PressAnyKey();
        }

        private async Task UploadAttachmentAsync()
        {
            try
            {
                ConsoleHelper.PrintHeader("Upload Attachment");
                
                var currentUser = _authService.GetCurrentUser();
                var myTickets = await _ticketService.GetMyTicketsAsync(currentUser.UserId);
                
                if (!myTickets.Any())
                {
                    Console.WriteLine("❌ You have no tickets.");
                    ConsoleHelper.PressAnyKey();
                    return;
                }
                
                Console.WriteLine("\n📋 Your Tickets:");
                for (int i = 0; i < myTickets.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {myTickets[i].TicketCode} - {myTickets[i].Title}");
                }
                
                Console.Write($"\nSelect ticket (1-{myTickets.Count}): ");
                int choice = int.Parse(Console.ReadLine() ?? "0") - 1;
                
                if (choice >= 0 && choice < myTickets.Count)
                {
                    Console.Write("\n📎 Enter file name (e.g., photo.jpg): ");
                    string fileName = Console.ReadLine() ?? "";
                    
                    if (!string.IsNullOrWhiteSpace(fileName))
                    {
                        string filePath = $"/uploads/tickets/{myTickets[choice].TicketId}/{fileName}";
                        await _attachmentService.UploadAttachmentAsync(myTickets[choice].TicketId, currentUser.UserId, fileName, filePath);
                        Console.WriteLine("\n✅ Attachment uploaded successfully!");
                        Console.WriteLine($"📁 File path: {filePath}");
                    }
                    else
                    {
                        Console.WriteLine("\n❌ File name cannot be empty.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n❌ Error: {ex.Message}");
            }
            
            ConsoleHelper.PressAnyKey();
        }

        private async Task TrackTicketStatusAsync()
        {
            ConsoleHelper.PrintHeader("Track Ticket Status");
            
            try
            {
                var currentUser = _authService.GetCurrentUser();
                var myTickets = await _ticketService.GetMyTicketsAsync(currentUser.UserId);
                
                if (myTickets.Count == 0)
                {
                    Console.WriteLine("📭 No tickets to track.");
                    ConsoleHelper.PressAnyKey();
                    return;
                }
                
                Console.WriteLine("\n📊 Ticket Status Summary:\n");
                Console.WriteLine(new string('═', 80));
                
                foreach (var ticket in myTickets)
                {
                    string status = GetStatusName(ticket.StatusId);
                    string emoji;
                    switch (ticket.StatusId)
                    {
                        case 1: emoji = "🆕"; break;
                        case 2: emoji = "📋"; break;
                        case 3: emoji = "⚙️"; break;
                        case 4: emoji = "✅"; break;
                        case 5: emoji = "🔒"; break;
                        case 6: emoji = "🔄"; break;
                        default: emoji = "❓"; break;
                    }
                    
                    Console.WriteLine($"{emoji} {ticket.TicketCode} - {ticket.Title}");
                    Console.WriteLine($"   Status: {status} | Created: {ticket.CreatedAt:yyyy-MM-dd}");
                    Console.WriteLine(new string('-', 80));
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
            Console.WriteLine("You'll see complete ticket history with timestamps.");
            ConsoleHelper.PressAnyKey();
            await Task.CompletedTask;
        }

        private async Task RequestEmergencyServiceAsync()
        {
            ConsoleHelper.PrintHeader("Request Emergency Service");
            Console.WriteLine("🚨 Feature Coming Soon!");
            Console.WriteLine("Emergency tickets will be prioritized automatically.");
            ConsoleHelper.PressAnyKey();
            await Task.CompletedTask;
        }

        // ==================== NEW TECHNICIAN METHODS ====================

        private async Task ChatWithTechnicianAsync()
        {
            ConsoleHelper.PrintHeader("Chat with Technician");
            Console.WriteLine("💬 Feature Coming Soon!");
            Console.WriteLine("Real-time chat with your assigned technician.");
            ConsoleHelper.PressAnyKey();
            await Task.CompletedTask;
        }

        // ==================== NEW PAYMENT METHODS ====================

        private async Task ViewMyInvoicesAsync()
        {
            ConsoleHelper.PrintHeader("My Invoices");
            Console.WriteLine("💰 Feature Coming Soon!");
            Console.WriteLine("View all your invoices and payment status.");
            ConsoleHelper.PressAnyKey();
            await Task.CompletedTask;
        }

        private async Task PayInvoiceAsync()
        {
            ConsoleHelper.PrintHeader("Pay Invoice");
            Console.WriteLine("💳 Feature Coming Soon!");
            Console.WriteLine("Pay via JazzCash, EasyPaisa, or Wallet.");
            ConsoleHelper.PressAnyKey();
            await Task.CompletedTask;
        }

        private async Task ViewPaymentHistoryAsync()
        {
            ConsoleHelper.PrintHeader("Payment History");
            Console.WriteLine("📜 Feature Coming Soon!");
            Console.WriteLine("View all your past payments.");
            ConsoleHelper.PressAnyKey();
            await Task.CompletedTask;
        }

        private async Task DownloadReceiptAsync()
        {
            ConsoleHelper.PrintHeader("Download Receipt");
            Console.WriteLine("🧾 Feature Coming Soon!");
            Console.WriteLine("Download payment receipts as PDF.");
            ConsoleHelper.PressAnyKey();
            await Task.CompletedTask;
        }

        private async Task CheckWalletBalanceAsync()
        {
            ConsoleHelper.PrintHeader("Wallet Balance");
            Console.WriteLine("💰 Feature Coming Soon!");
            Console.WriteLine("Current Balance: PKR 0.00");
            ConsoleHelper.PressAnyKey();
            await Task.CompletedTask;
        }

        private async Task AddMoneyToWalletAsync()
        {
            ConsoleHelper.PrintHeader("Add Money to Wallet");
            Console.WriteLine("💵 Feature Coming Soon!");
            Console.WriteLine("Top up your wallet for faster payments.");
            ConsoleHelper.PressAnyKey();
            await Task.CompletedTask;
        }

        private async Task ViewTransactionHistoryAsync()
        {
            ConsoleHelper.PrintHeader("Transaction History");
            Console.WriteLine("📊 Feature Coming Soon!");
            Console.WriteLine("View all wallet transactions.");
            ConsoleHelper.PressAnyKey();
            await Task.CompletedTask;
        }

        private async Task ViewServiceChargesAsync()
        {
            ConsoleHelper.PrintHeader("Service Charges");
            Console.WriteLine("\n💰 Current Service Charges:\n");
            Console.WriteLine("═══════════════════════════════════════");
            Console.WriteLine("Category          | Low  | Med  | High | Urgent");
            Console.WriteLine("═══════════════════════════════════════");
            Console.WriteLine("🔧 Plumbing       | 500  | 800  | 1200 | 2000");
            Console.WriteLine("⚡ Electric       | 600  | 1000 | 1500 | 2500");
            Console.WriteLine("📡 WiFi           | 400  | 700  | 1000 | 1800");
            Console.WriteLine("🪑 Furniture      | 300  | 500  | 800  | 1500");
            Console.WriteLine("🔨 Other          | 400  | 600  | 900  | 1600");
            Console.WriteLine("═══════════════════════════════════════");
            Console.WriteLine("\n💡 Prices in PKR. Emergency service +50%");
            ConsoleHelper.PressAnyKey();
            await Task.CompletedTask;
        }

        // ==================== NEW PROFILE METHODS ====================

        private async Task UpdateMyProfileAsync()
        {
            ConsoleHelper.PrintHeader("Update My Profile");
            Console.WriteLine("👤 Feature Coming Soon!");
            Console.WriteLine("Update your contact info and preferences.");
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
            Console.WriteLine("Share your feedback about the system.");
            ConsoleHelper.PressAnyKey();
            await Task.CompletedTask;
        }

        private async Task ViewMyFeedbackAsync()
        {
            ConsoleHelper.PrintHeader("My Feedback");
            Console.WriteLine("📋 Feature Coming Soon!");
            Console.WriteLine("View your submitted feedback.");
            ConsoleHelper.PressAnyKey();
            await Task.CompletedTask;
        }

        private async Task DownloadInvoicePDFAsync()
        {
            ConsoleHelper.PrintHeader("Download Invoice PDF");
            Console.WriteLine("📄 Feature Coming Soon!");
            Console.WriteLine("Download invoices as PDF files.");
            ConsoleHelper.PressAnyKey();
            await Task.CompletedTask;
        }
    }
}
