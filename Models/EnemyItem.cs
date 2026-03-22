using System;
using System.Collections.Generic;

namespace DemoArchitechture.Models;

public partial class EnemyItem
{
    public string EnemyId { get; set; } = null!;

    public int ItemId { get; set; }

    public float DropChance { get; set; }

    public virtual Enemy Enemy { get; set; } = null!;

    public virtual Item Item { get; set; } = null!;
}
