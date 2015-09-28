using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Blogs.Helper
{

    public delegate bool Cre<T>(T t) where T : class,new();
    /// <summary>
    /// 多线程 文件写入
    /// </summary>
    public static class SafetyWriteHelper<T> where T : class,new()
    {


        /// <summary>
        /// 消息队列
        /// </summary>
        private static Queue<T> logQueue = new Queue<T>();
        /// <summary>
        /// 消息队列 对外只读
        /// </summary>
        public static Queue<T> LogQueue
        {
            get { return SafetyWriteHelper<T>.logQueue; }
        }

        /// <summary>
        /// 标志锁
        /// </summary>
        static string myLock = "true";

        /// <summary>
        /// 写入
        /// </summary>
        /// <param name="logmede"></param>
        public static void logWrite(T logmede, Cre<T> cre)
        {
            // 这里需要锁上 不然会出现：源数组长度不足。请检查 srcIndex 和长度以及数组的下限。异常   
            //网上有资料说 http://blog.csdn.net/greatbody/article/details/26135057  不能多线程同时写入队列
            //其实  不仅仅 不能同时写入队列 也不能同时读和写如队列  所以  在Dequeue 取的时候也要锁定一个对象
            lock (myLock)
                logQueue.Enqueue(logmede);
            logStartWrite(cre);
        }

        /// <summary>
        /// 写入集合
        /// </summary>
        /// <param name="logmede"></param>
        public static void logWrite(List<T> logmede, Cre<T> cre)
        {
            lock (myLock)
            {
                foreach (var item in logmede)
                {
                    logQueue.Enqueue(item);
                }
            }
            logStartWrite(cre);
        }

        /// <summary>
        /// 是否开始自动记录日志
        /// </summary>
        private static bool isStart = false;

        /// <summary>
        /// 开始把队列消息写入文件
        /// </summary>
        private static void logStartWrite(Cre<T> cre)
        {
            if (isStart)
                return;
            isStart = true;
            Thread t = new Thread(delegate()
            {
                while (true)
                {
                    if (SafetyWriteHelper<T>.logQueue.Count >= 1)
                    {
                        T m = null;
                        lock (myLock)
                            m = SafetyWriteHelper<T>.logQueue.Dequeue();
                        if (m == null)
                            continue;
                        cre(m);
                        //PanGuLuceneHelper.instance.CreateIndex(T);
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
