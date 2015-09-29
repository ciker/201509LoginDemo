using System;
using System.Collections.Generic;
using System.Text;
#region 命名空间
using System.Data;
using ChatDAL;
using ChatModel;
#endregion

namespace ChatBLL
{
    /// <summary>
    /// 聊天信息--业务逻辑类
    /// </summary>
    public class MessageBLL
    {
        #region 公用对象
        /// <summary>
        /// 聊天信息处理
        /// </summary>
        readonly MessageDAL messageDAL = new MessageDAL();
        #endregion

        #region 新增聊天信息
        /// <summary>
        /// 新增聊天信息记录
        /// </summary>
        /// <param name="message">聊天信息</param>
        /// <returns></returns>
        public bool AddMessage(MessageInfo message)
        {
            int AffectRows = messageDAL.AddMessage(message);
            return AffectRows > 0;
        }
        #endregion

        #region 修改聊天信息
        /// <summary>
        /// 修改聊天信息记录(根据id)
        /// </summary>
        /// <param name="message">聊天信息</param>
        /// <returns></returns>
        public bool UpdateMessage(MessageInfo message)
        {
            int AffectRows = messageDAL.UpdateMessage(message);
            return AffectRows > 0;
        }
        #endregion
    }
}
