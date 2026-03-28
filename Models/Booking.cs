namespace SkillSwapBackend.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public string? LearnerEmail { get; set; }
        public string? MentorEmail { get; set; }
        public string? SkillTitle { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; } = "pending";

    }
}
