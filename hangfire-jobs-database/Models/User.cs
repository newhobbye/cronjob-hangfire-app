using System;
using System.Collections.Generic;

namespace hangfire_jobs_database.Models;

public partial class User
{
    public string Id { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Phone { get; set; }

    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();
}
