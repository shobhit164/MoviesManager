using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MoviesManager.Models;
using MoviesManager.Services;

namespace MoviesManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly MongoDbService _mongoDbService;
        private readonly JwtService _jwtService;

        public AuthController(MongoDbService mongoDbService, JwtService jwtService)
        {
            _mongoDbService = mongoDbService;
            _jwtService = jwtService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingUser = await _mongoDbService.Users
                .Find(u => u.Email.ToLower() == request.Email.ToLower())
                .FirstOrDefaultAsync();

            if (existingUser != null)
                return BadRequest(new { message = "User with this email already exists." });

            var user = new AppUser
            {
                FullName = request.FullName,
                Email = request.Email.ToLower(),
                PasswordHash = PasswordHelper.HashPassword(request.Password)
            };

            await _mongoDbService.Users.InsertOneAsync(user);

            var token = _jwtService.GenerateToken(user);

            return Ok(new
            {
                message = "Registration successful.",
                token
            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _mongoDbService.Users
                .Find(u => u.Email.ToLower() == request.Email.ToLower())
                .FirstOrDefaultAsync();

            if (user == null || !PasswordHelper.VerifyPassword(request.Password, user.PasswordHash))
                return Unauthorized(new { message = "Invalid email or password." });

            var token = _jwtService.GenerateToken(user);

            return Ok(new
            {
                message = "Login successful.",
                token
            });
        }
    }
}