using Microsoft.AspNetCore.Mvc;
using EgyEagles.BLL.Interfaces;
using EgyEagles.Shared.DTOs.Users;
using Microsoft.AspNetCore.Authorization;
using EgyEagles.BLL.Helpers;

namespace EgyEagles.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly JwtTokenGenerator _jwtTokenGenerator;

        public UserController(IUserService userService, JwtTokenGenerator jwtTokenGenerator)
        {
            _userService = userService;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        [HttpPost("create")]
        //[Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Create([FromBody] CreateUserDto dto)
        {
            var id = await _userService.CreateUserAsync(dto);
            return Ok(new { UserId = id });
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] UserLoginDto dto)
        {
            var user = await _userService.LoginAsync(dto); 

            var token = _jwtTokenGenerator.GenerateToken(user);

            return Ok(new
            {
                token = token,
                role = user.Role.ToString(),
                userId = user.Id,
                companyId = user.CompanyId
            });
        }

        [HttpGet("by-company/{companyId}")]
        [Authorize(Roles = "CompanyAdmin,SuperAdmin")]
        
        public async Task<IActionResult> GetByCompany(string companyId)
        {
            var users = await _userService.GetUsersByCompanyAsync(companyId);
            return Ok(users);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "SuperAdmin,CompanyAdmin")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
                return NotFound();

            return Ok(user);
        }
    }
}