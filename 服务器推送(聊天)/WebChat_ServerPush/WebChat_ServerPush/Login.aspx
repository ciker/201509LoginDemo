<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WebChat_ServerPush.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>用户登录</title>
    <link href="Styles/Login.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="Scripts/Login.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="loginMain">
        <div id="loginTitle">
            登录
        </div>
        <div id="loginUserId">
            <table>
                <tr>
                    <td>
                        用户名：
                    </td>
                    <td>
                        <input type="text" id="txtUserId" />
                    </td>
                </tr>
            </table>
        </div>
        <div id="loginPassWord">
            <table>
                <tr>
                    <td>
                        密&nbsp;&nbsp;码：
                    </td>
                    <td>
                        <input type="password" id="txtPassWord" />
                    </td>
                </tr>
            </table>
        </div>
        <div id="loginOperate">
            <div id="btnSubmit">
                登录</div>
            <div id="btnRegister">
                注册</div>
            <div id="msg">
            </div>
        </div>
    </div>
    </form>
</body>
</html>
