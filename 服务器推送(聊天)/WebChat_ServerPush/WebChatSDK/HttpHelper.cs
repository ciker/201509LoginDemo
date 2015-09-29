using System;
using System.Collections.Generic;
using System.Text;
#region 命名空间
using System.Net;
using System.Collections.Specialized;
using System.IO;
using System.IO.Compression;
#endregion

namespace WebChatSDK
{
    /// <summary>
    /// 访问模式
    /// </summary>
    public enum Method
    {
        /// <summary>
        ///  POST
        /// </summary>
        POST,
        /// <summary>
        /// GET
        /// </summary>
        GET
    }

    public class HttpHelper
    {
        /// <summary>
        /// HttpHelper
        /// </summary>
        public HttpHelper()
        {
            Url = string.Empty;
            PostData = string.Empty;
            Referer = string.Empty;
            Method = Method.GET;
            Html = string.Empty;
            ErrMsg = string.Empty;
            Encoding = Encoding.GetEncoding("UTF-8");
            ContentType = "application/x-www-form-urlencoded";
            UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US; rv:1.9.2.10) Gecko/20100914 Firefox/3.6.10 (.NET CLR 3.5.30729)";
            Accept = "*/*";

        }

        public static void SetHeaderValue(WebHeaderCollection header, string name, string value)
        {
            var property = typeof(WebHeaderCollection).GetProperty("InnerCollection",
                System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            if (property != null)
            {
                var collection = property.GetValue(header, null) as NameValueCollection;
                collection[name] = value;
            }
        }

        public bool Do()
        {
            HttpWebRequest httpWebRequest = null;
            HttpWebResponse httpWebResponse = null;
            try
            {
                httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(Url);
                if (!string.IsNullOrEmpty(Referer))
                {
                    httpWebRequest.Referer = Referer;
                }
                httpWebRequest.Accept = Accept;
                if (!string.IsNullOrEmpty(AcceptLanguage))
                {
                    httpWebRequest.Headers[HttpRequestHeader.AcceptLanguage] = AcceptLanguage;
                }
                if (!string.IsNullOrEmpty(AcceptEncoding))
                {
                    httpWebRequest.Headers[HttpRequestHeader.AcceptEncoding] = AcceptEncoding;
                }
                if (KeepAlive)
                {
                    SetHeaderValue(httpWebRequest.Headers, "Connection", "Keep-Alive");
                }
                //httpWebRequest.KeepAlive = KeepAlive;
                httpWebRequest.UserAgent = UserAgent;
                httpWebRequest.Method = Enum.GetName(typeof(Method), Method);
                httpWebRequest.ServicePoint.Expect100Continue = false;
                httpWebRequest.AllowAutoRedirect = AllowAutoRedirect;
                if (Method == Method.POST && !string.IsNullOrEmpty(PostData))
                {
                    httpWebRequest.ContentType = ContentType;
                    byte[] byteRequest = Encoding.GetBytes(PostData);
                    httpWebRequest.ContentLength = byteRequest.Length;
                    Stream stream = httpWebRequest.GetRequestStream();
                    stream.Write(byteRequest, 0, byteRequest.Length);
                    stream.Close();
                }
                if (Method == Method.POST && this.PostDataByte.Count > 0)
                {
                    httpWebRequest.ContentType = ContentType;
                    Stream stream = httpWebRequest.GetRequestStream();
                    foreach (byte b in this.PostDataByte)
                    {
                        stream.WriteByte(b);
                    }
                    stream.Close();
                }

                httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                Headers = httpWebResponse.Headers;
                StreamReader streamReader = null;
                if (httpWebResponse.ContentEncoding != null && httpWebResponse.ContentEncoding.Equals("gzip", StringComparison.InvariantCultureIgnoreCase))
                {
                    using (streamReader = new StreamReader(new GZipStream(httpWebResponse.GetResponseStream(), CompressionMode.Decompress), Encoding))
                    {
                        Html = streamReader.ReadToEnd();
                    }
                }
                else
                {
                    using (streamReader = new StreamReader(httpWebResponse.GetResponseStream(), Encoding))
                    {
                        Html = streamReader.ReadToEnd();
                    }
                }
                StatusCode = httpWebResponse.StatusCode;
                return true;
            }
            catch (Exception e)
            {
                ErrMsg = e.Message;
                return false;
            }
            finally
            {
                if (httpWebRequest != null)
                {
                    httpWebRequest.Abort();
                }
                if (httpWebResponse != null)
                {
                    httpWebResponse.Close();
                }
            }
        }

        /// <summary>
        /// 状态码
        /// </summary>
        public HttpStatusCode StatusCode { get; private set; }

        /// <summary>
        /// Headers
        /// </summary>
        public WebHeaderCollection Headers { get; private set; }

        /// <summary>
        /// 是否跳转后的页面
        /// </summary>
        public bool AllowAutoRedirect { get; set; }

        /// <summary>
        /// Url
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Referer
        /// </summary>
        public string Referer { get; set; }

        /// <summary>
        /// Accept
        /// </summary>
        public string Accept { get; set; }

        /// <summary>
        /// AcceptEncoding 'gzip, deflate
        /// </summary>
        public string AcceptEncoding { get; set; }

        /// <summary>
        /// AcceptLanguage  'zh-CN
        /// </summary>
        public string AcceptLanguage { get; set; }

        /// <summary>
        /// ContentType
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        /// KeepAlive
        /// </summary>
        public bool KeepAlive { get; set; }

        /// <summary>
        /// UserAgent
        /// </summary>
        public string UserAgent { get; set; }

        /// <summary>
        /// 发送数据
        /// </summary>
        public string PostData { get; set; }

        /// <summary>
        /// 发送数据
        /// </summary>
        public List<byte> PostDataByte { get; set; }

        /// <summary>
        /// 模式
        /// </summary>
        public Method Method { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public Encoding Encoding { get; set; }

        /// <summary>
        /// 获取错误
        /// </summary>
        public string ErrMsg { get; set; }

        /// <summary>
        /// 获取结果
        /// </summary>
        public string Html { get; set; }
    }
}
