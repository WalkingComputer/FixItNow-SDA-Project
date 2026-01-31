using System;
using System.Collections.Generic;
using System.Linq;  // ← ADD THIS LINE
using System.Threading.Tasks;
using FixItNow.Application.DTOs;
using FixItNow.Application.Interfaces;
using FixItNow.Domain.Entities;
using FixItNow.Domain.Interfaces;
using MongoDB.Driver;

namespace FixItNow.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuditLogRepository _auditRepository;

        public UserService(IUserRepository userRepository, IAuditLogRepository auditRepository)
        {
            _userRepository = userRepository;
            _auditRepository = auditRepository;
        }

        public async Task<User> RegisterUserAsync(RegisterUserRequest request)
        {
            // Validation
            var existingUser = await _userRepository.GetByUsernameAsync(request.Username);
            if (existingUser != null)
                throw new Exception("Username already exists!");

            // Get next UserId
            var allUsers = await _userRepository.GetAllAsync();
            var nextId = allUsers.Any() ? allUsers.Max(u => u.UserId) + 1 : 1;

            // Create user
            var user = new User
            {
                UserId = nextId,
                RoleId = request.RoleId,
                FullName = request.FullName,
                Username = request.Username,
                Email = request.Email,
                PasswordHash = request.Password, // TODO: Implement proper hashing
                Phone = request.Phone,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            await _userRepository.AddAsync(user);

            // Audit log
            await LogAuditAsync("User", user.UserId, "CREATE", $"User {user.Username} registered", user.UserId);

            return user;
        }

        public async Task DeactivateUserAsync(int userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
                throw new Exception("User not found!");

            user.IsActive = false;
            await _userRepository.UpdateAsync(user);

            await LogAuditAsync("User", userId, "DEACTIVATE", $"User {user.Username} deactivated", userId);
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            return await _userRepository.GetByIdAsync(userId);
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task<List<User>> GetTechniciansAsync()
        {
            var allUsers = await _userRepository.GetAllAsync();
            return allUsers.Where(u => u.RoleId == 3 && u.IsActive).ToList();
        }

        private async Task LogAuditAsync(string entityType, int entityId, string action, string details, int performedBy)
        {
            var allLogs = await _auditRepository.FindByTicketAsync(null);
            var nextAuditId = allLogs.Any() ? allLogs.Max(a => a.AuditId) + 1 : 1;

            var auditLog = new AuditLog
            {
                AuditId = nextAuditId,
                EntityType = entityType,
                EntityId = entityId,
                Action = action,
                Details = details,
                PerformedByUserId = performedBy,
                PerformedAt = DateTime.UtcNow
            };

            await _auditRepository.AddAsync(auditLog);
        }
    }
}