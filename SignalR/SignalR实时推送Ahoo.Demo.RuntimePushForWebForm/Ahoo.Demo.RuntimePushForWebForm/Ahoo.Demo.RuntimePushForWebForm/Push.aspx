<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Push.aspx.cs" Inherits="Ahoo.Demo.RuntimePushForWebForm.Push" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>消息推送管理</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:TextBox ID="txt_msg" runat="server"></asp:TextBox>
            <asp:Button ID="btn_Send"
                runat="server" Text="广播消息" OnClick="btn_Send_Click" />
        </div>
    </form>
    <div>
        <textarea rows="16" cols="66" id="msgbox"></textarea>
        <input type="button" id="btnsenmsg" value="SendMsg" />
    </div>
    <script type="text/javascript" src="Scripts/jquery-1.6.4.min.js"></script>
    <script type="text/javascript" src="Scripts/jquery.signalR-1.1.4.min.js"></script>
    <script src="/Signalr/Hubs"></script>
    <script type="text/javascript">
        $(function () {

            $("#btnsenmsg").click(function () {
                var msg = $("#msgbox").val();
                var chat = $.connection.noticeHub;
                $.connection.hub.start().done(function () {
                    console.log(chat);
                    chat.server.sendAllNotice(msg);
                    //chat.server.SendAllNotice(msg);
                });

            });
        })
    </script>
    <div>
    </div>
</body>
</html>
