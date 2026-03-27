using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MoviesManager.Models;
using MoviesManager.Services;

namespace MoviesManager.Controllers
{
    public class AccountController : Controller
    {
        private readonly MongoDbService _mongoDbService;
        private readonly JwtService _jwtService;

        public AccountController(MongoDbService mongoDbService, JwtService jwtService)
        {
            _mongoDbService = mongoDbService;
            _jwtService = jwtService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);

            var existingUser = await _mongoDbService.Users
                .Find(u => u.Email.ToLower() == request.Email.ToLower())
                .FirstOrDefaultAsync();

            if (existingUser != null)
            {
                ViewBag.Message = "User already exists with this email.";
                return View(request);
            }

            var user = new AppUser
            {
                FullName = request.FullName,
                Email = request.Email.ToLower(),
                PasswordHash = PasswordHelper.HashPassword(request.Password)
            };

            await _mongoDbService.Users.InsertOneAsync(user);

            ViewBag.Message = "Registration successful.";
            ViewBag.Token = _jwtService.GenerateToken(user);

            ModelState.Clear();
            return View(new RegisterRequest());
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);

            var user = await _mongoDbService.Users
                .Find(u => u.Email.ToLower() == request.Email.ToLower())
                .FirstOrDefaultAsync();

            if (user == null || !PasswordHelper.VerifyPassword(request.Password, user.PasswordHash))
            {
                ViewBag.Message = "Invalid email or password.";
                return View(request);
            }

            ViewBag.Message = "Login successful.";
            ViewBag.Token = _jwtService.GenerateToken(user);

            return View(new LoginRequest());
        }

        [HttpGet]
        public IActionResult ApiTester()
        {
            return View();
        }
    }
}