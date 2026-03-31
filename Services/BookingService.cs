using Microsoft.EntityFrameworkCore;
using SkillSwapBackend.Data;
using SkillSwapBackend.Models;

namespace SkillSwapBackend.Services
{
    public class BookingService
    {
        private readonly AppDbContext _context;

        public BookingService(AppDbContext context)
        {
            _context = context;
        }

        public void CreateBooking(Booking booking)
        {
            _context.Bookings.Add(booking);
            _context.SaveChanges();
        }

        public List<Booking> GetBookingsForUser(string email)
        {
            return _context.Bookings.Include(b => b.Skill).Where(b => b.LearnerEmail == email || b.MentorEmail == email).Select(b => new Booking
            {
                Id = b.Id,
                LearnerEmail = b.LearnerEmail,
                MentorEmail = b.MentorEmail,
                SkillTitle = b.SkillTitle,
                Date = b.Date,
                Status = b.Status,
                Skill = new Skill
                {
                    Id = b.Skill.Id,
                    Title = b.Skill.Title,
                    Description = b.Skill.Description
                }
            }).ToList();
        }

        public Booking GetById(int id)
        {
            return _context.Bookings.Include(b => b.Skill).Where(b => b.Id == id).FirstOrDefault();
        }

        public void UpdateBooking(Booking booking)
        {
            _context.Bookings.Update(booking);
            _context.SaveChanges();
        }

        public List<Booking> GetBookingsForMentor(string email)
        {
            return _context.Bookings.Where(b => b.MentorEmail == email).ToList();
        }
    }
}
