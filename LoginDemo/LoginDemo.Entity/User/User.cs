using System;
using System.Data.Entity;

namespace LoginDemo.Entity
{
    public class User : BaseEntity // DbContext,
    {
        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 密码 加密后
        /// </summary>
        public string UserPWD { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string Mobile { get; set; }



        public bool? IsDelete { get; set; }

        public int? DataStatus { get; set; }

        public DateTime? CreateDateTime { get; set; }

        public DateTime? UpdateDateTime { get; set; }
    }
}
