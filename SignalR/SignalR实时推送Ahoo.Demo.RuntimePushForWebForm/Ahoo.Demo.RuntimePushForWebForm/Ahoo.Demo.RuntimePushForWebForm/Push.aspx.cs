using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.SignalR;
using Ahoo.Demo.RuntimePushForWebForm.Hubs;

namespace Ahoo.Demo.RuntimePushForWebForm
{
    public partial class Push : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_Send_Click(object sender, EventArgs e)
        {

            IHubContext chat = GlobalHost.ConnectionManager.GetHubContext<PushHub>();
            chat.Clients.All.notice(txt_msg.Text);
            chat.Clients.All.show(txt_msg.Text);
            //ClientScript.RegisterStartupScript(this.GetType(), "message", "<script language='javascript' defer>alert('广播成功!');</script>");

        }
    }
}