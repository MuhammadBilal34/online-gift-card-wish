using System;
using System.Collections.Generic;

namespace mytheme.Models;

public partial class Categ
{
    public int Id { get; set; }

    public string? Namee { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
