using System;
using System.Collections.Generic;

namespace StoresApplication.Models;

public partial class Store
{
    public string Name { get; set; } = null!;

    public string CityArea { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string Telephone { get; set; } = null!;

    public decimal? Capital { get; set; }

    public string Profile { get; set; } = null!;

    public int Id { get; set; }

    public virtual ICollection<Delivery> Deliveries { get; } = new List<Delivery>();

    public virtual ICollection<Ownership> Ownerships { get; } = new List<Ownership>();
}
