using System;
using System.Threading.Tasks;
using FixItNow.Domain.Interfaces;

namespace FixItNow.Domain.Notifications
{
    /// <summary>
    /// SMS Notification Implementation
    /// Sends notification via SMS/Text message
    /// </summary>
    public class SMSNotification : INotification
    {
        public int NotificationId { get; set; }
        public int UserId { get; set; }
        public int TicketId { get; set; }
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsRead { get; set; }
        public string PhoneNumber { get; set; }

        public SMSNotification()
        {
            CreatedAt = DateTime.UtcNow;
            IsRead = false;
        }

        /// <summary>
        /// Send notification via SMS
        /// </summary>
        public async Task SendAsync()
        {
            await Task.Run(() =>
            {
                // Simulate SMS sending
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine($"[SMS NOTIFICATION]");
                Console.WriteLine($"   To: {PhoneNumber}");
                Console.WriteLine($"   Message: {Message}");
                Console.WriteLine($"   Sent: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                Console.ResetColor();

                // In production: Use Twilio API or similar SMS service
                // Example: var client = new TwilioRestClient(accountSid, authToken);
            });
        }

        public string GetNotificationType()
        {
            return "SMS";
        }
    }
}
