using System;
using System.Collections.Generic;

namespace DemoArchitechture.Models;

public partial class Skill
{
    public int SkillId { get; set; }

    public string Name { get; set; } = null!;

    public string? Data { get; set; }

    public virtual ICollection<CharacterSession> CharacterSessions { get; set; } = new List<CharacterSession>();
}
