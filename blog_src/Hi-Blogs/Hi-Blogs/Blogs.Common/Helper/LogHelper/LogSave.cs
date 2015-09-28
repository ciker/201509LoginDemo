using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Blogs.Helper.LogHelper
{
    /// <summary>
    /// 异步单线程
    /// </summary>
    public class LogSave
    {
        /// <summary>
        /// 获得Exception 的详细信息
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public static string GetExceptionInfo(Exception ex)
        {
            StringBuilder str = new StringBuilder();
            str.Append("错误信息：" + ex.Message);
            str.Append("\r\n错误源：" + ex.Source);
            str.Append("\r\n异常方法：" + ex.TargetSite);
            str.Append("\r\n堆栈信息：" + ex.StackTrace);
            return str.ToString();
        }

        /// <summary>
        /// 系统 自动 捕捉异常
        /// 保存异常详细信息 
        /// 包括： 浏览器  浏览器版本 操作系统 页面  Exception
        /// </summary>
        /// <param name="ex"></param>
        public static void SysErrLogSave(Exception ex, string fileName = null)
        {
            StringBuilder str = new StringBuilder();
            string ip = "";
            if (HttpContext.Current.Request.ServerVariables.Get("HTTP_X_FORWARDED_FOR") != null)
                ip = HttpContext.Current.Request.ServerVariables.Get("HTTP_X_FORWARDED_FOR").ToString().Trim();
            else
                ip = HttpContext.Current.Request.ServerVariables.Get("Remote_Addr").ToString().Trim();
            str.Append("Ip:" + ip);
            str.Append("\r\n浏览器:" + HttpContext.Current.Request.Browser.Browser.ToString());
            str.Append("\r\n浏览器版本:" + HttpContext.Current.Request.Browser.MajorVersion.ToString());
            str.Append("\r\n操作系统:" + HttpContext.Current.Request.Browser.Platform.ToString());
            str.Append("\r\n页面：" + HttpContext.Current.Request.Url.ToString());
            str.Append("\r\n" + GetExceptionInfo(ex));
            LogHelper.logWrite(new LogModel()
            {
                logFileName = "SysErr" + fileName ?? string.Empty,
                logMessg = str.ToString()
            });
        }

        /// <summary>
        /// 异常日志记录
        /// </summary>
        /// <param name="strmes"></param>
        /// <param name="ex"></param>
        public static void ErrLogSave(string strmes, Exception ex, string fileName = null)
        {
            StringBuilder str = new StringBuilder();
            str.Append(strmes);
            if (ex != null)
                str.Append("\r\n" + GetExceptionInfo(ex));
            LogHelper.logWrite(new LogModel()
            {
                logFileName = fileName ?? "Err",
                logMessg = str.ToString()
            });
        }

        /// <summary>
        /// 警告日志记录
        /// </summary>
        /// <param name="str"></param>
        public static void WarnLogSave(string str, string fileName = null)
        {
            if (!string.IsNullOrEmpty(str.Trim()))
                LogHelper.logWrite(new LogModel()
                {
                    logFileName = fileName ?? "Warn",
                    logMessg = str
                });
        }

        /// <summary>
        /// 追踪日志记录
        /// </summary>
        /// <param name="str"></param>
        public static void TrackLogSave(string str, string fileName = null)
        {
            if (!string.IsNullOrEmpty(str.Trim()))
                LogHelper.logWrite(new LogModel()
                {
                    logFileName = fileName ?? "Track",
                    logMessg = str
                });
        }

        public static void TrackLogSave(string str)
        {
            if (!string.IsNullOrEmpty(str.Trim()))
                LogHelper.logWrite(new LogModel()
                {
                    logFileName = "SqlTrack",
                    logMessg = str
                });
        }
    }
}
