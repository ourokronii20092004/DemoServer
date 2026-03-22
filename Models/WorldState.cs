using System;
using System.Collections.Generic;

namespace DemoArchitechture.Models;

public partial class WorldState
{
    public Guid StateId { get; set; }

    public Guid? SessionId { get; set; }

    public string EventId { get; set; } = null!;

    public int? StateValue { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Session? Session { get; set; }
}
