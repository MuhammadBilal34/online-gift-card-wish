using System;
using System.Collections.Generic;

namespace mytheme.Models;

public partial class Stumark
{
    public int Id { get; set; }

    public int? Marks { get; set; }

    public int? Stuid { get; set; }

    public virtual Studatum? Stu { get; set; }
}
