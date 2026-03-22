using System;
using System.Collections.Generic;

namespace DemoArchitechture.Models;

public partial class Session
{
    public Guid SessionId { get; set; }

    public int? PlayTime { get; set; }

    public DateTime? LastLoad { get; set; }

    public DateTime? LastSave { get; set; }

    public bool? IsMultiplayer { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<CharacterSession> CharacterSessions { get; set; } = new List<CharacterSession>();

    public virtual ICollection<WorldState> WorldStates { get; set; } = new List<WorldState>();
}
