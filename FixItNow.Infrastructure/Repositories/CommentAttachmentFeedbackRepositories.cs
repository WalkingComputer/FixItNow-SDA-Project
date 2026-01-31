using FixItNow.Domain.Entities;
using FixItNow.Domain.Interfaces;
using FixItNow.Infrastructure.Data;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FixItNow.Infrastructure.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly IMongoCollection<Comment> _comments;

        public CommentRepository(MongoDbContext context)
        {
            _comments = context.Comments;
        }

        public async Task<Comment> GetByIdAsync(int commentId)
        {
            var filter = Builders<Comment>.Filter.Eq(c => c.CommentId, commentId);
            return await _comments.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<List<Comment>> GetByTicketIdAsync(int ticketId)
        {
            var filter = Builders<Comment>.Filter.Eq(c => c.TicketId, ticketId);
            var sort = Builders<Comment>.Sort.Ascending(c => c.CreatedAt);
            return await _comments.Find(filter).Sort(sort).ToListAsync();
        }

        public async Task<List<Comment>> GetAllAsync()
        {
            return await _comments.Find(_ => true).ToListAsync();
        }

        public async Task AddAsync(Comment comment)
        {
            await _comments.InsertOneAsync(comment);
        }

        public async Task UpdateAsync(Comment comment)
        {
            var filter = Builders<Comment>.Filter.Eq(c => c.CommentId, comment.CommentId);
            await _comments.ReplaceOneAsync(filter, comment);
        }

        public async Task DeleteAsync(int commentId)
        {
            var filter = Builders<Comment>.Filter.Eq(c => c.CommentId, commentId);
            await _comments.DeleteOneAsync(filter);
        }

        public async Task<int> GetNextCommentIdAsync()
        {
            var all = await GetAllAsync();
            return all.Any() ? all.Max(c => c.CommentId) + 1 : 1;
        }
    }

    public class AttachmentRepository : IAttachmentRepository
    {
        private readonly IMongoCollection<Attachment> _attachments;

        public AttachmentRepository(MongoDbContext context)
        {
            _attachments = context.Attachments;
        }

        public async Task<Attachment> GetByIdAsync(int attachmentId)
        {
            var filter = Builders<Attachment>.Filter.Eq(a => a.AttachmentId, attachmentId);
            return await _attachments.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<List<Attachment>> GetByTicketIdAsync(int ticketId)
        {
            var filter = Builders<Attachment>.Filter.Eq(a => a.TicketId, ticketId);
            return await _attachments.Find(filter).ToListAsync();
        }

        public async Task<List<Attachment>> GetAllAsync()
        {
            return await _attachments.Find(_ => true).ToListAsync();
        }

        public async Task AddAsync(Attachment attachment)
        {
            await _attachments.InsertOneAsync(attachment);
        }

        public async Task DeleteAsync(int attachmentId)
        {
            var filter = Builders<Attachment>.Filter.Eq(a => a.AttachmentId, attachmentId);
            await _attachments.DeleteOneAsync(filter);
        }

        public async Task<int> GetNextAttachmentIdAsync()
        {
            var all = await GetAllAsync();
            return all.Any() ? all.Max(a => a.AttachmentId) + 1 : 1;
        }
    }

    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly IMongoCollection<Feedback> _feedbacks;

        public FeedbackRepository(MongoDbContext context)
        {
            _feedbacks = context.Feedbacks;
        }

        public async Task<Feedback> GetByIdAsync(int feedbackId)
        {
            var filter = Builders<Feedback>.Filter.Eq(f => f.FeedbackId, feedbackId);
            return await _feedbacks.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<List<Feedback>> GetByUserIdAsync(int userId)
        {
            var filter = Builders<Feedback>.Filter.Eq(f => f.ResidentUserId, userId);
            return await _feedbacks.Find(filter).ToListAsync();
        }

        public async Task<List<Feedback>> GetAllAsync()
        {
            return await _feedbacks.Find(_ => true).ToListAsync();
        }

        public async Task AddAsync(Feedback feedback)
        {
            await _feedbacks.InsertOneAsync(feedback);
        }

        public async Task UpdateAsync(Feedback feedback)
        {
            var filter = Builders<Feedback>.Filter.Eq(f => f.FeedbackId, feedback.FeedbackId);
            await _feedbacks.ReplaceOneAsync(filter, feedback);
        }

        public async Task<int> GetNextFeedbackIdAsync()
        {
            var all = await GetAllAsync();
            return all.Any() ? all.Max(f => f.FeedbackId) + 1 : 1;
        }
    }
}
