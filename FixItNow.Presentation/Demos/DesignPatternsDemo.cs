using FixItNow.Application.Patterns;
using FixItNow.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace FixItNow.Presentation.Demos
{
    /// <summary>
    /// Demonstration class showing all implemented design patterns
    /// Run this to see patterns in action
    /// </summary>
    public class DesignPatternsDemo
    {
        public static async Task RunAllPatternsDemo()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╔════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║          DESIGN PATTERNS DEMONSTRATION                         ║");
            Console.WriteLine("║     Showcasing Classical GoF Patterns Implementation           ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════════════╝");
            Console.ResetColor();

            await DemoSingletonPattern();
            await DemoStrategyPattern();
            await DemoObserverPattern();
            await DemoFacadePattern();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n✅ All design patterns demonstrated successfully!");
            Console.ResetColor();
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        private static async Task DemoSingletonPattern()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n\n═══════════════════════════════════════════════════════════");
            Console.WriteLine("  1. SINGLETON PATTERN (Creational)");
            Console.WriteLine("═══════════════════════════════════════════════════════════");
            Console.ResetColor();

            Console.WriteLine("\n📌 Purpose: Ensure only ONE instance of database connection manager exists");
            Console.WriteLine("📌 Benefits: Resource management, global access point, thread-safe\n");

            var instance1 = Infrastructure.Data.DatabaseConnectionManager.Instance;
            Console.WriteLine($"Instance 1: {instance1.GetConnectionInfo()}");

            await Task.Delay(100);

            var instance2 = Infrastructure.Data.DatabaseConnectionManager.Instance;
            Console.WriteLine($"Instance 2: {instance2.GetConnectionInfo()}");

            Console.WriteLine($"\n✓ Both references point to SAME instance: {ReferenceEquals(instance1, instance2)}");
        }

        private static async Task DemoStrategyPattern()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n\n═══════════════════════════════════════════════════════════");
            Console.WriteLine("  2. STRATEGY PATTERN (Behavioral)");
            Console.WriteLine("═══════════════════════════════════════════════════════════");
            Console.ResetColor();

            Console.WriteLine("\n📌 Purpose: Define family of algorithms and make them interchangeable");
            Console.WriteLine("📌 Benefits: Runtime algorithm selection, open/closed principle\n");

            var processor = new PaymentProcessor();

            // Strategy 1: Cash Payment
            processor.SetPaymentStrategy(new CashPaymentStrategy());
            var result1 = await processor.ProcessPaymentAsync(500, 1, 1);
            Console.WriteLine($"   Result: {result1.Message} | Ref: {result1.TransactionReference}");

            await Task.Delay(500);

            // Strategy 2: Online Transfer
            processor.SetPaymentStrategy(new OnlineTransferPaymentStrategy());
            var result2 = await processor.ProcessPaymentAsync(750, 1, 2);
            Console.WriteLine($"   Result: {result2.Message} | Ref: {result2.TransactionReference}");

            Console.WriteLine("\n✓ Payment method changed at runtime without modifying client code!");
        }

        private static async Task DemoObserverPattern()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n\n═══════════════════════════════════════════════════════════");
            Console.WriteLine("  3. OBSERVER PATTERN (Behavioral) - Event-Driven Architecture");
            Console.WriteLine("═══════════════════════════════════════════════════════════");
            Console.ResetColor();

            Console.WriteLine("\n📌 Purpose: Define one-to-many dependency for event notification");
            Console.WriteLine("📌 Benefits: Loose coupling, scalable event handling, reactive system\n");

            // Create publisher
            var publisher = new TicketEventPublisher();

            // Create observers
            var adminObserver = new AdminNotificationObserver();
            var residentObserver = new ResidentNotificationObserver();
            var technicianObserver = new TechnicianNotificationObserver();
            var auditObserver = new AuditLogObserver();

            // Subscribe observers
            publisher.Subscribe(adminObserver);
            publisher.Subscribe(residentObserver);
            publisher.Subscribe(technicianObserver);
            publisher.Subscribe(auditObserver);

            // Create a sample ticket
            var ticket = new Ticket
            {
                TicketId = 999,
                TicketCode = "TKT-00999",
                Title = "Demo Ticket for Pattern Testing",
                CreatedByUserId = 1
            };

            // Publish event - all observers will be notified
            await publisher.NotifyTicketCreatedAsync(ticket);

            await Task.Delay(500);

            // Publish another event
            await publisher.NotifyTicketAssignedAsync(ticket, 5);

            Console.WriteLine("\n✓ All 4 observers notified automatically when events occurred!");
        }

        private static async Task DemoFacadePattern()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n\n═══════════════════════════════════════════════════════════");
            Console.WriteLine("  4. FACADE PATTERN (Structural)");
            Console.WriteLine("═══════════════════════════════════════════════════════════");
            Console.ResetColor();

            Console.WriteLine("\n📌 Purpose: Provide simplified interface to complex subsystems");
            Console.WriteLine("📌 Benefits: Reduced complexity, easier to use, decouples client from subsystems\n");

            Console.WriteLine("Without Facade: Client must call multiple services:");
            Console.WriteLine("   1. TicketService.CreateTicket()");
            Console.WriteLine("   2. NotificationService.NotifyAdmins()");
            Console.WriteLine("   3. AuditService.LogCreation()");
            Console.WriteLine("   4. InvoiceService.GenerateInvoice()");

            Console.WriteLine("\nWith Facade: Single simplified call:");
            Console.WriteLine("   TicketManagementFacade.CreateTicketWithNotifications()");

            Console.WriteLine("\n✓ Facade hides complexity and coordinates multiple subsystems!");

            await Task.CompletedTask;
        }
    }
}
