using FixItNow.Application.Interfaces;
using FixItNow.Presentation.Helpers;
using System;
using System.Threading.Tasks;

namespace FixItNow.Presentation.Menus
{
    public class MainMenu
    {
        private readonly IAuthenticationService _authService;
        private readonly ResidentMenu _residentMenu;
        private readonly AdminMenu _adminMenu;
        private readonly TechnicianMenu _technicianMenu;

        public MainMenu(
            IAuthenticationService authService,
            ResidentMenu residentMenu,
            AdminMenu adminMenu,
            TechnicianMenu technicianMenu)
        {
            _authService = authService;
            _residentMenu = residentMenu;
            _adminMenu = adminMenu;
            _technicianMenu = technicianMenu;
        }

        public async Task ShowAsync()
        {
            while (true)
            {
                ConsoleHelper.PrintHeader("FixItNow - Main Menu");
                Console.WriteLine("1. Login");
                Console.WriteLine("2. Exit");
                Console.Write("\nSelect option: ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await LoginAsync();
                        break;
                    case "2":
                        Console.WriteLine("\nThank you for using FixItNow!");
                        return;
                    default:
                        Console.WriteLine("\n❌ Invalid option!");
                        ConsoleHelper.PressAnyKey();
                        break;
                }
            }
        }

        private async Task LoginAsync()
        {
            ConsoleHelper.PrintHeader("Login");

            Console.Write("Username: ");
            var username = Console.ReadLine();

            Console.Write("Password: ");
            var password = Console.ReadLine();

            try
            {
                var user = await _authService.LoginAsync(username, password);
                Console.WriteLine($"\n✅ Welcome, {user.FullName}!");
                await Task.Delay(1000);

                // Route to appropriate menu based on role
                switch (user.RoleId)
                {
                    case 1: // Resident
                        await _residentMenu.ShowAsync();
                        break;
                    case 2: // Admin
                        await _adminMenu.ShowAsync();
                        break;
                    case 3: // Technician
                        await _technicianMenu.ShowAsync();
                        break;
                }

                await _authService.LogoutAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n❌ Login failed: {ex.Message}");
                ConsoleHelper.PressAnyKey();
            }
        }
    }
}