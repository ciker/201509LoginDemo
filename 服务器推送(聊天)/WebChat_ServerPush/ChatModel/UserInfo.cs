using System;
using System.Collections.Generic;
using System.Text;

namespace ChatModel
{
    /// <summary>
    /// 查看用户信息
    /// </summary>
    public class UserInfo
    {
        /// <summary>
        /// 账号
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string PassWord { get; set; }
        /// <summary>
        /// 性别
        /// 0--女
        /// 1--男
        /// </summary>
        public string Sex { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>
        public int Age { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 状态
        /// 0--离线
        /// 1--在线
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// 上线时间
        /// </summary>
        public string OnlineTime { get; set; }
        /// <summary>
        /// 离线时间 
        /// </summary>
        public string OfflineTime { get; set; }
    }
}
