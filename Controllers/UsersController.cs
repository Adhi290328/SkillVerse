using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SkillSwapBackend.Models;
using SkillSwapBackend.Services;
using System.Security.Claims;
using SkillSwapBackend.DTO;

namespace SkillSwapBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private static List<User> users = new List<User>();
        private readonly UserService _userService;

        public UsersController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet("me")]
        [Authorize]
        public IActionResult GetMe()
        {
            var email = User.FindFirst(System.Security.Claims.ClaimTypes.Name)?.Value;

            var user = _userService.GetUserByEmail(email);

            var userDto = new UserDto
            {
                Email = user.Email,
                Role = user.Role
            };
            return Ok(user);
        }

        [HttpGet("mentor")]
        public IActionResult MentorOnly()
        {
            return Ok("mentor");
        }
    }
}
