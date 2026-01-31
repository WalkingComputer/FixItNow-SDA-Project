using FixItNow.Domain.Entities;
using FixItNow.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FixItNow.Application.Services
{
    /// <summary>
    /// ? COMMENT SERVICE - Manage ticket comments and discussions
    /// </summary>
    public interface ICommentService
    {
        Task<Comment> AddCommentAsync(int ticketId, int userId, string commentText, bool isInternal = false);
        Task<List<Comment>> GetTicketCommentsAsync(int ticketId);
    }

    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CommentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Comment> AddCommentAsync(int ticketId, int userId, string commentText, bool isInternal = false)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(userId);
            if (user == null)
                throw new Exception("User not found");

            var commentId = await _unitOfWork.Comments.GetNextCommentIdAsync();

            var comment = new Comment
            {
                CommentId = commentId,
                TicketId = ticketId,
                AuthorUserId = userId,
                CommentText = commentText,
                CreatedAt = DateTime.Now,
                IsInternal = isInternal
            };

            await _unitOfWork.Comments.AddAsync(comment);
            return comment;
        }

        public async Task<List<Comment>> GetTicketCommentsAsync(int ticketId)
        {
            return await _unitOfWork.Comments.GetByTicketIdAsync(ticketId);
        }
    }

    /// <summary>
    /// ? ATTACHMENT SERVICE - Manage ticket attachments
    /// </summary>
    public interface IAttachmentService
    {
        Task<Attachment> UploadAttachmentAsync(int ticketId, int userId, string fileName, string filePathOrUrl);
        Task<List<Attachment>> GetTicketAttachmentsAsync(int ticketId);
    }

    public class AttachmentService : IAttachmentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AttachmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Attachment> UploadAttachmentAsync(int ticketId, int userId, string fileName, string filePathOrUrl)
        {
            var ticket = await _unitOfWork.Tickets.GetByIdAsync(ticketId);
            if (ticket == null)
                throw new Exception("Ticket not found");

            var attachmentId = await _unitOfWork.Attachments.GetNextAttachmentIdAsync();

            var attachment = new Attachment
            {
                AttachmentId = attachmentId,
                TicketId = ticketId,
                UploadedByUserId = userId,
                FileName = fileName,
                FilePathOrUrl = filePathOrUrl ?? $"/uploads/tickets/{ticketId}/{fileName}",
                UploadedAt = DateTime.Now
            };

            await _unitOfWork.Attachments.AddAsync(attachment);
            return attachment;
        }

        public async Task<List<Attachment>> GetTicketAttachmentsAsync(int ticketId)
        {
            return await _unitOfWork.Attachments.GetByTicketIdAsync(ticketId);
        }
    }

    /// <summary>
    /// ? FEEDBACK SERVICE - Collect and manage system feedback
    /// </summary>
    public interface IFeedbackService
    {
        Task<Feedback> SubmitFeedbackAsync(int residentUserId, string feedbackText, int rating, int? ticketId = null);
        Task<List<Feedback>> GetResidentFeedbackAsync(int residentUserId);
        Task<List<Feedback>> GetAllFeedbackAsync();
    }

    public class FeedbackService : IFeedbackService
    {
        private readonly IUnitOfWork _unitOfWork;

        public FeedbackService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Feedback> SubmitFeedbackAsync(int residentUserId, string feedbackText, int rating, int? ticketId = null)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(residentUserId);
            if (user == null)
                throw new Exception("User not found");

            var feedbackId = await _unitOfWork.Feedbacks.GetNextFeedbackIdAsync();

            var feedback = new Feedback
            {
                FeedbackId = feedbackId,
                ResidentUserId = residentUserId,
                TicketId = ticketId ?? 0,
                Rating = rating,
                FeedbackText = feedbackText,
                CreatedAt = DateTime.Now
            };

            await _unitOfWork.Feedbacks.AddAsync(feedback);
            return feedback;
        }

        public async Task<List<Feedback>> GetResidentFeedbackAsync(int residentUserId)
        {
            return await _unitOfWork.Feedbacks.GetByUserIdAsync(residentUserId);
        }

        public async Task<List<Feedback>> GetAllFeedbackAsync()
        {
            return await _unitOfWork.Feedbacks.GetAllAsync();
        }
    }

    /// <summary>
    /// ? CHAT SERVICE - Real-time messaging between users
    /// </summary>
    public interface IChatService
    {
        Task<ChatMessage> SendMessageAsync(int ticketId, int senderId, int receiverId, string messageText);
        Task<List<ChatMessage>> GetConversationAsync(int userId1, int userId2, int ticketId);
        Task<List<ChatMessage>> GetTicketChatAsync(int ticketId);
    }

    public class ChatService : IChatService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ChatService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ChatMessage> SendMessageAsync(int ticketId, int senderId, int receiverId, string messageText)
        {
            var sender = await _unitOfWork.Users.GetByIdAsync(senderId);
            if (sender == null)
                throw new Exception("Sender not found");

            var messageId = await _unitOfWork.ChatMessages.GetNextMessageIdAsync();

            var message = new ChatMessage
            {
                MessageId = messageId,
                TicketId = ticketId,
                SenderId = senderId,
                SenderName = sender.FullName,
                ReceiverId = receiverId,
                MessageText = messageText,
                SentAt = DateTime.Now,
                IsRead = false,
                IsSimulated = true
            };

            await _unitOfWork.ChatMessages.AddAsync(message);
            return message;
        }

        public async Task<List<ChatMessage>> GetConversationAsync(int userId1, int userId2, int ticketId)
        {
            return await _unitOfWork.ChatMessages.GetConversationAsync(userId1, userId2, ticketId);
        }

        public async Task<List<ChatMessage>> GetTicketChatAsync(int ticketId)
        {
            return await _unitOfWork.ChatMessages.GetByTicketIdAsync(ticketId);
        }
    }
}
