using System;
using System.Collections.Generic;

namespace DemoArchitechture.Models;

public partial class User
{
    public Guid UserId { get; set; }

    public string Fullname { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Role { get; set; }

    public bool? IsBanned { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<Character> Characters { get; set; } = new List<Character>();
}
