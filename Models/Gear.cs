using System;
using System.Collections.Generic;

namespace DemoArchitechture.Models;

public partial class Gear
{
    public int ItemId { get; set; }

    public string? EquipSlotName { get; set; }

    public int? BonusAd { get; set; }

    public int? BonusAp { get; set; }

    public int? BonusDef { get; set; }

    public int? BonusRes { get; set; }

    public float? BonusMovementSpeed { get; set; }

    public float? BonusAttackSpeed { get; set; }

    public string? Effect { get; set; }

    public virtual Item Item { get; set; } = null!;
}
