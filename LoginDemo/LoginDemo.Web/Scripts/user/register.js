var options = {
    registerUrl: "/Users/User/RegisterUser",
    loginUrl: "/Users/User/Login"
}

var register = (function () {
    return {
        init: function () {

            $(".register").click(function () {
                if ($("form").valid()) {
                    utils.ajax(options.registerUrl, "post", $("form").serialize(), function (re) {
                        console.log(re);
                        if (re.action === 1 && re.success) {
                            alert(re.msg);
                            location.href = options.loginUrl;
                        } else {
                            alert(re.msg);
                        }
                    });
                }

            });
        }
    }
})()

$(function () {

    register.init();
})