using DemoArchitechture.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DemoArchitechture.DTOs;

namespace DemoArchitechture.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CharacterController(AppDbContext context) => _context = context;

        // Lấy danh sách nhân vật của 1 User cụ thể
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetCharactersByUser(Guid userId)
        {
            var characters = await _context.Characters
                .Where(c => c.UserId == userId)
                .Select(c => new {
                    c.CharacterId,
                    c.Username,
                    c.TotalPlaytime,
                    c.LastLogin,
                    c.CreatedAt
                })
                .ToListAsync();

            if (!characters.Any())
                return NotFound("This player doesn't have a character yet.");

            return Ok(characters);
        }

        // Lấy thông tin chi tiết của 1 nhân vật (để load game)
        [HttpGet("{characterId}")]
        public async Task<IActionResult> GetCharacterDetails(Guid characterId)
        {
            var character = await _context.Characters
                .FirstOrDefaultAsync(c => c.CharacterId == characterId);

            if (character == null)
                return NotFound("No character found.");

            return Ok(character);
        }

        // Tạo nhân vật mới
        [HttpPost("create")]
        public async Task<IActionResult> CreateCharacter([FromBody] CreateCharacterRequestDTO request)
        {
            // Check trùng tên nhân vật (Username trong bảng characters là Unique)
            if (await _context.Characters.AnyAsync(c => c.Username == request.CharacterName))
            {
                return BadRequest("The character's name already exists. Please choose a different name.");
            }

            var newCharacter = new Character
            {
                UserId = request.UserId,
                Username = request.CharacterName,
                TotalPlaytime = 0,
                CreatedAt = DateTime.UtcNow
            };

            _context.Characters.Add(newCharacter);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Character creation successful!",
                characterId = newCharacter.CharacterId
            });
        }
    }
}