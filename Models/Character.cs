using System;
using System.Collections.Generic;

namespace DemoArchitechture.Models;

public partial class Character
{
    public Guid CharacterId { get; set; }

    public Guid? UserId { get; set; }

    public string Username { get; set; } = null!;

    public int? TotalPlaytime { get; set; }

    public DateTime? LastLogin { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<CharacterSession> CharacterSessions { get; set; } = new List<CharacterSession>();

    public virtual User? User { get; set; }
}
