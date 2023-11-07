using System;
using System.Collections.Generic;

namespace MyCashier.MVVM.Models;

public partial class Category
{
    public Guid id { get; set; }

    public string name { get; set; } = null!;

    public byte[]? icon { get; set; }

    public virtual ICollection<Obligation> obligations { get; set; } = new List<Obligation>();

    public virtual ICollection<Transaction> transactions { get; set; } = new List<Transaction>();
}
