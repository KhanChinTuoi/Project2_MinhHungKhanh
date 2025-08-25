using System;
using System.Collections.Generic;

namespace Project2_MinhHungKhanh.Models;

public partial class MhkProduct
{
    public int MhkProductId { get; set; }

    public int MhkCategoryId { get; set; }

    public string MhkName { get; set; } = null!;

    public decimal MhkPrice { get; set; }

    public string? MhkDescription { get; set; }

    public virtual MhkCategory MhkCategory { get; set; } = null!;

    public virtual ICollection<MhkOrderDetail> MhkOrderDetails { get; set; } = new List<MhkOrderDetail>();
}
