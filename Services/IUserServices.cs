using SkillSwapBackend.Models;

namespace SkillSwapBackend.Services
{
    public interface IUserServices
    {
        User GetUserByEmail(string email);
        void AddUser(User user);
    }
}
