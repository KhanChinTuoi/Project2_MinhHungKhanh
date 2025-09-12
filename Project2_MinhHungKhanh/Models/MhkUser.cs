    using System;
    using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

    namespace Project2_MinhHungKhanh.Models;

    public partial class MhkUser
    {
        [Display(Name = "Mã người dùng")]
        public int MhkUserId { get; set; }

        [Display(Name = "Họ và tên")]     
        public string MhkFullName { get; set; } = null!;

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Vui lòng nhập Email")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ, vui lòng nhập lại")]
        public string MhkEmail { get; set; } = null!;

        public virtual ICollection<MhkAddress> MhkAddresses { get; set; } = new List<MhkAddress>();

        public virtual ICollection<MhkOrder> MhkOrders { get; set; } = new List<MhkOrder>();
    }
