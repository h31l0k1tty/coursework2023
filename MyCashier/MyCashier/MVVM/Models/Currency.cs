using System;
using System.Collections.Generic;

namespace MyCashier.MVVM.Models;

public partial class Currency
{
    public string id { get; set; } = null!;

    public string name { get; set; } = null!;

    public virtual ICollection<Account> accounts { get; set; } = new List<Account>();

    public virtual ICollection<Obligation> obligations { get; set; } = new List<Obligation>();

    public virtual ICollection<Transaction> transactions { get; set; } = new List<Transaction>();
}
