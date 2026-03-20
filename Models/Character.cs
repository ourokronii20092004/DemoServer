using System;
using System.Collections.Generic;

namespace DemoArchitechture.Models;

public partial class Character
{
    public Guid CharacterId { get; set; }

    public Guid UserId { get; set; }

    public string CharacterName { get; set; } = null!;

    public double CharacterHealth { get; set; }

    public double CharacterAttack { get; set; }

    public virtual User User { get; set; } = null!;
}
