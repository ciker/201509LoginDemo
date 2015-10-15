<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Chat.aspx.cs" Inherits="SignalR.Chat" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>SignalR 聊天室</title>
    <style>
        #userName {
            display: none;
            color: red;
        }

        #messageBox, #chatList {
            float: left;
            height: 300px;
            width: 300px;
            overflow: auto;
        }

        #messageBox {
            border: 1px solid #000;
        }

        #chatList {
            width: 150px;
            overflow: scroll;
        }

        #list li {
            cursor: pointer;
        }

        #bar {
            clear: both;
        }

        p {
            margin: 0;
        }
    </style>
</head>
<body>
    <p id="userName">Hi！ </p>
    <div id="messageBox">
        <p>聊天室內容</p>
        <ul id="messageList"></ul>
    </div>
    <div id="chatList">
        <p>上線清單</p>
        <ul id="list">
        </ul>
    </div>
    <div id="bar">
        <select id="box">
            <option value="all">所有人</option>
        </select>
        <input type="text" id="message" />
        <input type="button" id="send" value="發送" />
    </div>
    <%-- 引用 jQuery 的參考--%>
    <script src="/Scripts/jquery-1.6.4.min.js"></script>
    <%--引用 SignalR 的參考--%>
    <script src="/Scripts/jquery.signalR-1.1.1.min.js"></script>

    <%--這邊滿重要的，這個參考是動態產生的，當我們build之後才會動態建立這個資料夾，且需引用在jQuery和signalR之後--%>
    <script src="/signalr/hubs"></script>

    <script type="text/javascript">
        var userID = "";

        $(function () {
            while (userID.length == 0) {
                userID = window.prompt("請輸入使用者名稱");
                if (!userID)
                    userID = "";
            }
            $("#userName").append(userID).show();

            //建立與Server端的Hub的物件，注意Hub的開頭字母一定要為小寫
            var chat = $.connection.codingChatHub;

            //取得所有上線清單
            chat.client.getList = function (userList) {
                var li = "";
                $.each(userList, function (index, data) {
                    li += "<li id='" + data.id + "'>" + data.name + "</li>";
                });
                $("#list").html(li);
            }
            //新增一筆上線人員
            chat.client.addList = function (id, name) {
                var li = "<li id='" + id + "'>" + name + "</li>";
                $("#list").append(li);
            }
            //移除一筆上線人員
            chat.client.removeList = function (id) {
                $("#" + id).remove();
            }

            //全體聊天
            chat.client.sendAllMessge = function (message) {
                $("#messageList").append("<li>" + message + "</li>");
            }

            //密語聊天
            chat.client.sendMessage = function (message) {
                $("#messageList").append("<li>" + message + "</li>");
            }

            chat.client.hello = function (message) {
                $("#messageList").append("<li>" + message + "</li");
            }

            //將連線打開
            $.connection.hub.start().done(function () {
                //當連線完成後，呼叫Server端的userConnected方法，並傳送使用者姓名給Server
                chat.server.userConnected(userID);
            });;

            $("#send").click(function () {
                var to = $("#box").val();
                //當to為all代表全體聊天，否則為私密聊天
                if (to == "all") {
                    chat.server.sendAllMessage($("#message").val());
                } else {
                    chat.server.sendMessage(to, $("#message").val());
                }
                $("#message").val('');
            });

            $("#list li").live("click", function () {
                var $this = $(this);
                var id = $this.attr("id");
                var text = $this.text();

                //防止重複加入密語清單
                if ($("#box").has("." + id).length > 0)
                    return false;

                var option = "<option></option>"
                $("#box").append(option).find("option:last").val(id).text(text).attr({ "selected": "selected" }).addClass(id);
            });

        });
    </script>

</body>
</html>
