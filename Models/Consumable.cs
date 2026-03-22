using System;
using System.Collections.Generic;

namespace DemoArchitechture.Models;

public partial class Consumable
{
    public int ItemId { get; set; }

    public int? HpRestore { get; set; }

    public int? ManaRestore { get; set; }

    public virtual Item Item { get; set; } = null!;
}
