using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace Blogs.Common.CustomModel
{
    public enum EnumState
    {
        异常或Session超时 = 0,
        成功 = 1,
        失败 = 2,
        正常重定向 = 3
    }

    public class JSData
    {
        /// <summary>
        /// 状态
        /// </summary>
        public EnumState State = EnumState.成功;

        /// <summary>
        /// 消息
        /// </summary>
        public string Messg;
        /// <summary>
        /// 数据
        /// </summary>
        public object Data;

        /// <summary>
        /// 重定向url         
        /// </summary>
        public string JSurl;
    }
}
