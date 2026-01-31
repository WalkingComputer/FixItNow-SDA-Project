using FixItNow.Domain.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace FixItNow.Infrastructure.Data
{
    public class SeedData
    {
        private readonly MongoDbContext _context;

        public SeedData(MongoDbContext context)
        {
            _context = context;
        }

        public async Task SeedAllAsync()
        {
            await SeedRolesAsync();
            await SeedCategoriesAsync();
            await SeedPrioritiesAsync();
            await SeedTicketStatusesAsync();
            await SeedLocationsAsync();
            await SeedDefaultUsersAsync();
        }

        private async Task SeedRolesAsync()
        {
            var count = await _context.Roles.CountDocumentsAsync(_ => true);
            if (count > 0) return;

            var roles = new List<Role>
            {
                new Role { RoleId = 1, RoleName = "Resident" },
                new Role { RoleId = 2, RoleName = "Admin" },
                new Role { RoleId = 3, RoleName = "Technician" }
            };

            await _context.Roles.InsertManyAsync(roles);
            Console.WriteLine("✅ Roles seeded: Resident, Admin, Technician");
        }

        private async Task SeedCategoriesAsync()
        {
            var count = await _context.Categories.CountDocumentsAsync(_ => true);
            if (count > 0) return;

            var categories = new List<Category>
            {
                new Category { CategoryId = 1, CategoryName = "Plumbing", IsActive = true },
                new Category { CategoryId = 2, CategoryName = "Electric", IsActive = true },
                new Category { CategoryId = 3, CategoryName = "WiFi", IsActive = true },
                new Category { CategoryId = 4, CategoryName = "Furniture", IsActive = true },
                new Category { CategoryId = 5, CategoryName = "Other", IsActive = true }
            };

            await _context.Categories.InsertManyAsync(categories);
            Console.WriteLine("✅ Categories seeded: 5 categories");
        }

        private async Task SeedPrioritiesAsync()
        {
            var count = await _context.Priorities.CountDocumentsAsync(_ => true);
            if (count > 0) return;

            var priorities = new List<Priority>
            {
                new Priority { PriorityId = 1, PriorityName = "Low", PriorityOrder = 1 },
                new Priority { PriorityId = 2, PriorityName = "Medium", PriorityOrder = 2 },
                new Priority { PriorityId = 3, PriorityName = "High", PriorityOrder = 3 },
                new Priority { PriorityId = 4, PriorityName = "Urgent", PriorityOrder = 4 }
            };

            await _context.Priorities.InsertManyAsync(priorities);
            Console.WriteLine("✅ Priorities seeded: Low, Medium, High, Urgent");
        }

        private async Task SeedTicketStatusesAsync()
        {
            var count = await _context.TicketStatuses.CountDocumentsAsync(_ => true);
            if (count > 0) return;

            var statuses = new List<TicketStatus>
            {
                new TicketStatus { StatusId = 1, StatusName = "Open" },
                new TicketStatus { StatusId = 2, StatusName = "Assigned" },
                new TicketStatus { StatusId = 3, StatusName = "InProgress" },
                new TicketStatus { StatusId = 4, StatusName = "Resolved" },
                new TicketStatus { StatusId = 5, StatusName = "Closed" },
                new TicketStatus { StatusId = 6, StatusName = "Reopened" }
            };

            await _context.TicketStatuses.InsertManyAsync(statuses);
            Console.WriteLine("✅ Ticket Statuses seeded: 6 statuses");
        }

        private async Task SeedLocationsAsync()
        {
            var count = await _context.Locations.CountDocumentsAsync(_ => true);
            if (count > 0) return;

            var locations = new List<Location>
            {
                new Location { LocationId = 1, HostelName = "Boys Hostel A", Block = "Block 1", Floor = "Ground", RoomNumber = "101" },
                new Location { LocationId = 2, HostelName = "Boys Hostel A", Block = "Block 1", Floor = "First", RoomNumber = "201" },
                new Location { LocationId = 3, HostelName = "Girls Hostel B", Block = "Block 2", Floor = "Ground", RoomNumber = "102" },
                new Location { LocationId = 4, HostelName = "Common Area", Block = "Main", Floor = "Ground", RoomNumber = "Cafeteria" }
            };

            await _context.Locations.InsertManyAsync(locations);
            Console.WriteLine("✅ Locations seeded: 4 locations");
        }

        private async Task SeedDefaultUsersAsync()
        {
            var count = await _context.Users.CountDocumentsAsync(_ => true);
            if (count > 0) return;

            var users = new List<User>
            {
                // Admin user
                new User
                {
                    UserId = 1,
                    RoleId = 2,
                    FullName = "Admin User",
                    Username = "admin",
                    Email = "admin@fixitnow.com",
                    PasswordHash = "admin123", // In production, use proper hashing
                    Phone = "0300-1234567",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                },
                // Technician user
                new User
                {
                    UserId = 2,
                    RoleId = 3,
                    FullName = "John Technician",
                    Username = "tech1",
                    Email = "tech1@fixitnow.com",
                    PasswordHash = "tech123",
                    Phone = "0300-7654321",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                },
                // Resident user
                new User
                {
                    UserId = 3,
                    RoleId = 1,
                    FullName = "Ali Resident",
                    Username = "resident1",
                    Email = "resident1@student.com",
                    PasswordHash = "resident123",
                    Phone = "0300-1112222",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                }
            };

            await _context.Users.InsertManyAsync(users);
            Console.WriteLine("✅ Default users seeded: admin, tech1, resident1");
        }
    }
}