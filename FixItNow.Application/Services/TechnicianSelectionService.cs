using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FixItNow.Domain.Entities;
using FixItNow.Domain.Interfaces;

namespace FixItNow.Application.Services
{
    /// <summary>
    /// Service for technician selection and rating system
    /// Allows residents to choose technicians based on ratings
    /// </summary>
    public interface ITechnicianSelectionService
    {
        Task<List<User>> GetAvailableTechniciansByCategoryAsync(int categoryId);
        Task RateTechnicianAsync(int ticketId, int technicianId, int rating, string review);
        Task<List<User>> GetAllTechniciansSortedByRatingAsync();
        Task<User> GetTechnicianStatsAsync(int technicianId);
    }

    public class TechnicianSelectionService : ITechnicianSelectionService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITicketRepository _ticketRepository;

        public TechnicianSelectionService(
            IUserRepository userRepository,
            ITicketRepository ticketRepository)
        {
            _userRepository = userRepository;
            _ticketRepository = ticketRepository;
        }

        /// <summary>
        /// Get technicians available for a specific category, sorted by rating
        /// </summary>
        public async Task<List<User>> GetAvailableTechniciansByCategoryAsync(int categoryId)
        {
            var allUsers = await _userRepository.GetAllAsync();

            var technicians = allUsers
                .Where(u => u.RoleId == 3 && u.IsActive)
                .OrderByDescending(t => t.AverageRating)
                .ThenByDescending(t => t.CompletedTickets)
                .ToList();

            return technicians;
        }

        /// <summary>
        /// Get all technicians sorted by rating and completed tickets
        /// </summary>
        public async Task<List<User>> GetAllTechniciansSortedByRatingAsync()
        {
            var allUsers = await _userRepository.GetAllAsync();

            var technicians = allUsers
                .Where(u => u.RoleId == 3 && u.IsActive)
                .OrderByDescending(t => t.AverageRating)
                .ThenByDescending(t => t.CompletedTickets)
                .ToList();

            return technicians;
        }

        /// <summary>
        /// Rate a technician after ticket completion
        /// Updates technician's average rating
        /// </summary>
        public async Task RateTechnicianAsync(int ticketId, int technicianId, int rating, string review)
        {
            if (rating < 1 || rating > 5)
                throw new ArgumentException("Rating must be between 1 and 5", nameof(rating));

            // Get ticket
            var ticket = await _ticketRepository.GetByIdAsync(ticketId);
            if (ticket == null)
                throw new Exception("Ticket not found");

            if (ticket.StatusId != 4) // Not resolved
                throw new Exception("Can only rate resolved tickets");

            if (ticket.TechnicianRatingGiven.HasValue)
                throw new Exception("Ticket already rated");

            // Update ticket
            ticket.TechnicianRatingGiven = rating;
            ticket.UserReview = review;
            await _ticketRepository.UpdateAsync(ticket);

            // Update technician rating
            var technician = await _userRepository.GetByIdAsync(technicianId);
            if (technician == null)
                throw new Exception("Technician not found");

            // Calculate new average rating
            double totalRatingPoints = (technician.AverageRating * technician.TotalRatings) + rating;
            technician.TotalRatings++;
            technician.AverageRating = totalRatingPoints / technician.TotalRatings;
            technician.CompletedTickets++;

            await _userRepository.UpdateAsync(technician);

            LogRating(technician, rating);
        }

        /// <summary>
        /// Get technician statistics
        /// </summary>
        public async Task<User> GetTechnicianStatsAsync(int technicianId)
        {
            var technician = await _userRepository.GetByIdAsync(technicianId);
            if (technician == null)
                throw new Exception("Technician not found");

            return technician;
        }

        private void LogRating(User technician, int rating)
        {
            string stars = new string('?', rating);
            string emptyStars = new string('?', 5 - rating);

            Console.WriteLine($"\n? Rating submitted for {technician.FullName}");
            Console.WriteLine($"   Rating: {stars}{emptyStars} {rating}/5");
            Console.WriteLine($"   New average: {technician.AverageRating:F2}/5 ({technician.TotalRatings} total ratings)");
            Console.WriteLine($"   Completed tickets: {technician.CompletedTickets}");
        }
    }
}
