<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebChat.aspx.cs" Inherits="WebChat_ServerPush.WebChat" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>用户聊天</title>
    <link href="Styles/WebChat.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        var current_user = {
            UserId: "<%=UserId%>",
            UserName: "<%=UserName%>",
            PassWord: "<%=PassWord%>",
            Sex: "<%=Sex%>",
            Age: "<%=Age%>",
            Email: "<%=Email%>"
        };
    </script>
    <script src="Scripts/WebChat.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="chatMain">
        <div id="Friends">
            <div class="list_online">
                <div class="title">
                    在线好友</div>
                <div class="list">
                </div>
            </div>
            <div class="list_offline">
                <div class="title">
                    离线好友</div>
                <div class="list">
                </div>
            </div>
        </div>
        <div id="Messages">
            <div class="user_info">
            </div>
            <div class="chat">
                <div class="title">
                    聊天内容</div>
                <div class="message">
                </div>
                <div class="send_msg">
                    <textarea id="txtSendMsg" rows="0" cols="30">输入聊天内容...</textarea>
                </div>
                <div class="operate">
                    <div id="btnSendMsg">
                        发送</div>
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
