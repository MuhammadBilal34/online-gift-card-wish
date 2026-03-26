using System;
using System.Collections.Generic;

namespace mytheme.Models;

public partial class Login
{
    public int Id { get; set; }

    public string? Username { get; set; }

    public string? Email { get; set; }

    public string? Passwordd { get; set; }

    public int? RoleId { get; set; }

    public virtual ICollection<CusCard> CusCards { get; set; } = new List<CusCard>();

    public virtual Role? Role { get; set; }

    public virtual ICollection<UserDetail> UserDetails { get; set; } = new List<UserDetail>();
}
