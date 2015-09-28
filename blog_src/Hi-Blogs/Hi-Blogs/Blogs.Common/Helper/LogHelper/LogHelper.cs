using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Blogs.Helper.LogHelper
{
    /// <summary>
    /// 日志操作辅助类
    /// zhaopeiym@163.com
    /// 创建20150104 修改20150104
    /// </summary>
    public class LogHelper
    {
        /// <summary>
        /// 消息队列
        /// </summary>
        private static Queue<LogModel> logQueue = new Queue<LogModel>();
        /// <summary>
        /// 消息队列 对外只读
        /// </summary>
        public static Queue<LogModel> LogQueue
        {
            get { return LogHelper.logQueue; }
        }

        /// <summary>
        /// 标志锁
        /// </summary>
        static string myLock = "true";

        /// <summary>
        /// 写入日志文件（异步单线程 记录日志）
        /// </summary>
        /// <param name="logmede"></param>
        public static void logWrite(LogModel logmede)
        {
            // 这里需要锁上 不然会出现：源数组长度不足。请检查 srcIndex 和长度以及数组的下限。异常   
            //网上有资料说 http://blog.csdn.net/greatbody/article/details/26135057  不能多线程同时写入队列
            //其实  不仅仅 不能同时写入队列 也不能同时读和写如队列  所以  在Dequeue 取的时候也要锁定一个对象
            lock (myLock)
                logQueue.Enqueue(logmede);
            logStartWrite();
        }

        /// <summary>
        /// 部分日志文件大小
        /// </summary>
        public static int SectionlogFileSize = 1024 * 1024 * 1; // 1024Byte * 1024KB * 1MB

        /// <summary>
        /// 变动文件大小
        /// </summary>
        public static int fileSize = 1024 * 1024 * 4;

        /// <summary>
        /// 文件编码格式
        /// </summary>
        public static Encoding encoding = Encoding.Default;

        /// <summary>
        /// 是否开始自动记录日志
        /// </summary>
        private static bool isStart = false;

        /// <summary>
        /// 用来 标识 最好一次 检测是否 需要 清理 日志文件 时间
        /// </summary>
        private static DateTime time = DateTime.MinValue;

        /// <summary>
        /// 间隔检测时间（一天）
        /// </summary>
        private static int TestingInterval = 1;

        /// <summary>
        /// 删除 天之前的的 日志
        /// </summary>
        private static int DelInterval = 60;


        /// <summary>
        /// 开始把队列消息写入文件
        /// </summary>
        private static void logStartWrite()
        {
            if (isStart)
                return;
            isStart = true;
            Thread t = new Thread(delegate()
            {
                while (true)
                {
                    #region 检测 并删除 之前之外的 日志文件
                    if (time.AddDays(TestingInterval) <= DateTime.Now)// 时间内 检测一次
                    {
                        time = DateTime.Now;
                        DirectoryInfo dir = new DirectoryInfo(LogModel._logFilePath);
                        FileHelper.CreatePath(LogModel._logFilePath);
                        var dirs = dir.GetDirectories();
                        foreach (var dirinfo in dirs)
                            if (dirinfo.CreationTime.AddDays(DelInterval) <= DateTime.Now)//删除 设定时间 之前的日志
                                Directory.Delete(dirinfo.FullName, true);
                    }
                    #endregion

                    if (LogHelper.logQueue.Count >= 1)
                    {
                        LogModel m = null;
                        lock (myLock)
                            m = LogHelper.logQueue.Dequeue();
                        if (m == null)
                            continue;
                        if (!Directory.Exists(m.logFilePath + m.logFileName + @"\"))
                            Directory.CreateDirectory(m.logFilePath + m.logFileName + @"\");

                        int i = 0;
                        //部分 日志 文件路径
                        string SectionfileFullName = m.logFilePath + m.logFileName + @"\" + m.logFileName + "_" + i.ToString("000") + ".txt";
                        //最新的写了内容的 部分 日志文件路径
                        string TopSectionfileFullName = SectionfileFullName;
                        // 需要实时更新的 最新日志文件 路径
                        string LogfileFullNqme = m.logFilePath + m.logFileName + @"\" + m.logFileName + ".txt";

                        FileInfo file = new FileInfo(SectionfileFullName);
                        while (file.Exists && file.Length >= LogHelper.SectionlogFileSize)
                        {
                            TopSectionfileFullName = SectionfileFullName;
                            i++;
                            SectionfileFullName = m.logFilePath + m.logFileName + @"\" + m.logFileName + "_" + i.ToString("000") + ".txt";
                            file = new FileInfo(SectionfileFullName);
                        }

                        try
                        {
                            if (!file.Exists)//如果不存在 这个文件 就说明需要 创建新的部分日志文件了
                            {
                                //因为SectionfileFullName路径的文件不存在    所以创建
                                File.WriteAllText(SectionfileFullName, m.logMessg, encoding);

                                FileInfo Logfile = new FileInfo(LogfileFullNqme);
                                if (Logfile.Exists && Logfile.Length >= LogHelper.fileSize)
                                    //先清空  然后加上 上一个部分文件的内容
                                    File.WriteAllText(LogfileFullNqme, File.ReadAllText(TopSectionfileFullName, encoding), encoding);//如果存在则覆盖                           
                            }
                            else
                                File.AppendAllText(SectionfileFullName, m.logMessg, encoding);//累加

                            //追加这次内容 到动态更新的日志文件
                            File.AppendAllText(LogfileFullNqme, m.logMessg, encoding);
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }

                    }
                    else
                    {
                        isStart = false;//标记下次可执行
                        break;//跳出循环
                    }
                }
            });
            t.Start();
        }
    }
}
