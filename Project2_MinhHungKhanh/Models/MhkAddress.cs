using System;

namespace Project2_MinhHungKhanh.Models
{
    public partial class MhkAddress
    {
        public int MhkAddressId { get; set; }

        public int MhkUserId { get; set; }

        public string MhkLine1 { get; set; } = null!;

        // Cho phép null để không bị lỗi khi tạo mới
        public virtual MhkUser? MhkUser { get; set; }
    }
}
