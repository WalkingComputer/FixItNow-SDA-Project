using FixItNow.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FixItNow.Application.Patterns
{
    /// <summary>
    /// OBSERVER PATTERN (Behavioral) - Event-Driven Architecture
    /// Implements publish-subscribe mechanism for ticket events
    /// Allows multiple observers to react to ticket state changes
    /// </summary>
    
    // Subject interface
    public interface ITicketEventPublisher
    {
        void Subscribe(ITicketObserver observer);
        void Unsubscribe(ITicketObserver observer);
        Task NotifyTicketCreatedAsync(Ticket ticket);
        Task NotifyTicketAssignedAsync(Ticket ticket, int technicianId);
        Task NotifyTicketStatusChangedAsync(Ticket ticket, string oldStatus, string newStatus);
        Task NotifyTicketResolvedAsync(Ticket ticket);
    }

    // Observer interface
    public interface ITicketObserver
    {
        string ObserverName { get; }
        Task OnTicketCreated(Ticket ticket);
        Task OnTicketAssigned(Ticket ticket, int technicianId);
        Task OnTicketStatusChanged(Ticket ticket, string oldStatus, string newStatus);
        Task OnTicketResolved(Ticket ticket);
    }

    /// <summary>
    /// Concrete Subject: Ticket Event Publisher
    /// </summary>
    public class TicketEventPublisher : ITicketEventPublisher
    {
        private readonly List<ITicketObserver> _observers = new List<ITicketObserver>();

        public void Subscribe(ITicketObserver observer)
        {
            if (!_observers.Contains(observer))
            {
                _observers.Add(observer);
                Console.WriteLine($"📢 Observer Pattern: {observer.ObserverName} subscribed to ticket events");
            }
        }

        public void Unsubscribe(ITicketObserver observer)
        {
            if (_observers.Contains(observer))
            {
                _observers.Remove(observer);
                Console.WriteLine($"📢 Observer Pattern: {observer.ObserverName} unsubscribed from ticket events");
            }
        }

        public async Task NotifyTicketCreatedAsync(Ticket ticket)
        {
            Console.WriteLine($"\n🔔 EVENT: Ticket {ticket.TicketCode} created - Notifying {_observers.Count} observers...");
            foreach (var observer in _observers)
            {
                await observer.OnTicketCreated(ticket);
            }
        }

        public async Task NotifyTicketAssignedAsync(Ticket ticket, int technicianId)
        {
            Console.WriteLine($"\n🔔 EVENT: Ticket {ticket.TicketCode} assigned - Notifying {_observers.Count} observers...");
            foreach (var observer in _observers)
            {
                await observer.OnTicketAssigned(ticket, technicianId);
            }
        }

        public async Task NotifyTicketStatusChangedAsync(Ticket ticket, string oldStatus, string newStatus)
        {
            Console.WriteLine($"\n🔔 EVENT: Ticket {ticket.TicketCode} status changed from {oldStatus} to {newStatus} - Notifying {_observers.Count} observers...");
            foreach (var observer in _observers)
            {
                await observer.OnTicketStatusChanged(ticket, oldStatus, newStatus);
            }
        }

        public async Task NotifyTicketResolvedAsync(Ticket ticket)
        {
            Console.WriteLine($"\n🔔 EVENT: Ticket {ticket.TicketCode} resolved - Notifying {_observers.Count} observers...");
            foreach (var observer in _observers)
            {
                await observer.OnTicketResolved(ticket);
            }
        }
    }

    /// <summary>
    /// Concrete Observer: Admin Notification Observer
    /// </summary>
    public class AdminNotificationObserver : ITicketObserver
    {
        public string ObserverName => "Admin Notification System";

        public async Task OnTicketCreated(Ticket ticket)
        {
            Console.WriteLine($"   👨‍💼 {ObserverName}: Sending notification to all admins about new ticket {ticket.TicketCode}");
            await Task.Delay(50); // Simulate notification sending
        }

        public async Task OnTicketAssigned(Ticket ticket, int technicianId)
        {
            Console.WriteLine($"   👨‍💼 {ObserverName}: Logging ticket assignment in admin dashboard");
            await Task.Delay(50);
        }

        public async Task OnTicketStatusChanged(Ticket ticket, string oldStatus, string newStatus)
        {
            Console.WriteLine($"   👨‍💼 {ObserverName}: Updating admin dashboard with status change");
            await Task.Delay(50);
        }

        public async Task OnTicketResolved(Ticket ticket)
        {
            Console.WriteLine($"   👨‍💼 {ObserverName}: Notifying admins of ticket resolution");
            await Task.Delay(50);
        }
    }

    /// <summary>
    /// Concrete Observer: Resident Notification Observer
    /// </summary>
    public class ResidentNotificationObserver : ITicketObserver
    {
        public string ObserverName => "Resident Notification System";

        public async Task OnTicketCreated(Ticket ticket)
        {
            Console.WriteLine($"   👤 {ObserverName}: Confirming ticket creation to resident");
            await Task.Delay(50);
        }

        public async Task OnTicketAssigned(Ticket ticket, int technicianId)
        {
            Console.WriteLine($"   👤 {ObserverName}: Notifying resident that technician has been assigned");
            await Task.Delay(50);
        }

        public async Task OnTicketStatusChanged(Ticket ticket, string oldStatus, string newStatus)
        {
            Console.WriteLine($"   👤 {ObserverName}: Updating resident about ticket progress");
            await Task.Delay(50);
        }

        public async Task OnTicketResolved(Ticket ticket)
        {
            Console.WriteLine($"   👤 {ObserverName}: Notifying resident that ticket is resolved");
            await Task.Delay(50);
        }
    }

    /// <summary>
    /// Concrete Observer: Technician Notification Observer
    /// </summary>
    public class TechnicianNotificationObserver : ITicketObserver
    {
        public string ObserverName => "Technician Notification System";

        public async Task OnTicketCreated(Ticket ticket)
        {
            Console.WriteLine($"   🔧 {ObserverName}: Adding ticket to available tickets pool");
            await Task.Delay(50);
        }

        public async Task OnTicketAssigned(Ticket ticket, int technicianId)
        {
            Console.WriteLine($"   🔧 {ObserverName}: Notifying technician #{technicianId} of new assignment");
            await Task.Delay(50);
        }

        public async Task OnTicketStatusChanged(Ticket ticket, string oldStatus, string newStatus)
        {
            Console.WriteLine($"   🔧 {ObserverName}: Updating technician's work queue");
            await Task.Delay(50);
        }

        public async Task OnTicketResolved(Ticket ticket)
        {
            Console.WriteLine($"   🔧 {ObserverName}: Marking ticket as complete in technician's dashboard");
            await Task.Delay(50);
        }
    }

    /// <summary>
    /// Concrete Observer: Audit Log Observer
    /// </summary>
    public class AuditLogObserver : ITicketObserver
    {
        public string ObserverName => "Audit Logging System";

        public async Task OnTicketCreated(Ticket ticket)
        {
            Console.WriteLine($"   📝 {ObserverName}: Logging ticket creation event");
            await Task.Delay(50);
        }

        public async Task OnTicketAssigned(Ticket ticket, int technicianId)
        {
            Console.WriteLine($"   📝 {ObserverName}: Logging ticket assignment event");
            await Task.Delay(50);
        }

        public async Task OnTicketStatusChanged(Ticket ticket, string oldStatus, string newStatus)
        {
            Console.WriteLine($"   📝 {ObserverName}: Logging status change event");
            await Task.Delay(50);
        }

        public async Task OnTicketResolved(Ticket ticket)
        {
            Console.WriteLine($"   📝 {ObserverName}: Logging ticket resolution event");
            await Task.Delay(50);
        }
    }
}
