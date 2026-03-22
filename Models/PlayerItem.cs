using System;
using System.Collections.Generic;

namespace DemoArchitechture.Models;

public partial class PlayerItem
{
    public Guid CharacterId { get; set; }

    public Guid SessionId { get; set; }

    public int ItemId { get; set; }

    public short? Quantity { get; set; }

    public virtual CharacterSession CharacterSession { get; set; } = null!;

    public virtual Item Item { get; set; } = null!;
}
