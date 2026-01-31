using FixItNow.Application.DTOs;
using FixItNow.Domain.Entities;
using FixItNow.Domain.Enums;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FixItNow.Application.Interfaces
{
    public interface ITicketService
    {
        Task<Ticket> CreateTicketAsync(CreateTicketRequest request);
        Task<Ticket> CreateTicketAsync(CreateTicketRequest request, int selectedTechnicianId);
        Task AssignTicketAsync(int ticketId, int technicianId, int adminId);
        Task ChangeStatusAsync(int ticketId, TicketStatusEnum newStatus, int performedByUserId);
        Task<Comment> AddCommentAsync(int ticketId, int authorUserId, string text, bool isInternal);
        Task<List<Ticket>> GetAllTicketsAsync();
        Task<List<Ticket>> GetTicketsByStatusAsync(int statusId);
        Task<List<Ticket>> GetMyTicketsAsync(int userId);
        Task<List<Ticket>> GetAssignedTicketsAsync(int technicianId);
        Task<Ticket> GetTicketByIdAsync(int ticketId);
    }
}