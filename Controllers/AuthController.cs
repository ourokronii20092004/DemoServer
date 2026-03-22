using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DemoArchitechture.Models;
using DemoArchitechture.DTOs;

namespace DemoArchitechture.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO request)
        {
            // Tìm user bằng email
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == request.Email);

            // Kiểm tra tồn tại và so sánh mật đã hash
            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                return Unauthorized("Incorrect account or password.");
            }

            // Kiểm tra trạng thái ban tài khoản
            if (user.IsBanned == true)
            {
                return Forbid("Your account has been banned.");
            }

            // Update thời gian cuối cùng login
            user.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            return Ok(new
            {
                userId = user.UserId,
                fullName = user.Fullname,
                role = user.Role
            });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDTO request)
        {
            // Kiểm tra trùng email
            if (await _context.Users.AnyAsync(u => u.Email == request.Email))
            {
                return BadRequest("This email address has already been used.");
            }

            // Hash password trước khi lưu
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var newUser = new User
            {
                // UserId sẽ tự động generate UUID nhờ DB default gen_random_uuid()
                Fullname = request.FullName,
                Email = request.Email,
                PasswordHash = hashedPassword,
                Role = "player",
                IsBanned = false,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Registration successful!", userId = newUser.UserId });
        }
    }
}