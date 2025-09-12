using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project2_MinhHungKhanh.Models;

public partial class MhkProduct
{
    [Display(Name = "Mã sản phẩm")]
    public int MhkProductId { get; set; }

    [Display(Name = "Mã danh mục")]
    public int MhkCategoryId { get; set; }

    [Display(Name = "Tên sản phẩm")]
    public string MhkName { get; set; } = null!;

    [Display(Name = "Đơn giá")]
    public decimal MhkPrice { get; set; }

    [Display(Name = "Mô tả")]
    public string? MhkDescription { get; set; }

    public virtual MhkCategory? MhkCategory { get; set; }

    public virtual ICollection<MhkOrderDetail> MhkOrderDetails { get; set; } = new List<MhkOrderDetail>();
}
