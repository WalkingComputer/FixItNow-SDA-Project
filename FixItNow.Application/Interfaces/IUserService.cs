using FixItNow.Application.DTOs;
using FixItNow.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FixItNow.Application.Interfaces
{
    public interface IUserService
    {
        Task<User> RegisterUserAsync(RegisterUserRequest request);
        Task DeactivateUserAsync(int userId);
        Task<User> GetUserByIdAsync(int userId);
        Task<List<User>> GetAllUsersAsync();
        Task<List<User>> GetTechniciansAsync();
    }
}