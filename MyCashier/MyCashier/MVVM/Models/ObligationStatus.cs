using System;
using System.Collections.Generic;

namespace MyCashier.MVVM.Models;

public partial class ObligationStatus
{
    public Guid id { get; set; }

    public string name { get; set; } = null!;

    public virtual ICollection<Obligation> obligations { get; set; } = new List<Obligation>();
}
