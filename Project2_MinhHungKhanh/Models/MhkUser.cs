using System;
using System.Collections.Generic;

namespace Project2_MinhHungKhanh.Models;

public partial class MhkUser
{
    public int MhkUserId { get; set; }

    public string MhkFullName { get; set; } = null!;

    public string MhkEmail { get; set; } = null!;

    public virtual ICollection<MhkAddress> MhkAddresses { get; set; } = new List<MhkAddress>();

    public virtual ICollection<MhkOrder> MhkOrders { get; set; } = new List<MhkOrder>();
}
