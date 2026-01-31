using FixItNow.Application.Interfaces;
using FixItNow.Application.Services;
using FixItNow.Domain.Interfaces;
using FixItNow.Infrastructure.Data;
using FixItNow.Infrastructure.Repositories;
using FixItNow.Infrastructure.UnitOfWork;
using FixItNow.Presentation.Menus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Threading.Tasks;

namespace FixItNow.Presentation
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                // Setup Configuration
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .Build();

                // Setup Dependency Injection
                var serviceProvider = ConfigureServices(configuration);

                // Seed Data
                Console.WriteLine("Initializing database...");
                var context = serviceProvider.GetService<MongoDbContext>();
                var seeder = new SeedData(context);
                await seeder.SeedAllAsync();
                Console.WriteLine();

                // Run Application
                Console.Clear();
                Console.WriteLine("╔════════════════════════════════════════════════════════╗");
                Console.WriteLine("║                   FixItNow System                      ║");
                Console.WriteLine("║          Hostel Maintenance Ticket Manager            ║");
                Console.WriteLine("╚════════════════════════════════════════════════════════╝");
                Console.WriteLine();

                var mainMenu = serviceProvider.GetService<MainMenu>();
                await mainMenu.ShowAsync();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n❌ Fatal Error: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                Console.ResetColor();
            }
        }

        private static ServiceProvider ConfigureServices(IConfiguration configuration)
        {
            var services = new ServiceCollection();

            // Configuration
            var mongoSettings = new MongoDbSettings
            {
                ConnectionString = configuration["MongoDbSettings:ConnectionString"],
                DatabaseName = configuration["MongoDbSettings:DatabaseName"]
            };
            services.AddSingleton(mongoSettings);

            // MongoDB Context
            services.AddSingleton<MongoDbContext>();

            // Repositories
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITicketRepository, TicketRepository>();
            services.AddScoped<ITicketAssignmentRepository, TicketAssignmentRepository>();
            services.AddScoped<IAuditLogRepository, AuditLogRepository>();
            services.AddScoped<INotificationRepository, NotificationRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // ✨ NEW: Factories
            services.AddScoped<INotificationFactory, NotificationFactory>();

            // ✨ NEW: New Services

            // Application Services
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<ITicketService, TicketService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITechnicianSelectionService, TechnicianSelectionService>();

            // Presentation (Menus)
            services.AddTransient<MainMenu>();
            services.AddTransient<ResidentMenu>();
            services.AddTransient<AdminMenu>();
            services.AddTransient<TechnicianMenu>();

            return services.BuildServiceProvider();
        }
    }
}