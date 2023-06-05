using System;
using System.Collections.Generic;

namespace StoresApplication.Models;

public partial class Vendor
{
    public string Name { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string Telephone { get; set; } = null!;

    public int Id { get; set; }

    public virtual ICollection<Delivery> Deliveries { get; } = new List<Delivery>();
}
