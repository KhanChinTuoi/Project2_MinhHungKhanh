using System;
using System.Collections.Generic;

namespace Project2_MinhHungKhanh.Models;

public partial class MhkOrderDetail
{
    public int MhkOrderDetailId { get; set; }

    public int MhkOrderId { get; set; }

    public int MhkProductId { get; set; }

    public int MhkQuantity { get; set; }

    public decimal MhkUnitPrice { get; set; }

    public virtual MhkOrder MhkOrder { get; set; } = null!;

    public virtual MhkProduct MhkProduct { get; set; } = null!;
}
