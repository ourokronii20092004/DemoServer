using System;
using System.Collections.Generic;

namespace DemoArchitechture.Models;

public partial class Level
{
    public int Level1 { get; set; }

    public int? ExpNext { get; set; }

    public int? Ad { get; set; }

    public int? Ap { get; set; }

    public int? Def { get; set; }

    public int? Res { get; set; }

    public float? MovementSpeed { get; set; }

    public float? AttackSpeed { get; set; }
}
