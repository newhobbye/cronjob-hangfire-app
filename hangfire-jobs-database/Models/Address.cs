using System;
using System.Collections.Generic;

namespace hangfire_jobs_database.Models;

public partial class Address
{
    public string Id { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public string Cep { get; set; } = null!;

    public string Street { get; set; } = null!;

    public string Number { get; set; } = null!;

    public string State { get; set; } = null!;

    public string Neightborhood { get; set; } = null!;

    public string Country { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
