/// <reference path="../jquery-1.8.2.js" />



function ShowAjaxResult(obj, divMess) {
    /// <signature>
    ///   <summary>返回请求状态 提示请求消息(如果有跳转则弹消息 没有跳转显示消息 没有显示容器弹消息 没消息什么都不做)</summary>
    ///   <param name="obj" type="Object">json对象</param>
    ///   <param name="divMess" type="String">显示消息容器</param>   
    ///   <returns type="Boolean" />
    /// </signature>

    //异常 或 session超时
    if (obj.State == "0") {
        if (obj.Messg)
            alert(obj.Messg);//这里暂时alert一下  因为要跳转需要用户确定  (这里是从新的窗口打开 _blank)
        //  mymsg.showMsgErr(obj.Messg);
        if (!$("#a_err_href").length)
            $("body").append("<a id='a_err_href' target='_blank'></a>");
        $("#a_err_href").attr("href", "http://" + location.host + obj.JSurl);
        document.getElementById("a_err_href").click();
        //location.href = obj.JSurl;            
        return false;

    }
        //成功
    else if (obj.State == "1") {
        if (divMess && obj.Messg && !obj.JSurl)
            $(divMess).html(obj.Messg)
        else if (obj.Messg)
            alert(obj.Messg);
        if (obj.JSurl)
            location.href = obj.JSurl;
        return true;
    }
        //失败
    else if (obj.State == "2") {
        if (divMess && obj.Messg && !obj.JSurl)
            $(divMess).html(obj.Messg)
        else if (obj.Messg)
            alert(obj.Messg);
        if (obj.JSurl)
            location.href = obj.JSurl;
        return false;
    }
        //正常重定向
    else if (obj.State == "3") {
        if (divMess && obj.Messg && !obj.JSurl)
            $(divMess).html(obj.Messg)
        else if (obj.Messg)
            alert(obj.Messg);
        if (obj.JSurl)
            location.href = obj.JSurl;
        return true;
    }
    else return true;
}

/// <reference path="../jquery-1.7.1.js" /> 

//自执行函数
(function ($) {
    $.extend({
        //将json对象转换成字符串   [貌似jquery没有自带的这种方法]
        toJSONString: function (object) {
            if (object == null)
                return;
            var type = typeof object;
            if ('object' == type) {
                if (Array == object.constructor) type = 'array';
                else if (RegExp == object.constructor) type = 'regexp';
                else type = 'object';
            }
            switch (type) {
                case 'undefined':
                case 'unknown':
                    return;
                    break;
                case 'function':
                case 'boolean':
                case 'regexp':
                    return object.toString();
                    break;
                case 'number':
                    return isFinite(object) ? object.toString() : 'null';
                    break;
                case 'string':
                    return '"' + object.replace(/(\\|\")/g, "\\$1").replace(/\n|\r|\t/g, function () {
                        var a = arguments[0];
                        return (a == '\n') ? '\\n' : (a == '\r') ? '\\r' : (a == '\t') ? '\\t' : ""
                    }) + '"';
                    break;
                case 'object':
                    if (object === null) return 'null';
                    var results = [];
                    for (var property in object) {
                        var value = $.toJSONString(object[property]);
                        if (value !== undefined) results.push($.toJSONString(property) + ':' + value);
                    }
                    return '{' + results.join(',') + '}';
                    break;
                case 'array':
                    var results = [];
                    for (var i = 0; i < object.length; i++) {
                        var value = $.toJSONString(object[i]);
                        if (value !== undefined) results.push(value);
                    }
                    return '[' + results.join(',') + ']';
                    break;
            }
        },
        //获取url 参数
        getUrlVars: function () {
            var vars = [], hash;
            var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
            for (var i = 0; i < hashes.length; i++) {
                hash = hashes[i].split('=');
                vars.push(hash[0]);
                vars[hash[0]] = hash[1];
            }
            return vars;
        },
        //获取url 参数
        getUrlVar: function (name) {
            return $.getUrlVars()[name];
        },
        //取窗口可视范围的高度[浏览器可见区域高度]
        getClientHeight: function () {
            var clientHeight = 0;
            if (document.body.clientHeight && document.documentElement.clientHeight) {
                var clientHeight = (document.body.clientHeight < document.documentElement.clientHeight) ? document.body.clientHeight : document.documentElement.clientHeight;
            } else {
                var clientHeight = (document.body.clientHeight > document.documentElement.clientHeight) ? document.body.clientHeight : document.documentElement.clientHeight;
            }
            return clientHeight;
        },
        //取窗口滚动条高度[滚动条距离顶部的高度]
        getScrollTop: function () {
            var scrollTop = 0;
            if (document.documentElement && document.documentElement.scrollTop) {
                scrollTop = document.documentElement.scrollTop;
            } else if (document.body) {
                scrollTop = document.body.scrollTop;
            }
            return scrollTop;
        },
        //取文档内容实际高度
        getScrollHeight: function () {
            return Math.max(document.body.scrollHeight, document.documentElement.scrollHeight);
        },
        //滚动条距离底部的高度
        getScrollbheight: function () {
            return this.getScrollHeight() - this.getScrollTop() - this.getClientHeight();
        }
    });
}(jQuery))






