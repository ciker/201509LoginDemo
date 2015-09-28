using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Blogs.Common.Helper
{
    public class MyHtmlHelper
    {

        #region MyRegion
        /// <summary>
        /// 获取get请求返回的数据
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetRequest(string url)
        {
            HttpClient httpclient = new HttpClient();
            HttpResponseMessage response = httpclient.GetAsync(new Uri(url)).Result;
            return response.Content.ReadAsStringAsync().Result;
        }

        /// <summary>
        ///获取 Post 请求 的返回数据
        /// </summary>
        /// <param name="url"></param>
        /// <param name="str_content"></param>
        /// <returns></returns>
        public static string PostReqest(string url, string str_content = "")
        {
            HttpClient httpclient = new HttpClient();
            try
            {
                StringContent fromurlcontent = new StringContent(str_content);
                fromurlcontent.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
                HttpResponseMessage response = httpclient.PostAsync(new Uri(url), fromurlcontent).Result;
                return response.Content.ReadAsStringAsync().Result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 返回Html字符中的Text 类似jquery中.text()方法
        /// </summary>
        /// <param name="str"></param>
        /// <param name="length">要截取的长度</param>
        /// <returns></returns>
        public static string GetHtmlText(string str, int length = 0)
        {
            if (string.IsNullOrEmpty(str))
                return string.Empty;
            var docment = new HtmlAgilityPack.HtmlDocument();
            docment.LoadHtml(str);
            if (0 == length)
            {
                return docment.DocumentNode.InnerText.Replace("&amp;nbsp;", "").Replace("&nbsp;", "");
            }
            else
            {
                var innertext = docment.DocumentNode.InnerText.Replace("&amp;nbsp;", "").Replace("&nbsp;", "");
                var strcon = innertext.Length >= innertext.Length ? innertext.Substring(0, length) : innertext;
                return strcon;
            }
        }
        #endregion
    }
}
