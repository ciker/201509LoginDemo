using System;
using System.Collections.Generic;
using System.Text;
#region 命名空间
using WebChatSDK.PHPLibrary;
#endregion

namespace WebChatSDK
{
    /// <summary>
    /// OAuth签名
    /// </summary>
    public class OAuth
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="sdk"></param>
        public OAuth(WebChat sdk)
        {
            this.SDK = sdk;
        }

        /// <summary>
        /// SDK
        /// </summary>
        public WebChat SDK { get; private set; }

        /// <summary>
        /// 访问地址
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 其他参数
        /// </summary>
        public PHPArray Array { get; set; }

        /// <summary>
        /// 生成Url
        /// </summary>
        /// <returns></returns>
        public string GetUrl()
        {
            string result = string.Concat(this.Url, this.ParameToString(false));
            return result;
        }

        /// <summary>
        /// 返回参数字符串
        /// </summary>
        /// <param name="isEncode">是否urlencode</param>
        /// <returns></returns>
        private string ParameToString(bool isEncode)
        {
            List<Parameter> list = this.GetParameList();
            return OAuth.ParameToString(list, isEncode);
        }

        /// <summary>
        /// 生成所有参数
        /// </summary>
        /// <returns></returns>
        private List<Parameter> GetParameList()
        {
            List<Parameter> parame = new List<Parameter>();
            parame.Add(new Parameter("UserId", this.SDK.User.UserId));
            parame.Add(new Parameter("PassWord", this.SDK.User.PassWord));
            if (!string.IsNullOrEmpty(this.SDK.CallBackUrl))
            {
                parame.Add(new Parameter("oauth_callback", this.SDK.CallBackUrl));
            }
            if (this.Array != null)
            {
                foreach (KeyValuePair<object, object> p in this.Array)
                {
                    parame.Add(new Parameter((string)p.Key, p.Value.ToString()));
                }
            }
            parame.Sort(delegate(Parameter x, Parameter y)
            {
                if (x.Name == y.Name)
                {
                    return string.Compare(x.Value, y.Value);
                }
                else
                {
                    return string.Compare(x.Name, y.Name);
                }
            });
            return parame;
        }

        /// <summary>
        /// 返回参数字符串
        /// </summary>
        /// <param name="par"></param>
        /// <param name="isEncode"></param>
        /// <returns></returns>
        public static string ParameToString(List<Parameter> par, bool isEncode)
        {
            StringBuilder ParameString = new StringBuilder();
            for (int i = 0; i < par.Count; i++)
            {
                string formatString = i == 0 ? "?{0}={1}" : "&{0}={1}";
                if (isEncode)
                {
                    ParameString.AppendFormat(formatString, Common.UrlEncode(par[i].Name), Common.UrlEncode(par[i].Value));
                }
                else
                {
                    ParameString.AppendFormat(formatString, par[i].Name, par[i].Value);
                }
            }
            return ParameString.ToString();
        }
    }
}
