using System;
using System.Collections.Generic;

namespace mytheme.Models;

public partial class Studatum
{
    public int Id { get; set; }

    public string? Stuname { get; set; }

    public long? Fee { get; set; }

    public string? Addresss { get; set; }

    public virtual ICollection<Stumark> Stumarks { get; set; } = new List<Stumark>();
}
