using SkillSwapBackend.Models;
using SkillSwapBackend.Data;

namespace SkillSwapBackend.Services
{
    public class UserService : IUserServices
    {
        private readonly AppDbContext _context;
        
        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public void AddUser(User user)
        {
            _context.users.Add(user);
            _context.SaveChanges();
        }

        public User GetUserByEmail(string email)
        {
            return _context.users.FirstOrDefault(u => u.Email == email);
        }
    }
}
