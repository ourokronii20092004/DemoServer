using System;
using System.Collections.Generic;

namespace DemoArchitechture.Models;

public partial class Enemy
{
    public string EnemyId { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Tier { get; set; }

    public string? SpawnBiomeName { get; set; }

    public int? Hp { get; set; }

    public int? Ad { get; set; }

    public int? Ap { get; set; }

    public int? Def { get; set; }

    public int? Res { get; set; }

    public float? MovementSpeed { get; set; }

    public float? AttackSpeed { get; set; }

    public bool? IsRanged { get; set; }

    public int? ExpReward { get; set; }

    public virtual ICollection<EnemyItem> EnemyItems { get; set; } = new List<EnemyItem>();
}
