using System;
using System.Collections.Generic;

namespace Project2_MinhHungKhanh.Models;

public partial class MhkOrder
{
    public int MhkOrderId { get; set; }

    public int MhkUserId { get; set; }

    public DateTime? MhkOrderDate { get; set; }

    public string? MhkStatus { get; set; }

    public virtual ICollection<MhkOrderDetail> MhkOrderDetails { get; set; } = new List<MhkOrderDetail>();

    public virtual MhkUser MhkUser { get; set; } = null!;
}
