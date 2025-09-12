using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project2_MinhHungKhanh.Models;

public partial class MhkOrderDetail
{
    [Required]
    public int MhkOrderDetailId { get; set; }

    [Display(Name = "Mã đơn hàng")]
    public int MhkOrderId { get; set; }

    [Display(Name = "Mã sản phẩm")]
    public int MhkProductId { get; set; }

    [Display(Name = "Số lượng")]
    public int MhkQuantity { get; set; }

    [Display(Name = "Đơn giá đơn vị")]
    public decimal MhkUnitPrice { get; set; }

    public virtual MhkOrder? MhkOrder { get; set; } 

    public virtual MhkProduct? MhkProduct { get; set; } 
}
