using FixItNow.Domain.Entities;
using System.Threading.Tasks;

namespace FixItNow.Application.Interfaces
{
    public interface IAuthenticationService
    {
        Task<User> LoginAsync(string username, string password);
        Task LogoutAsync();
        User GetCurrentUser();
    }
}