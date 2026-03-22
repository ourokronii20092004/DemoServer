namespace DemoArchitechture.DTOs
{
    public class CreateCharacterRequestDTO
    {
        public Guid UserId { get; set; }
        public string CharacterName { get; set; } = string.Empty;
    }
}
