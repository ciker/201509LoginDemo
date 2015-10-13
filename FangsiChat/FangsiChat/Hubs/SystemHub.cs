using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace FangsiChat.Hubs
{
    [HubName("systemHub")]
    public class SystemHub : Hub
    {
        #region Data Members
        public static List<UserDetail> ConnectedUsers = new List<UserDetail>();
        #endregion

        /// <summary>
        /// 登录连线
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="userName">用户名</param>
        /// <param name="deptName">部门名</param>
        public void Connect(string userID, string userName, string deptName)
        {
            var id = Context.ConnectionId;

            if (ConnectedUsers.Count(x => x.ConnectionId == id) == 0)
            {
                if (ConnectedUsers.Count(x => x.UserID == userID) > 0)
                {
                    var items = ConnectedUsers.Where(x => x.UserID == userID).ToList();
                    foreach (var item in items)
                    {
                        Clients.AllExcept(id).onUserDisconnected(item.ConnectionId, item.UserName);
                    }
                    ConnectedUsers.RemoveAll(x => x.UserID == userID);
                }
                //添加在线人员
                ConnectedUsers.Add(new UserDetail { ConnectionId = id, UserID = userID, UserName = userName, DeptName = deptName, LoginTime = DateTime.Now });

                // 反馈信息给登录者
                Clients.Caller.onConnected(id, userName, ConnectedUsers);

                // 通知所有用户，有新用户连接
                Clients.AllExcept(id).onNewUserConnected(id, userID, userName, deptName, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            }
            else
            {

            }
        }

        /// <summary>
        /// 发送私聊
        /// </summary>
        /// <param name="toUserId">接收方用户连接ID</param>
        /// <param name="message">内容</param>
        public void SendPrivateMessage(string toUserId, string message)
        {
            var fromUserId = Context.ConnectionId;

            var toUser = ConnectedUsers.FirstOrDefault(x => x.ConnectionId == toUserId);
            var fromUser = ConnectedUsers.FirstOrDefault(x => x.ConnectionId == fromUserId);

            if (toUser != null && fromUser != null)
            {   // send to 
                Clients.Client(toUserId).receivePrivateMessage(fromUserId, fromUser.UserName, message);

                // send to caller user
                //Clients.Caller.sendPrivateMessage(toUserId, fromUser.UserName, message);
            }
            else
            {
                //表示对方不在线
                Clients.Caller.absentSubscriber();
            }
        }

        /// <summary>
        /// 离线
        /// </summary>
        public override System.Threading.Tasks.Task OnDisconnected(bool stopCalled)
        {
            var item = ConnectedUsers.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
            if (item == null) return base.OnDisconnected(stopCalled);
            Clients.All.onUserDisconnected(item.ConnectionId, item.UserName);   //调用客户端用户离线通知
            ConnectedUsers.Remove(item);
            return base.OnDisconnected(stopCalled);
        }
    }
}