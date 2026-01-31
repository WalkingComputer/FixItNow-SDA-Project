using System.Collections.Generic;
using System.Threading.Tasks;
using FixItNow.Domain.Entities;

namespace FixItNow.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetByIdAsync(int userId);
        Task<User> GetByUsernameAsync(string username);
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task<List<User>> GetAllAsync();
    }
}