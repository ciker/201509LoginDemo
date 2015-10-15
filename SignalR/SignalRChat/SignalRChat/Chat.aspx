<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Chat.aspx.cs" Inherits="SignalRChat.Chat" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Signalr Chat Messenger</title>
    <script src="Scripts/jquery-1.6.4.min.js" type="text/javascript"></script>
    <script src="Scripts/jquery.signalR-1.0.0-rc1.js" type="text/javascript"></script>
    <script src="signalr/hubs" type="text/javascript"></script>
    <link href="Css/main.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">

        <script type="text/javascript">
            $(function () {

                //            var IWannaChat = $.connection.myChatHub;

                //            IWannaChat.client.addMessage = function (message) {
                //                $('#listMessages').append('<li>' + message + '</li>');
                //            };

                //            $("#SendMessage").click(function () {
                //                IWannaChat.server.send($('#txtMessage').val());
                //            });

                //            $.connection.hub.start();
            });

            $(function () {

                //创建链接的实例
                var IWannaChat = $.connection.myChatHub;
                var count = 0;

                //浏览文件
                $("#btnBrowse").bind("click", function () {
                    $("#fileBrowe").click();
                    $("#fileBrowe").bind("change", function () {
                        var path = $(this).val();
                        console.log($(this))
                        if (path != null && path != "") {
                            //当选择好文件以后,就将文件路径信息加入到UI中.
                            $('#listFiles').append('<tr><td id="fileNameSpecific">' + path + '</td><td id="myPrograss' + (count) + '" "></td><td id="myState' + count + '">Ready</td></tr>');
                            count++;
                            preventDefault();
                        }
                    });
                });

                //点击上传按钮,将文件名称用竖线分割,然后发送到后台
                $("#btnUpload").bind("click", function () {
                    var resultFeed = "";
                    $("#listFiles td ").each(function (index, element) {
                        if (index % 3 == 0)  //get feed names and concreate.
                            resultFeed = $(this).text() + "|" + resultFeed;
                    });
                    if (resultFeed != null && resultFeed != "")
                        //将文件发送到后台
                        IWannaChat.server.send(resultFeed.substring(0, resultFeed.length - 1));
                });

                //这个主要是接收后台处理的结果,然后打印到前台来
                IWannaChat.client.addMessage = function (message) {
                    if (message.contains("|")) {
                        var result = message.split('|');
                        var fileFlag = result[0];
                        var filePrograss = result[1];

                        $('#myPrograss' + fileFlag).html('<table><tr><th  style="width:' + filePrograss * 200 + 'px;background-color:green;"></th><th style="line-height:10px;background-color:white;border:none;">' + parseInt(filePrograss * 100) + '%</th></tr></table>');
                        if (filePrograss != 1)
                            $('#myState' + fileFlag).html('In Prograss');
                        else
                            $('#myState' + fileFlag).html('Done');
                    }
                    else {
                        $("#log").append("<li>" + message + "</li>");
                    }
                };

                //开启(长轮训的方式)
                $.connection.hub.start();
            });

            String.prototype.contains = function (strInput) {
                return this.indexOf(strInput) != -1;
            }

        </script>

        <div>
            <input type="button" id="btnBrowse" class="button green" value="Browse files" />
            <input type="file" id="fileBrowe" style="display: none;" />
            <input type="button" id="btnUpload" class="button green" value="Upload" /><br />

            <table id="listFiles" cellspacing="0">
                <caption></caption>
                <tr>
                    <th scope="col">文件名</th>
                    <th scope="col" style="width: 300px;">当前进度</th>
                    <th scope="col" style="width: 100px;">状态</th>
                </tr>
            </table>

            <ul id="log"></ul>
        </div>
    </form>
</body>
</html>
