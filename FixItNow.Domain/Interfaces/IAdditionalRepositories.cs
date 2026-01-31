using FixItNow.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FixItNow.Domain.Interfaces
{
    public interface IChatMessageRepository
    {
        Task<List<ChatMessage>> GetByTicketIdAsync(int ticketId);
        Task<List<ChatMessage>> GetConversationAsync(int userId1, int userId2, int ticketId);
        Task AddAsync(ChatMessage message);
        Task MarkAsReadAsync(int messageId);
        Task<int> GetNextMessageIdAsync();
    }

    public interface ITechnicianAvailabilityRepository
    {
        Task<TechnicianAvailability> GetByTechnicianIdAsync(int technicianId);
        Task<List<TechnicianAvailability>> GetAllAsync();
        Task AddAsync(TechnicianAvailability availability);
        Task UpdateAsync(TechnicianAvailability availability);
        Task<int> GetNextAvailabilityIdAsync();
    }

    public interface ITechnicianEarningsRepository
    {
        Task<List<TechnicianEarnings>> GetByTechnicianIdAsync(int technicianId);
        Task<List<TechnicianEarnings>> GetUnpaidAsync();
        Task<List<TechnicianEarnings>> GetAllAsync();
        Task AddAsync(TechnicianEarnings earnings);
        Task UpdateAsync(TechnicianEarnings earnings);
        Task<int> GetNextEarningIdAsync();
    }

    public interface ISystemSettingsRepository
    {
        Task<SystemSettings> GetByKeyAsync(string key);
        Task<List<SystemSettings>> GetAllAsync();
        Task AddAsync(SystemSettings setting);
        Task UpdateAsync(SystemSettings setting);
        Task<int> GetNextSettingIdAsync();
    }
}
