namespace SkillSwapBackend.Models
{
    public class Skill
    {
        public int Id { get; set; }
        public string MentorName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string MentorEmail { get; set; }

        public double price { get; set; }
    }
}
