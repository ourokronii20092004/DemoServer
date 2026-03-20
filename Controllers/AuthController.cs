using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DemoArchitechture.Data;
using DemoArchitechture.Models;

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
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.UserEmail == request.Email && u.UserPasswordHash == request.Password);

        if (user == null) return Unauthorized("Sai tài khoản hoặc mật khẩu");

        
        return Ok(new { userId = user.UserId, fullName = user.UserFullname });
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        
        if (await _context.Users.AnyAsync(u => u.UserEmail == request.Email))
        {
            return BadRequest("Email này đã được sử dụng.");
        }

        
        var newUser = new User
        {
            UserId = Guid.NewGuid(),
            UserFullname = request.FullName,
            UserEmail = request.Email,
            UserPasswordHash = request.Password, 
            IsBanned = false,
            UserCreatedAt = DateTime.UtcNow,
            UserUpdatedAt = DateTime.UtcNow
        };

        
        _context.Users.Add(newUser);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Đăng ký thành công!", userId = newUser.UserId });
    }

    
    public class RegisterRequest
    {
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}


public class LoginRequest
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}