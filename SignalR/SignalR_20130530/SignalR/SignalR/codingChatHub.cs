using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;

namespace SignalR
{
    public class codingChatHub : Hub
    {

        //宣告靜態類別，來儲存上線清單
        public static class UserHandler
        {
            public static Dictionary<string, string> ConnectedIds = new Dictionary<string, string>();
        }

        //當使用者斷線時呼叫
        public override Task OnDisconnected()
        {
            //當使用者離開時，移除在清單內的 ConnectionId
            Clients.All.removeList(Context.ConnectionId);
            UserHandler.ConnectedIds.Remove(Context.ConnectionId);
            return base.OnDisconnected();
        }

        //使用者連現時呼叫
        public void userConnected(string name)
        {
            //進行編碼，防止XSS攻擊
            name = HttpUtility.HtmlEncode(name);
            string message = "歡迎使用者 " + name + " 加入聊天室";

            //發送訊息給除了自己的其他
            Clients.Others.addList(Context.ConnectionId, name);
            Clients.Others.hello(message);

            //發送訊息至自己，並且取得上線清單
            Clients.Caller.getList(UserHandler.ConnectedIds.Select(p => new { id = p.Key, name = p.Value }).ToList());

            //新增目前使用者至上線清單
            UserHandler.ConnectedIds.Add(Context.ConnectionId, name);
        }

        //發送訊息給所有人
        public void sendAllMessage(string message)
        {
            message = HttpUtility.HtmlEncode(message);
            var name = UserHandler.ConnectedIds.Where(p => p.Key == Context.ConnectionId).FirstOrDefault().Value;
            message = name + "說：" + message;
            Clients.All.sendAllMessge(message);
        }


        //發送訊息至特定使用者
        public void sendMessage(string ToId, string message)
        {
            message = HttpUtility.HtmlEncode(message);
            var fromName = UserHandler.ConnectedIds.Where(p => p.Key == Context.ConnectionId).FirstOrDefault().Value;
            message = fromName + " <span style='color:red'>悄悄的對你說</span>：" + message;
            Clients.Client(ToId).sendMessage(message);
        }
    }
}