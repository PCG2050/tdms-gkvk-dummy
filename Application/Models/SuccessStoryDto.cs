namespace Application.Models
{
    public class SuccessStoryDto
    {
        public int Id { get; set; }
        public string ProgramType { get; set; } = string.Empty; 
        public string Title { get; set; } = string.Empty; 
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }

        // User information
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty; 
        public string? ProfileImageUrl { get; set; }
        public string Department { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty; 

        // Success Story Content
        public string SuccessStoryContent { get; set; } = string.Empty;

        public DateTimeOffset CreatedAt { get; set; }
    }
}