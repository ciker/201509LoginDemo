using System;
using System.Collections.Generic;
using System.Text;
#region 命名空间
using WebChatSDK.PHPLibrary;
using Newtonsoft.Json.Linq;
#endregion

namespace WebChatSDK
{
    public static class Common
    {
        /// <summary>
        /// 对字符串进行编码
        /// </summary>
        /// <param name="value">要编码的字符串</param>
        /// <returns>编码后的字符串</returns>
        public static string UrlEncode(string value)
        {
            return UrlEncode(value, string.Empty);
        }

        public static string UrlEncode(string str, string other)
        {
            StringBuilder sb = new StringBuilder();
            string strSpecial = string.Format("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_.~{0}", other);
            for (int i = 0; i < str.Length; i++)
            {
                string crt = str.Substring(i, 1);
                if (strSpecial.Contains(crt))
                {
                    sb.Append(crt);
                }
                else
                {
                    byte[] bts = Encoding.UTF8.GetBytes(crt);
                    foreach (byte bt in bts)
                    {
                        sb.Append("%" + bt.ToString("X"));
                    }
                }
            }
            return sb.ToString();
        }
    }
}
