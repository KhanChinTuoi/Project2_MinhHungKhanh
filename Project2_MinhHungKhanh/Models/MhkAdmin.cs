using System;
using System.Collections.Generic;

namespace Project2_MinhHungKhanh.Models;

public partial class MhkAdmin
{
    public int MhkAdminId { get; set; }

    public string MhkUserName { get; set; } = null!;

    public string MhkPassword { get; set; } = null!;

    public string? MhkFullName { get; set; }

    public string? MhkRole { get; set; }
}
