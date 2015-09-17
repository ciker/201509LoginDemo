var utils = (function () {

    return {

        ajax: function (url, methodType, requestData, success, callback) {
            $.ajax(
                {
                    url: url,
                    type: methodType || "get",
                    data: requestData || [],
                    dataType: "json",
                    success: function (res) {
                        if (res.action === 0) {
                            if (res.data.Message) {
                                alert(res.data.Message);
                            } else {
                                alert(res.msg);
                            }
                        } else {
                            success && success(res);
                        }
                    },
                    complete: function () {
                        callback && callback();
                    }
                }
            );
        }
    }

})()