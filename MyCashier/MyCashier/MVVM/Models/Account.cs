using System;
using System.Collections.Generic;

namespace MyCashier.MVVM.Models;

public partial class Account
{
    public Guid id { get; set; }

    public string name { get; set; } = null!;

    public decimal balance { get; set; }

    public string currencyId { get; set; } = null!;

    public Guid userId { get; set; }

    public virtual Currency currency { get; set; } = null!;

    public virtual ICollection<Obligation> obligations { get; set; } = new List<Obligation>();

    public virtual ICollection<Transaction> transactions { get; set; } = new List<Transaction>();

    public virtual User user { get; set; } = null!;
}
