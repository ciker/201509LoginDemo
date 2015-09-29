using System;
using System.Collections.Generic;
using System.Text;
#region 命名空间
using System.Web;
using System.Threading;
using System.Collections;
using WebChatSDK;
#endregion

namespace ServerPushHttpHandler
{
    public class ServerPushResult : IAsyncResult
    {
        #region 全局变量
        HttpContext m_Context;
        AsyncCallback m_Callback;
        object m_ExtraData;
        bool m_IsCompleted = false;
        public string Result { get; set; }
        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="context"></param>
        /// <param name="cb"></param>
        /// <param name="extraData"></param>
        public ServerPushResult(HttpContext context, AsyncCallback cb, object extraData)
        {
            m_Context = context;
            m_Callback = cb;
            m_ExtraData = extraData;
        }
        #endregion

        #region 向客户端发送消息
        /// <summary>
        /// 向客户端响应消息
        /// </summary>
        /// <param name="result">结果信息</param>
        public void Send()
        {
            try
            {
                m_Context.Response.Write(Result);
                if (m_Callback != null)
                {
                    m_Callback(this);
                }
            }
            catch { }
            finally
            {
                m_IsCompleted = true;
            }
        }
        #endregion

        #region IAsyncResult 成员
        //获取用户定义的对象，它限定或包含关于异步操作的信息。
        public object AsyncState
        {
            get { return null; }
        }

        //获取用于等待异步操作完成的 WaitHandle。
        public WaitHandle AsyncWaitHandle
        {
            get { return null; }
        }

        //获取异步操作是否同步完成的指示。
        public bool CompletedSynchronously
        {
            get { return false; }
        }

        //获取异步操作是否已完成的指示。
        public bool IsCompleted
        {
            get { return m_IsCompleted; }
        }
        #endregion
    }
}
