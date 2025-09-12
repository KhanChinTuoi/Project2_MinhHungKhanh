using System;
using System.ComponentModel.DataAnnotations;

namespace Project2_MinhHungKhanh.Models
{
    public partial class MhkAddress
    {
        [Display(Name = "Mã địa chỉ")]
        public int MhkAddressId { get; set; }


        [Display(Name = "Mã người dùng")]
        public int MhkUserId { get; set; }

        [Display(Name = "Địa chỉ")]
        public string MhkLine1 { get; set; } = null!;

        // Cho phép null để không bị lỗi khi tạo mới
        public virtual MhkUser? MhkUser { get; set; }
    }
}
