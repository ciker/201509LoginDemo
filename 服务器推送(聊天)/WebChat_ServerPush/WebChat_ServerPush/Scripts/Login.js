$(function () {
    //登录
    $("#btnSubmit").click(function () {
        var UserId = $("#txtUserId").val();
        if (!UserId) { $("#msg").text("请输入账  号！"); return; }
        var PassWord = $("#txtPassWord").val();
        if (!PassWord) { $("#msg").text("请输入密  码！"); return; }
        $.post("comet_broadcast.asyn",
        {
            Action: "Online",
            UserId: UserId,
            PassWord: PassWord
        },
        function (data, status) {
            if (data.ResponseStatus != 1) {
                $("#msg").text(data.ResponseDetails);
                return;
            }
            var url = "WebChat.aspx";
            url += "?UserId=" + data.ResponseData.UserId;
            url += "&UserName=" + data.ResponseData.UserName;
            url += "&PassWord=" + data.ResponseData.PassWord;
            url += "&Sex=" + data.ResponseData.Sex;
            url += "&Age=" + data.ResponseData.Age;
            url += "&Email=" + data.ResponseData.Email;
            window.location.href = url;
        }, "json");
    });
    //注册
    $("#btnRegister").click(function () {
        window.location.href = "Register.aspx";
    });
});