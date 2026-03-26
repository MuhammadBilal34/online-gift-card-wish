using System;
using System.Collections.Generic;

namespace mytheme.Models;

public partial class UserDetail
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Address { get; set; }

    public int? UserId { get; set; }

    public string? Departr { get; set; }

    public int? RoleId { get; set; }

    public virtual Role? Role { get; set; }

    public virtual Login? User { get; set; }
}
