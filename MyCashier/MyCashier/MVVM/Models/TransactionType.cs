using System;
using System.Collections.Generic;

namespace MyCashier.MVVM.Models;

public partial class TransactionType
{
    public Guid id { get; set; }

    public string name { get; set; } = null!;

    public virtual ICollection<Transaction> transactions { get; set; } = new List<Transaction>();
}
