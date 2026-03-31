using System.ComponentModel.DataAnnotations;

namespace SkillSwapBackend.Models
{
    public class User
    {
        public int id {  get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public string Role { get; set; }
        public string Name { get; set; }

    }
}
