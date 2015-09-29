using System;
using System.Collections.Generic;
using System.Text;
#region 命名空间
using System.Web;
using Newtonsoft.Json;
#endregion

namespace ChatModel
{
    /// <summary>
    /// 处理信息类
    ///     ResponseData--输出处理数据
    ///     ResponseDetails--处理详细信息
    ///     ResponseStatus--处理状态
    ///         -1=失败,0=没有获取数据,1=处理成功！
    /// </summary>
    public class ResponseResult
    {
        public object ResponseData { get; set; }
        public string ResponseDetails { get; set; }
        public int ResponseStatus { get; set; }
        public string ResultString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        public void ResponseWrite()
        {
            HttpContext.Current.Response.Write(ResultString());
        }
    }
}
