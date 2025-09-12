using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project2_MinhHungKhanh.Models;

public partial class MhkOrder
{
    [Display(Name = "Mã đơn hàng")]
    public int MhkOrderId { get; set; }

    public int MhkUserId { get; set; }

    [Display(Name = "Ngày đặt hàng")]
    public DateTime? MhkOrderDate { get; set; }

    [Display(Name = "Trạng thái")]
    public string? MhkStatus { get; set; }

    public virtual ICollection<MhkOrderDetail> MhkOrderDetails { get; set; } = new List<MhkOrderDetail>();

    public virtual MhkUser MhkUser { get; set; } = null!;
}
