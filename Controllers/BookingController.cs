using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkillSwapBackend.Data;
using SkillSwapBackend.Models;
using SkillSwapBackend.Services;

[Route("api/[controller]")]
[ApiController]
public class BookingController : ControllerBase
{
    private readonly BookingService _bookingService;
    private readonly AppDbContext _context;

    public BookingController(BookingService bookingService, AppDbContext context)
    {
        _bookingService = bookingService;
        _context = context;
    }

    [HttpPost]
    [Authorize(Roles = "learner")]
    public IActionResult BookSkill(Booking booking)
    {
        var learnerEmail = User.FindFirst(ClaimTypes.Name)?.Value;

        var skill = _context.Skills.FirstOrDefault(s => s.Id == booking.Id);

        if (skill == null)
            return NotFound("Skill not found");

        booking.SkillTitle = skill.Title;
        booking.MentorEmail = skill.MentorEmail;
        booking.LearnerEmail = learnerEmail;
        booking.Status = "pending";

        _context.Bookings.Add(booking);
        _context.SaveChanges();

        return Ok("Booking Successful");
    }

    [HttpGet("my")]
    [Authorize]
    public IActionResult MyBookings()
    {
        var email = User.FindFirst(ClaimTypes.Name)?.Value;
        var bookings = _bookingService.GetBookingsForUser(email);
        return Ok(bookings);
    }

    [HttpPatch("{id}")]
    [Authorize(Roles = "mentor")]
    public IActionResult UpdateStatus(int id, [FromBody] string status)
    {
        var booking = _bookingService.GetById(id);

        if (booking == null)
            return NotFound();

        booking.Status = status;
        _bookingService.UpdateBooking(booking);

        return Ok("Booking Updated");
    }
}