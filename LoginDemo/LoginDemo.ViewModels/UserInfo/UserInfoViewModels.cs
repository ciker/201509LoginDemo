using System;
using System.ComponentModel.DataAnnotations;
using LoginDemo.Commom;

namespace LoginDemo.ViewModels.UserInfo
{
    public class UserInfoViewModels
    {
        public long Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} 必须至少包含 {2} 个字符。", MinimumLength = 6)]
        public string Account { get; set; }

        public int AccountType { get { return this.Account.GetAccountType(); } }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "{0} 必须至少包含 {2} 个字符。", MinimumLength = 6)]
        public string Password { get; set; }

        public string NickName { get; set; }

        public int? Gender { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "{0} 必须至少包含{2} 个字符", MinimumLength = 6)]
        public string CompanyName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} 必须至少包含{2} 个字符", MinimumLength = 6)]
        public string Address { get; set; }

        public string Remark { get; set; }



        public bool? IsDelete { get; set; }


        public int? DataStatus { get; set; }


        public DateTime? CreateDateTime { get; set; }


        public DateTime? UpdateDateTime { get; set; }
    }
}