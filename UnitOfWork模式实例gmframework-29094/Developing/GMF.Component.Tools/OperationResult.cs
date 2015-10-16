// 源文件头信息：
// <copyright file="OperationResult.cs">
// Copyright(c)2012-2013 GMFCN.All rights reserved.
// CLR版本：4.0.30319.239
// 开发组织：郭明锋@中国
// 公司网站：http://www.gmfcn.net
// 所属工程：GMF.Component.Tools
// 最后修改：郭明锋
// 最后修改：2013/05/14 23:04
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace GMF.Component.Tools
{
    /// <summary>
    ///     业务操作结果信息类，对操作结果进行封装
    /// </summary>
    public class OperationResult
    {
        #region 构造函数

        /// <summary>
        ///     初始化一个 业务操作结果信息类 的新实例
        /// </summary>
        /// <param name="resultType">业务操作结果类型</param>
        public OperationResult(OperationResultType resultType)
        {
            ResultType = resultType;
        }

        /// <summary>
        ///     初始化一个 定义返回消息的业务操作结果信息类 的新实例
        /// </summary>
        /// <param name="resultType">业务操作结果类型</param>
        /// <param name="message">业务返回消息</param>
        public OperationResult(OperationResultType resultType, string message)
            : this(resultType)
        {
            Message = message;
        }

        /// <summary>
        ///     初始化一个 定义返回消息与附加数据的业务操作结果信息类 的新实例
        /// </summary>
        /// <param name="resultType">业务操作结果类型</param>
        /// <param name="message">业务返回消息</param>
        /// <param name="appendData">业务返回数据</param>
        public OperationResult(OperationResultType resultType, string message, object appendData)
            : this(resultType, message)
        {
            AppendData = appendData;
        }

        /// <summary>
        ///     初始化一个 定义返回消息与日志消息的业务操作结果信息类 的新实例
        /// </summary>
        /// <param name="resultType">业务操作结果类型</param>
        /// <param name="message">业务返回消息</param>
        /// <param name="logMessage">业务日志记录消息</param>
        public OperationResult(OperationResultType resultType, string message, string logMessage)
            : this(resultType, message)
        {
            LogMessage = logMessage;
        }

        /// <summary>
        ///     初始化一个 定义返回消息、日志消息与附加数据的业务操作结果信息类 的新实例
        /// </summary>
        /// <param name="resultType">业务操作结果类型</param>
        /// <param name="message">业务返回消息</param>
        /// <param name="logMessage">业务日志记录消息</param>
        /// <param name="appendData">业务返回数据</param>
        public OperationResult(OperationResultType resultType, string message, string logMessage, object appendData)
            : this(resultType, message, logMessage)
        {
            AppendData = appendData;
        }

        #endregion

        #region 属性

        /// <summary>
        ///     获取或设置 操作结果类型
        /// </summary>
        public OperationResultType ResultType { get; set; }

        /// <summary>
        ///     获取或设置 操作返回信息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        ///     获取或设置 操作返回的日志消息，用于记录日志
        /// </summary>
        public string LogMessage { get; set; }

        /// <summary>
        ///     获取或设置 操作结果附加信息
        /// </summary>
        public object AppendData { get; set; }

        #endregion
    }
}