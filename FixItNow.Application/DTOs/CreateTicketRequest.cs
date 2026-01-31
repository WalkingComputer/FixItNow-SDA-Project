namespace FixItNow.Application.DTOs
{
    public class CreateTicketRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public int LocationId { get; set; }
        public int PriorityId { get; set; }
        public int CreatedByUserId { get; set; }
    }
}