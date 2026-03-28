using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SkillSwapBackend.Models;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using SkillSwapBackend.Services;
using SkillSwapBackend.DTO;
using SkillSwapBackend.Data;

namespace SkillSwapBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly AppDbContext _context;

        public AuthController(IConfiguration config, AppDbContext context)
        {
            _config = config;
            _context = context;
        }

        [HttpPost("register")]
        public IActionResult Register(User user)
        {
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

            if (_context.users.Any(u => u.Email == user.Email))
            {
                return BadRequest("User already exists");
            }

            _context.users.Add(user);
            _context.SaveChanges();

            return Ok("User Registered Successfully");
        }

        [HttpPost("login")]
        public IActionResult Login(LoginDto loginuser)
        {
            var user = _context.users.FirstOrDefault(u => u.Email ==  loginuser.Email);

            if(user == null || !BCrypt.Net.BCrypt.Verify(loginuser.Password, user.Password))
            {
                Console.WriteLine($"Stored: {user?.Password}");
                Console.WriteLine($"Input: {loginuser.Password}");
                return Unauthorized("Invalid Credentials");
            }

            var token = GenerateJwtToken(user);

            

            return Ok(new {
                token = token,
                email = user.Email,
                role = user.Role
            });

        }

        private string GenerateJwtToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
             );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
