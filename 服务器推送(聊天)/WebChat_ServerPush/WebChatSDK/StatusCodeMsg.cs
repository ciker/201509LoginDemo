using System;
using System.Collections.Generic;
using System.Text;
#region 命名空间
using System.Net;
#endregion

namespace WebChatSDK
{
    public static class StatusCodeMsg
    {
        public static string GetMsg(HttpStatusCode code)
        {
            switch (code)
            {
                case HttpStatusCode.OK:
                    return "正常返回";
                case HttpStatusCode.Accepted:
                    return "接口执行错误,错误信息可以看msg";
                case HttpStatusCode.BadRequest:
                    return "参数错误";
                case HttpStatusCode.Unauthorized:
                    return "请求验证错误";
                case HttpStatusCode.Forbidden:
                    return "无权限操作";
                case HttpStatusCode.NotFound:
                    return "无此文件";
                case HttpStatusCode.NotAcceptable:
                    return "同时操作太多文件";
                case HttpStatusCode.RequestEntityTooLarge:
                    return "文件太大";
                case HttpStatusCode.InternalServerError:
                    return "服务器内部错误";
                default:
                    return code.ToString();
            }
        }
    }
}
