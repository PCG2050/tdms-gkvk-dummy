namespace Application.Models
{
    public class SuccessStoryDto
    {
        public int Id { get; set; }
        public string ProgramType { get; set; } = string.Empty; // "STU", "KVK", "NAEP", etc.
        public string Title { get; set; } = string.Empty; // Program title
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }

        // User information
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty; // FirstName + LastName
        public string? ProfileImageUrl { get; set; }
        public string Department { get; set; } = string.Empty; // UnitLocation.Unit.Name
        public string Position { get; set; } = string.Empty; // Role (UnitHead/Trainer)

        // Success Story Content
        public string SuccessStoryContent { get; set; } = string.Empty; // From Recommendation.SuccessStories

        public DateTimeOffset CreatedAt { get; set; }
    }
}
