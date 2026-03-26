using System;
using System.Collections.Generic;

namespace mytheme.Models;

public partial class CusCard
{
    public int Id { get; set; }

    public int? TemplateId { get; set; }

    public string? Message { get; set; }

    public string? FinalImagePath { get; set; }

    public string? CreatedAt { get; set; }

    public int? Userid { get; set; }

    public virtual Product? Template { get; set; }

    public virtual Login? User { get; set; }
}
