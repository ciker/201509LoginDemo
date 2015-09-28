//返回主页
function returnherf(name) {
    if (name)
        location.href = "http://" + location.host + "/" + name;
    else
        location.href = "http://" + location.host;
}

//注销
function Cancellation() {
    $.get("/UserManage/Cancellation", "", function () {
        location.href = "/";
    });
}

//点击搜索
function sosoclick(obj) {
    var key = $(".text_soso").val().trim();
    if (obj) {
        key = "blog:" + obj + " " + key;
    }
    var url = "http://" + location.host + "/Search/Index?key=" + key + "&p=1";
    location.href = url;
}
//回车搜索
function sosokeyup(obj) {
    var keycode = event.keyCode;
    if (keycode == "13") {
        sosoclick(obj);
    }
}

//自定义 定时器[当元素加载完成是执行回调函数]
function customTimer(inpId, fn) {
    if ($(inpId).length) {
        fn();
    }
    else {
        var intervalId = setInterval(function () {
            if ($(inpId).length) {  //如果存在了
                clearInterval(intervalId);  // 则关闭定时器
                customTimer(inpId, fn);              //执行自身
            }
        }, 100);
    }
}