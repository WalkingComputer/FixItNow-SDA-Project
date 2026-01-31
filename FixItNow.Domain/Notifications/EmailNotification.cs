using System;
using System.Threading.Tasks;
using FixItNow.Domain.Interfaces;

namespace FixItNow.Domain.Notifications
{
    /// <summary>
    /// Email Notification Implementation
    /// Sends notification via email
    /// </summary>
    public class EmailNotification : INotification
    {
        public int NotificationId { get; set; }
        public int UserId { get; set; }
        public int TicketId { get; set; }
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsRead { get; set; }
        public string EmailAddress { get; set; }

        public EmailNotification()
        {
            CreatedAt = DateTime.UtcNow;
            IsRead = false;
        }

        /// <summary>
        /// Send notification via email
        /// </summary>
        public async Task SendAsync()
        {
            await Task.Run(() =>
            {
                // Simulate email sending
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"[EMAIL NOTIFICATION]");
                Console.WriteLine($"   To: {EmailAddress}");
                Console.WriteLine($"   Subject: FixItNow - Ticket Notification");
                Console.WriteLine($"   Message: {Message}");
                Console.WriteLine($"   Ticket ID: {TicketId}");
                Console.WriteLine($"   Sent: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                Console.ResetColor();

                // In production: Use SMTP to send actual email
                // Example: using (SmtpClient client = new SmtpClient("smtp.gmail.com", 587)) { ... }
            });
        }

        public string GetNotificationType()
        {
            return "Email";
        }
    }
}
