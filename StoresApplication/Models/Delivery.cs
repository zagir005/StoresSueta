using System;
using System.Collections.Generic;

namespace StoresApplication.Models;

public partial class Delivery
{
    public int VendorId { get; set; }

    public int StoreId { get; set; }

    public decimal Price { get; set; }

    public int Id { get; set; }

    public virtual Store Store { get; set; } = null!;

    public virtual Vendor Vendor { get; set; } = null!;
}
