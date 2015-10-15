using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace Ahoo.Demo.RuntimePushForWebForm.Hubs
{
    [HubName("noticeHub")]
    public class NoticeHub : Hub
    {
        public void SendAllNotice(string Msg)
        {
            Clients.All.GetAllMsg(Msg);
        }

        public void SendOthersNotice(string Msg)
        {
            Clients.Others.GetOtherMsg(Msg);
        }

    }
}