using System;
using System.Collections.Generic;

namespace Project2_MinhHungKhanh.Models;

public partial class MhkAddress
{
    public int MhkAddressId { get; set; }

    public int MhkUserId { get; set; }

    public string MhkLine1 { get; set; } = null!;

    public virtual MhkUser MhkUser { get; set; } = null!;
}
