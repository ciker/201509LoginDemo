/**
 * 
 * 
 *utils
 * 
 * 
 * jango
 * 
 * 
**/
!function () {
    var utils = {
        ajax: function (url, type, data, dataType, callback) {
            if (!url) {
                console.log('url can not be null');
                return false;
            }
            var _context = callback.context || null;
            $.ajax({
                url: url,
                data: data || {},
                type: type || 'get',
                dataType: dataType || 'html',
                beforeSend: function () {
                    callback.before && callback.before.call(_context);
                },
                success: function (resp, textStatus) {
                    console.log(textStatus);
                    callback.success && callback.success.call(_context, resp);
                },
                complete: function (xhr) {
                    callback.complete && callback.complete.call(_context, xhr);
                },
                error: function (xhr, textStatus, errorThrown) {
                    console.log(xhr);
                    console.log(textStatus);
                    console.log(errorThrown);
                    callback.error && callback.error.call(_context, errorThrown);
                }
            });
        },
        load: function (url, data, callback) {
            $(document).load(url, data || {}, function (resp, textStatus, xhr) {
                debugger;
                if (textStatus === "success") {
                    callback.success && callback.success.call(resp, xhr);
                }
            });
        }
    }

    if (typeof define === "function" && define.amd) {
        define(['jquery'], function () {
            return utils;
        });
        return false;
    }

    Window.utils = utils;

}()



