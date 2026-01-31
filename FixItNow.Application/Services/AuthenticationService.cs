using FixItNow.Application.Interfaces;
using FixItNow.Domain.Entities;
using FixItNow.Domain.Interfaces;
using System;
using System.Threading.Tasks;

namespace FixItNow.Application.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _userRepository;
        private User _currentUser;

        public AuthenticationService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> LoginAsync(string username, string password)
        {
            var user = await _userRepository.GetByUsernameAsync(username);

            if (user == null)
                throw new Exception("User not found!");

            if (!user.IsActive)
                throw new Exception("User account is deactivated!");

            // TODO: Implement proper password hashing comparison
            if (user.PasswordHash != password)
                throw new Exception("Invalid password!");

            _currentUser = user;
            return user;
        }

        public async Task LogoutAsync()
        {
            _currentUser = null;
            await Task.CompletedTask;
        }

        public User GetCurrentUser()
        {
            return _currentUser;
        }
    }
}