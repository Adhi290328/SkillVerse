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
            return NotFound(new { message = "Skill not found" });

        booking.SkillTitle = skill.Title;
        booking.MentorEmail = skill.MentorEmail;
        booking.LearnerEmail = learnerEmail;
        booking.Status = BookingStatus.Pending;

        _context.Bookings.Add(booking);
        _context.SaveChanges();

        return Ok(new
        {
            message = "Booking Successful",
            Id = booking.Id
        });
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
    public IActionResult UpdateStatus(int id, [FromBody] BookingStatus status)
    {
        var booking = _bookingService.GetById(id);

        if (booking == null)
            return NotFound();

        booking.Status = status;
        _bookingService.UpdateBooking(booking);

        return Ok(new { message = "Booking Updated", status = booking.Status });
    }

    [HttpGet("mentor")]
    [Authorize(Roles = "mentor")]
    public IActionResult MentorBookings()
    {
        var email = User.FindFirst(ClaimTypes.Name)?.Value;
        var bookings = _bookingService.GetBookingsForMentor(email);
        return Ok(bookings);
    }
}