using System;
using System.Collections.Generic;
using System.Text;
#region 命名空间
using WebChatSDK;
using ChatModel;
using Newtonsoft.Json;
using System.Web;
#endregion

namespace ServerPushHttpHandler
{
    public class ServerPushHandler
    {
        #region 全局变量
        HttpContext m_Context;
        //推送结果
        ServerPushResult _IAsyncResult;
        //声明一个集合
        static Dictionary<string, ServerPushResult> dict = new Dictionary<string, ServerPushResult>();
        //sdk对外接口
        WebChat sdk = new WebChat();
        #endregion

        #region 构造函数
        /// <summary>
        /// 构造方法
        /// </summary>
        public ServerPushHandler(HttpContext context, ServerPushResult _IAsyncResult)
        {
            this.m_Context = context;
            this._IAsyncResult = _IAsyncResult;
        }
        #endregion

        #region 执行操作
        /// <summary>
        /// 根据Action判断执行方法
        /// </summary>
        /// <returns></returns>
        public ServerPushResult ExecAction()
        {
            sdk.User = new UserInfo()
            {
                UserId = m_Context.Request["UserId"],
                PassWord = m_Context.Request["PassWord"]
            };
            switch (m_Context.Request["Action"])
            {
                case "Register":
                    Register();
                    break;
                case "Online":
                    Online();
                    break;
                case "Offline":
                    Offline();
                    break;
                case "Keepline":
                    Keepline();
                    break;
                case "GetFriends":
                    GetFriends();
                    break;
                case "SendMsg":
                    SendMsg();
                    break;
                default:
                    break;
            }
            return _IAsyncResult;
        }
        #endregion

        #region 注册用户
        private void Register()
        {
            sdk.User.UserName = m_Context.Request["UserName"];
            sdk.User.Sex = m_Context.Request["Sex"];
            sdk.User.Age = Convert.ToInt32(m_Context.Request["Age"]);
            sdk.User.Email = m_Context.Request["Email"];
            _IAsyncResult.Result = sdk.Register_User();
            if (!dict.ContainsKey(sdk.User.UserId))
            {
                dict.Add(sdk.User.UserId, _IAsyncResult);
            }
            _IAsyncResult.Send();
        }
        #endregion

        #region 用户上线
        private void Online()
        {
            _IAsyncResult.Result = sdk.Verify_User();
            if (!dict.ContainsKey(sdk.User.UserId))
            {
                dict.Add(sdk.User.UserId, _IAsyncResult);
            }
            _IAsyncResult.Send();
        }
        #endregion

        #region 用户下线
        private void Offline()
        {
            _IAsyncResult.Result = sdk.Downline_User();
            if (dict.ContainsKey(sdk.User.UserId))
            {
                dict.Remove(sdk.User.UserId);
            }
            _IAsyncResult.Send();
        }
        #endregion

        #region 保持联接
        private void Keepline()
        {
            if (!dict.ContainsKey(sdk.User.UserId))
                dict.Add(sdk.User.UserId, _IAsyncResult);
            else //登录时虽然保存了当前用户的连接，但是登录完后异步向客户端推送了数据，此时这个客户端连接已经失效，那么在connect时相当于才是此客户端与服务器端真正的连接，需要重新更新ServerPushResult的值
                dict[sdk.User.UserId] = _IAsyncResult;
        }
        #endregion

        #region 获取好友
        private void GetFriends()
        {
            _IAsyncResult.Result = sdk.Get_Friends();
            _IAsyncResult.Send();
        }
        #endregion

        #region 添加好友
        private void AddFriend()
        {
            UserInfo friend = new UserInfo()
            {
                UserId = m_Context.Request["FriendId"]
            };
            _IAsyncResult.Result = sdk.Add_Friend(friend);
            _IAsyncResult.Send();
        }
        #endregion

        #region 发送消息
        private void SendMsg()
        {
            MessageInfo message = new MessageInfo()
            {
                SendUserId = m_Context.Request["UserId"],
                ReciveUserId = m_Context.Request["ReciveUserId"],
                Content = m_Context.Request["Content"]
            };
            string result = sdk.Send_Msg(message);
            if (dict.ContainsKey(message.ReciveUserId))
            {
                dict[message.ReciveUserId].Result = result;
                dict[message.ReciveUserId].Send();
            }
            _IAsyncResult.Result = result;
            _IAsyncResult.Send();
        }
        #endregion

        #region 全站广播
        public void SendMsg(string Action, string strContent)
        {
            MessageInfo message = new MessageInfo()
            {
                SendUserId = m_Context.Request["UserId"],
                ReciveUserId = m_Context.Request["ReciveUserId"],
                Content = m_Context.Request["Content"]
            };
            foreach (ServerPushResult IAsyncResult in dict.Values)
            {
                //IAsyncResult.ResultXml = strContent;
                //IAsyncResult.Send(null);
            }
        }
        #endregion
    }
}
