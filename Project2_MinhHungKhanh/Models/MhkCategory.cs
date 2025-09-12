using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project2_MinhHungKhanh.Models;

public partial class MhkCategory
{
    [Display(Name = "Mã danh mục")]
    public int MhkCategoryId { get; set; }

    [Display(Name = "Tên danh mục")]
    public string MhkCategoryName { get; set; } = null!;

    public virtual ICollection<MhkProduct> MhkProducts { get; set; } = new List<MhkProduct>();
}
