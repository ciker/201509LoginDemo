using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace Ahoo.Demo.RuntimePushForWebForm.Hubs
{

    [HubName("pushHub")]
    public class PushHub : Hub
    {

    }
}