using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FixItNow.Application.DTOs;
using FixItNow.Application.Factories;
using FixItNow.Application.Interfaces;
using FixItNow.Domain.Entities;
using FixItNow.Domain.Enums;
using FixItNow.Domain.Interfaces;

namespace FixItNow.Application.Services
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly ITicketAssignmentRepository _assignmentRepository;
        private readonly IAuditLogRepository _auditRepository;
        private readonly INotificationRepository _notificationRepository;
        private readonly INotificationFactory _notificationFactory;

        public TicketService(
            ITicketRepository ticketRepository,
            ITicketAssignmentRepository assignmentRepository,
            IAuditLogRepository auditRepository,
            INotificationRepository notificationRepository,
            INotificationFactory notificationFactory)
        {
            _ticketRepository = ticketRepository;
            _assignmentRepository = assignmentRepository;
            _auditRepository = auditRepository;
            _notificationRepository = notificationRepository;
            _notificationFactory = notificationFactory;
        }

        public async Task<Ticket> CreateTicketAsync(CreateTicketRequest request)
        {
            // Get next IDs
            var allTickets = await _ticketRepository.GetAllAsync();
            var nextTicketId = allTickets.Any() ? allTickets.Max(t => t.TicketId) + 1 : 1;

            var ticket = new Ticket
            {
                TicketId = nextTicketId,
                TicketCode = $"TKT-{nextTicketId:D5}",
                Title = request.Title,
                Description = request.Description,
                CategoryId = request.CategoryId,
                LocationId = request.LocationId,
                PriorityId = request.PriorityId,
                StatusId = 1, // Open
                CreatedByUserId = request.CreatedByUserId,
                CreatedAt = DateTime.UtcNow
            };

            await _ticketRepository.AddAsync(ticket);

            // Create notification for admins
            await CreateNotificationAsync(2, ticket.TicketId, "NEW_TICKET",
                $"New ticket created: {ticket.Title}");

            // Audit log
            await LogAuditAsync(ticket.TicketId, "Ticket", ticket.TicketId, "CREATE",
                $"Ticket {ticket.TicketCode} created", request.CreatedByUserId);

            return ticket;
        }

        public async Task AssignTicketAsync(int ticketId, int technicianId, int adminId)
        {
            var ticket = await _ticketRepository.GetByIdAsync(ticketId);
            if (ticket == null)
                throw new Exception("Ticket not found!");

            // Unassign previous technician if exists
            var activeAssignment = await _assignmentRepository.GetActiveByTicketAsync(ticketId);
            if (activeAssignment != null)
            {
                activeAssignment.IsActive = false;
                activeAssignment.UnassignedAt = DateTime.UtcNow;
                // Note: You'll need to add Update method to ITicketAssignmentRepository
            }

            // Create new assignment
            var allAssignments = await _assignmentRepository.GetHistoryByTicketAsync(ticketId);
            var nextAssignmentId = allAssignments.Any() ? allAssignments.Max(a => a.AssignmentId) + 1 : 1;

            var assignment = new TicketAssignment
            {
                AssignmentId = nextAssignmentId,
                TicketId = ticketId,
                TechnicianId = technicianId,
                AssignedByAdminId = adminId,
                AssignedAt = DateTime.UtcNow,
                IsActive = true
            };

            await _assignmentRepository.AddAsync(assignment);

            // Update ticket
            ticket.CurrentTechnicianId = technicianId;
            ticket.StatusId = 2; // Assigned
            ticket.UpdatedAt = DateTime.UtcNow;
            await _ticketRepository.UpdateAsync(ticket);

            // Notify technician
            await CreateNotificationAsync(technicianId, ticketId, "ASSIGNED",
                $"You have been assigned ticket: {ticket.TicketCode}");

            // Audit log
            await LogAuditAsync(ticketId, "TicketAssignment", assignment.AssignmentId, "ASSIGN",
                $"Ticket assigned to technician ID {technicianId}", adminId);
        }

        public async Task ChangeStatusAsync(int ticketId, TicketStatusEnum newStatus, int performedByUserId)
        {
            var ticket = await _ticketRepository.GetByIdAsync(ticketId);
            if (ticket == null)
                throw new Exception("Ticket not found!");

            var oldStatus = ticket.StatusId;
            ticket.StatusId = (int)newStatus;
            ticket.UpdatedAt = DateTime.UtcNow;

            // Update lifecycle timestamps
            switch (newStatus)
            {
                case TicketStatusEnum.InProgress:
                    // No specific timestamp
                    break;
                case TicketStatusEnum.Resolved:
                    ticket.ResolvedAt = DateTime.UtcNow;
                    break;
                case TicketStatusEnum.Closed:
                    ticket.ClosedAt = DateTime.UtcNow;
                    break;
                case TicketStatusEnum.Reopened:
                    ticket.ReopenedAt = DateTime.UtcNow;
                    ticket.ResolvedAt = null;
                    ticket.ClosedAt = null;
                    break;
            }

            await _ticketRepository.UpdateAsync(ticket);

            // Notify resident
            await CreateNotificationAsync(ticket.CreatedByUserId, ticketId, "STATUS_CHANGE",
                $"Ticket {ticket.TicketCode} status changed to {newStatus}");

            // Audit log
            await LogAuditAsync(ticketId, "Ticket", ticketId, "STATUS_CHANGE",
                $"Status changed from {oldStatus} to {(int)newStatus}", performedByUserId);
        }

        public async Task<Comment> AddCommentAsync(int ticketId, int authorUserId, string text, bool isInternal)
        {
            var ticket = await _ticketRepository.GetByIdAsync(ticketId);
            if (ticket == null)
                throw new Exception("Ticket not found!");

            // Get next CommentId (you'll need to add a Comments repository or use a counter)
            var nextCommentId = 1; // Simplified for now

            var comment = new Comment
            {
                CommentId = nextCommentId,
                TicketId = ticketId,
                AuthorUserId = authorUserId,
                CommentText = text,
                IsInternal = isInternal,
                CreatedAt = DateTime.UtcNow
            };

            // You'll need to add ICommentRepository
            // await _commentRepository.AddAsync(comment);

            // Audit log
            await LogAuditAsync(ticketId, "Comment", comment.CommentId, "ADD_COMMENT",
                $"Comment added to ticket {ticket.TicketCode}", authorUserId);

            return comment;
        }

        public async Task<List<Ticket>> GetAllTicketsAsync()
        {
            return await _ticketRepository.GetAllAsync();
        }

        public async Task<List<Ticket>> GetTicketsByStatusAsync(int statusId)
        {
            return await _ticketRepository.FindByStatusAsync(statusId);
        }

        public async Task<List<Ticket>> GetMyTicketsAsync(int userId)
        {
            var allTickets = await _ticketRepository.GetAllAsync();
            return allTickets.Where(t => t.CreatedByUserId == userId).ToList();
        }

        public async Task<List<Ticket>> GetAssignedTicketsAsync(int technicianId)
        {
            var allTickets = await _ticketRepository.GetAllAsync();
            return allTickets.Where(t => t.CurrentTechnicianId == technicianId).ToList();
        }

        private async Task CreateNotificationAsync(int userId, int ticketId, string type, string message)
        {
            var allNotifications = await _notificationRepository.FindUnreadByUserAsync(userId);
            var nextNotificationId = allNotifications.Any() ? allNotifications.Max(n => n.NotificationId) + 1 : 1;

            // ✨ USE FACTORY TO CREATE APPROPRIATE NOTIFICATION TYPE
            var notification = _notificationFactory.CreateNotification("InApp");
            notification.NotificationId = nextNotificationId;
            notification.UserId = userId;
            notification.TicketId = ticketId;
            notification.Message = message;
            notification.CreatedAt = DateTime.UtcNow;

            // Save to database
            var dbNotification = new Notification
            {
                NotificationId = nextNotificationId,
                UserId = userId,
                TicketId = ticketId,
                Type = type,
                Message = message,
                IsRead = false,
                CreatedAt = DateTime.UtcNow
            };

            await _notificationRepository.AddAsync(dbNotification);

            // Send through appropriate channel
            await notification.SendAsync();
        }

        private async Task LogAuditAsync(int? ticketId, string entityType, int entityId, string action, string details, int performedBy)
        {
            var allLogs = await _auditRepository.FindByTicketAsync(ticketId);
            var nextAuditId = allLogs.Any() ? allLogs.Max(a => a.AuditId) + 1 : 1;

            var auditLog = new AuditLog
            {
                AuditId = nextAuditId,
                TicketId = ticketId,
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