using System;
using System.Threading.Tasks;
using FixItNow.Domain.Interfaces;

namespace FixItNow.Domain.Notifications
{
    /// <summary>
    /// In-App Notification Implementation
    /// Shows notification directly in the application UI
    /// </summary>
    public class InAppNotification : INotification
    {
        public int NotificationId { get; set; }
        public int UserId { get; set; }
        public int TicketId { get; set; }
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsRead { get; set; }

        public InAppNotification()
        {
            CreatedAt = DateTime.UtcNow;
            IsRead = false;
        }

        /// <summary>
        /// Send in-app notification (stored in database, shown in UI)
        /// </summary>
        public async Task SendAsync()
        {
            await Task.Run(() =>
            {
                // Log to console for demonstration
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"[IN-APP NOTIFICATION] User {UserId}");
                Console.WriteLine($"   Message: {Message}");
                Console.WriteLine($"   Ticket: {TicketId}");
                Console.WriteLine($"   Time: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                Console.ResetColor();

                // In production: Store in database via repository
                // In this case, it's automatically stored when creating notification
            });
        }

        public string GetNotificationType()
        {
            return "InApp";
        }
    }
}
