using System;
using System.Threading.Tasks;

namespace FixItNow.Domain.Interfaces
{
    /// <summary>
    /// Abstract interface for notification implementations
    /// Follows Factory Method Design Pattern
    /// </summary>
    public interface INotification
    {
        int NotificationId { get; set; }
        int UserId { get; set; }
        int TicketId { get; set; }
        string Message { get; set; }
        DateTime CreatedAt { get; set; }
        bool IsRead { get; set; }

        /// <summary>
        /// Send notification through the appropriate channel
        /// </summary>
        Task SendAsync();

        /// <summary>
        /// Get the notification type
        /// </summary>
        string GetNotificationType();
    }
}
