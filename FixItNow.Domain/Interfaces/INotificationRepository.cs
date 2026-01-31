using System.Collections.Generic;
using System.Threading.Tasks;
using FixItNow.Domain.Entities;

namespace FixItNow.Domain.Interfaces
{
    public interface INotificationRepository
    {
        Task AddAsync(Notification notification);
        Task<List<Notification>> FindUnreadByUserAsync(int userId);
        Task MarkReadAsync(int notificationId);
    }
}