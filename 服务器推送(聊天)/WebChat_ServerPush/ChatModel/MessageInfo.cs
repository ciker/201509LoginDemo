using System;
using System.Collections.Generic;
using System.Text;

namespace ChatModel
{
    /// <summary>
    /// 查看发送聊天信息
    /// </summary>
    public class MessageInfo
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 发送账号
        /// </summary>
        public string SendUserId { get; set; }
        /// <summary>
        /// 接受账号
        /// </summary>
        public string ReciveUserId { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public object Content { get; set; }
        /// <summary>
        /// 发送时间
        /// </summary>
        public string SendTime { get; set; }
        /// <summary>
        /// 接收时间
        /// </summary>
        public string ReciveTime { get; set; }
    }
}
