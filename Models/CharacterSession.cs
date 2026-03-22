using System;
using System.Collections.Generic;

namespace DemoArchitechture.Models;

public partial class CharacterSession
{
    public Guid CharacterId { get; set; }

    public Guid SessionId { get; set; }

    public int? PlayerRole { get; set; }

    public int? CurrentLevel { get; set; }

    public int? CurrentExp { get; set; }

    public int? MaxHp { get; set; }

    public int? CurrentHp { get; set; }

    public int? MaxMana { get; set; }

    public int? CurrentMana { get; set; }

    public int? MaxStamina { get; set; }

    public float? AttackSpeed { get; set; }

    public float? MovementSpeed { get; set; }

    public string? WorldLocation { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Character Character { get; set; } = null!;

    public virtual ICollection<PlayerItem> PlayerItems { get; set; } = new List<PlayerItem>();

    public virtual Session Session { get; set; } = null!;

    public virtual ICollection<Skill> Skills { get; set; } = new List<Skill>();
}
