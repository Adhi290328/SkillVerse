using SkillSwapBackend.Data;
using SkillSwapBackend.Models;

namespace SkillSwapBackend.Services
{
    public class SkillService
    {
        private readonly AppDbContext _context;

        public SkillService(AppDbContext context)
        {
            _context = context;
        }

        public List<Skill> GetAll()
        {
            return _context.Skills.ToList();
        }

        public Skill GetById(int id)
        {
            return _context.Skills.FirstOrDefault(s => s.Id == id);
        }

        public void AddSkill(Skill skill)
        {
            _context.Skills.Add(skill);
            _context.SaveChanges();
        }
    }
}
