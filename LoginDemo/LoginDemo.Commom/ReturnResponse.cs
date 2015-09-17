
using System;
namespace LoginDemo.Commom
{
    public class ReturnResponse<T> where T : new()
    {
        public T Body { get; set; }
        public bool IsSuccess { get { return this.ResponseCode == 1; } }

        /// <summary>
        /// 状态码
        ///未知错误 = -1,
        ///成功 = 1,
        ///验证失败 = 100,
        ///错误 = 400,
        ///无权限访问 = 403,
        ///服务器错误 = 500
        /// </summary>
        public int ResponseCode { get; set; }

        public string Message { get; set; }
    }
}
