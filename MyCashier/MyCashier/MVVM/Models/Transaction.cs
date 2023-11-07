using System;
using System.Collections.Generic;

namespace MyCashier.MVVM.Models;

public partial class Transaction
{
    public Guid id { get; set; }

    public Guid typeId { get; set; }

    public Guid accountId { get; set; }

    public string? currency { get; set; }

    public decimal sum { get; set; }

    public DateOnly date { get; set; }

    public Guid categoryId { get; set; }

    public string? description { get; set; }

    public virtual Account account { get; set; } = null!;

    public virtual Category category { get; set; } = null!;

    public virtual Currency? currencyNavigation { get; set; }

    public virtual TransactionType type { get; set; } = null!;
}
