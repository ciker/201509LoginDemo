$(function () {
    for (var i = 10; i < 101; i++) {
        $("#selAge").append("<option value='" + i + "'>" + i + "</option>");
    }
    $("#selAge").val(20);
    //登录
    $("#btnSubmit").click(function () {
        var UserId = $("#txtUserId").val();
        if (!UserId) { $("#msg").text("请输入账  号！"); return; }
        var UserName = $("#txtUserName").val();
        if (!UserName) { $("#msg").text("请输入姓  名！"); return; }
        var PassWord = $("#txtPassWord").val();
        if (!PassWord) { $("#msg").text("请输入密  码！"); return; }
        var Sex = $("input[name='sex']:checked").val();
        var Age = $("#selAge").val();
        var Email = $("#txtEmail").val();
        if (!Email) { $("#msg").text("请输入邮  箱！"); return; }
        $.post("comet_broadcast.asyn",
        {
            Action: "Register",
            UserId: UserId,
            UserName: UserName,
            PassWord: PassWord,
            Sex: Sex,
            Age: Age,
            Email: Email
        },
        function (data, status) {
            window.location.href = "WebChat.aspx?UserId=" + UserId + "&PassWord=" + PassWord;
        }, "json");
    });
    //退出
    $("#btnExit").click(function () { window.close(); });
});