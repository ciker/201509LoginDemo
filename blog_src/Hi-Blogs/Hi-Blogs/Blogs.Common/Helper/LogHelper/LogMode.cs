using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;

namespace Blogs.Helper.LogHelper
{
    /// <summary>
    /// 日志模型
    /// </summary>
    public class LogModel
    {
        /// <summary>
        /// 日志要存的路径 默认路径：网站根目录 + Log 文件夹
        /// 在程序第一次启动是设置
        /// </summary>       
        public static string _logFilePath;// = HttpContext.Current.Server.MapPath("~/") + @"\Log\";

        public string logFilePath
        {
            get { return _logFilePath; }
        }

        private string _logFileName;

        /// <summary>
        /// 日志文件名字
        /// </summary>
        public string logFileName
        {
            get { return _logFileName + "_" + DateTime.Now.ToString("yyyyMMdd"); }
            set { _logFileName = value; }
        }

        private string _logMessg;

        /// <summary>
        /// 日志内容
        /// </summary>
        public string logMessg
        {
            get
            {
                return "----begin-------" + DateTime.Now.ToString() + "----Queue.Count：" + LogHelper.LogQueue.Count + "-----------------------------------\r\n\r\n"
                    + _logMessg
                    + "\r\n\r\n----end----------" + DateTime.Now.ToString() + "----Queue.Count：" + LogHelper.LogQueue.Count + "-----------------------------------"
                    + "\r\n\r\n\r\n";
            }
            set { _logMessg = value; }
        }
    }
}
