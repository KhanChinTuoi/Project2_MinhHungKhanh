using System;
using System.Collections.Generic;

namespace Project2_MinhHungKhanh.Models;

public partial class MhkCategory
{
    public int MhkCategoryId { get; set; }

    public string MhkCategoryName { get; set; } = null!;

    public virtual ICollection<MhkProduct> MhkProducts { get; set; } = new List<MhkProduct>();
}
