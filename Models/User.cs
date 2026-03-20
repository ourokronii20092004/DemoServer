using System;
using System.Collections.Generic;

namespace DemoArchitechture.Models;

public partial class User
{
    public Guid UserId { get; set; }

    public string UserFullname { get; set; } = null!;

    public string UserEmail { get; set; } = null!;

    public string UserPasswordHash { get; set; } = null!;

    public DateTime? UserCreatedAt { get; set; }

    public DateTime? UserUpdatedAt { get; set; }

    public bool? IsBanned { get; set; }

    public virtual ICollection<Character> Characters { get; set; } = new List<Character>();
}
