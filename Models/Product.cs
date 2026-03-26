using System;
using System.Collections.Generic;

namespace mytheme.Models;

public partial class Product
{
    public int Id { get; set; }

    public string? Namee { get; set; }

    public int? Price { get; set; }

    public string? Picture { get; set; }

    public string? Descp { get; set; }

    public int? CatId { get; set; }

    public int? Qty { get; set; }

    public virtual Categ? Cat { get; set; }

    public virtual ICollection<CusCard> CusCards { get; set; } = new List<CusCard>();
}
