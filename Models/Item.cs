using System;
using System.Collections.Generic;

namespace DemoArchitechture.Models;

public partial class Item
{
    public int ItemId { get; set; }

    public string Name { get; set; } = null!;

    public string? Type { get; set; }

    public short? StackLimit { get; set; }

    public virtual Consumable? Consumable { get; set; }

    public virtual ICollection<EnemyItem> EnemyItems { get; set; } = new List<EnemyItem>();

    public virtual Gear? Gear { get; set; }

    public virtual ICollection<PlayerItem> PlayerItems { get; set; } = new List<PlayerItem>();
}
