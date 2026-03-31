using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SkillSwapBackend.Models;
using SkillSwapBackend.Services;

namespace SkillSwapBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillController : ControllerBase
    {
        private readonly SkillService _skillService;
        private readonly UserService _userService;

        public SkillController(SkillService skillService, UserService userService)
        {
            _skillService = skillService;
            _userService = userService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_skillService.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var skill = _skillService.GetById(id);
            if (skill == null)
            {
                return NotFound();
            }
            return Ok(skill);
        }

        [HttpPost]
        [Authorize(Roles = "mentor")]
        public IActionResult AddSkill(Skill skill)
        {
            var email = User.FindFirst(ClaimTypes.Name)?.Value;
            var user = _userService.GetUserByEmail(email);

            skill.MentorEmail = email;
            skill.MentorName = user.Name;

            _skillService.AddSkill(skill);
            return Ok("Skill Added");
        }

        [HttpGet("my")]
        [Authorize(Roles = "mentor")]
        public IActionResult GetMySkills()
        {
            var email = User.FindFirst(ClaimTypes.Name)?.Value;
            var skills = _skillService.GetAll().Where(s => s.MentorEmail == email).ToList();
            return Ok(skills);
        }
    }
}
