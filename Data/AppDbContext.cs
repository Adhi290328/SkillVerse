using Microsoft.EntityFrameworkCore;
using SkillSwapBackend.Models;

namespace SkillSwapBackend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<User> users { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Booking> Bookings { get; set; }
    }
}
