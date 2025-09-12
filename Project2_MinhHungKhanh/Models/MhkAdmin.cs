using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project2_MinhHungKhanh.Models;

public partial class MhkAdmin
{

    public int MhkAdminId { get; set; }


    [Display(Name = "Tên đăng nhập")]
    public string MhkUserName { get; set; } = null!;

    [Display(Name = "Mật khẩu")]
    public string MhkPassword { get; set; } = null!;

    [Display(Name = "Họ và tên")]
    public string? MhkFullName { get; set; }

    [Display(Name = "Vai trò")]
    public string? MhkRole { get; set; }
}
