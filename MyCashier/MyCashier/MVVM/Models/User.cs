using System;
using System.Collections.Generic;

namespace MyCashier.MVVM.Models;

public partial class User
{
    public Guid id { get; set; }

    public string login { get; set; } = null!;

    public string password { get; set; } = null!;

    public string name { get; set; } = null!;

    public string email { get; set; } = null!;

    public virtual ICollection<Account> accounts { get; set; } = new List<Account>();
}
