using System;
using System.Collections.Generic;

namespace StoresApplication.Models;

public partial class Ownership
{
    public int StoreOwnerId { get; set; }

    public int StoreId { get; set; }

    public decimal Deposit { get; set; }


    public DateTime RegisterDate { get; set; }

    public int Id { get; set; }

    public virtual Store Store { get; set; } = null!;

    public virtual StoreOwner StoreOwner { get; set; } = null!;
}
