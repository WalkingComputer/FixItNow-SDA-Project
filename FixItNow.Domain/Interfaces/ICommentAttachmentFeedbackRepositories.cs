using FixItNow.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FixItNow.Domain.Interfaces
{
    public interface ICommentRepository
    {
        Task<Comment> GetByIdAsync(int commentId);
        Task<List<Comment>> GetByTicketIdAsync(int ticketId);
        Task<List<Comment>> GetAllAsync();
        Task AddAsync(Comment comment);
        Task UpdateAsync(Comment comment);
        Task DeleteAsync(int commentId);
        Task<int> GetNextCommentIdAsync();
    }

    public interface IAttachmentRepository
    {
        Task<Attachment> GetByIdAsync(int attachmentId);
        Task<List<Attachment>> GetByTicketIdAsync(int ticketId);
        Task<List<Attachment>> GetAllAsync();
        Task AddAsync(Attachment attachment);
        Task DeleteAsync(int attachmentId);
        Task<int> GetNextAttachmentIdAsync();
    }

    public interface IFeedbackRepository
    {
        Task<Feedback> GetByIdAsync(int feedbackId);
        Task<List<Feedback>> GetByUserIdAsync(int userId);
        Task<List<Feedback>> GetAllAsync();
        Task AddAsync(Feedback feedback);
        Task UpdateAsync(Feedback feedback);
        Task<int> GetNextFeedbackIdAsync();
    }
}
