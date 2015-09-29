using System;
using System.Collections.Generic;
using System.Text;

namespace WebChatSDK
{
    public class Parameter
    {
        /// <summary>
        /// 参数名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 参数值
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public Parameter(string name, string value)
        {
            this.Name = name;
            this.Value = value;
        }
    }
}
