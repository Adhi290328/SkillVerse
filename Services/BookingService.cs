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
            return _context.Bookings.Where(b => b.LearnerEmail == email || b.MentorEmail == email).ToList();
        }

        public Booking GetById(int id)
        {
            return _context.Bookings.FirstOrDefault(b => b.Id == id);
        }

        public void UpdateBooking(Booking booking)
        {
            _context.Bookings.Update(booking);
            _context.SaveChanges();
        }
    }
}
