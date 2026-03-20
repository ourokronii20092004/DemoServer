using DemoArchitechture.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class CharacterController : ControllerBase
{
    private readonly AppDbContext _context;

    public CharacterController(AppDbContext context) => _context = context;

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetCharacter(Guid userId)
    {
        var character = await _context.Characters
            .FirstOrDefaultAsync(c => c.UserId == userId);

        if (character == null) return NotFound("Không tìm thấy nhân vật");

        return Ok(character);
    }
}