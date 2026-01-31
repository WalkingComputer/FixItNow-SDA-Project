using System;
using FixItNow.Domain.Interfaces;
using FixItNow.Domain.Notifications;

namespace FixItNow.Application.Factories
{
    /// <summary>
    /// Factory for creating different types of notifications
    /// Implements Factory Method Design Pattern
    /// </summary>
    public interface INotificationFactory
    {
        INotification CreateNotification(string notificationType);
    }

    public class NotificationFactory : INotificationFactory
    {
        /// <summary>
        /// Create notification instance based on type
        /// </summary>
        /// <param name="notificationType">Type of notification: "InApp", "Email", "SMS"</param>
        /// <returns>Notification instance</returns>
        public INotification CreateNotification(string notificationType)
        {
            if (string.IsNullOrEmpty(notificationType))
                throw new ArgumentNullException(nameof(notificationType));

            notificationType = notificationType.ToLower().Trim();

            switch (notificationType)
            {
                case "inapp":
                    return new InAppNotification();

                case "email":
                    return new EmailNotification();

                case "sms":
                    return new SMSNotification();

                default:
                    throw new ArgumentException(
                        $"Unknown notification type: {notificationType}. " +
                        "Supported types: InApp, Email, SMS",
                        nameof(notificationType));
            }
        }

        /// <summary>
        /// Get all supported notification types
        /// </summary>
        public static string[] GetSupportedTypes()
        {
            return new[] { "InApp", "Email", "SMS" };
        }
    }
}
