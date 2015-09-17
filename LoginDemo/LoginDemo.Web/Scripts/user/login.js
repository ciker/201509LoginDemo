var options = {
    loginUrl: "/Users/User/Login",
    userlistUrl: "/Users/User/UserList"
}

var login = (function () {
    return {
        init: function () {

            $(".login").click(function () {
                if ($("form").valid()) {

                    utils.ajax(options.registerUrl, "post", $("form").serialize(), function (re) {
                        console.log(re);
                        if (re.action === 1 && re.success) {
                            location.href = options.userlistUrl;
                        }
                    });

                }
            });
        }
    }
})();

$(function () {

    login.init();
})