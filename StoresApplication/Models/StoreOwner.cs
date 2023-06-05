using System;
using System.Collections.Generic;

namespace StoresApplication.Models;

public partial class StoreOwner
{
    public string Name { get; set; } = null!;

    public DateTime BirthDate { get; set; }
    public string CityArea { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string Telephone { get; set; } = null!;

    public int Id { get; set; }

    public virtual ICollection<Ownership> Ownerships { get; } = new List<Ownership>();
}
