using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using FixItNow.Domain.Entities;
using FixItNow.Domain.Interfaces;
using FixItNow.Infrastructure.Data;

namespace FixItNow.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> _users;

        public UserRepository(MongoDbContext context)
        {
            _users = context.Users;
        }

        public async Task<User> GetByIdAsync(int userId)
        {
            var filter = Builders<User>.Filter.Eq(u => u.UserId, userId);
            return await _users.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<User> GetByUsernameAsync(string username)
        {
            var filter = Builders<User>.Filter.Eq(u => u.Username, username);
            return await _users.Find(filter).FirstOrDefaultAsync();
        }

        public async Task AddAsync(User user)
        {
            await _users.InsertOneAsync(user);
        }

        public async Task UpdateAsync(User user)
        {
            var filter = Builders<User>.Filter.Eq(u => u.UserId, user.UserId);
            await _users.ReplaceOneAsync(filter, user);
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _users.Find(_ => true).ToListAsync();
        }
    }
}