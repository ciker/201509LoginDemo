﻿// 源文件头信息：
// <copyright file="ExceptionMessage.cs">
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
    ///     异常信息封装类
    /// </summary>
    public class ExceptionMessage
    {
        #region 字段

        #endregion

        #region 构造函数

        /// <summary>
        ///     以自定义用户信息和异常对象实例化一个异常信息对象
        /// </summary>
        /// <param name="e"> 异常对象 </param>
        /// <param name="userMessage"> 自定义用户信息 </param>
        /// <param name="isHideStackTrace"> 是否隐藏异常堆栈信息 </param>
        public ExceptionMessage(Exception e, string userMessage = null, bool isHideStackTrace = false)
        {
            UserMessage = string.IsNullOrEmpty(userMessage) ? e.Message : userMessage;

            StringBuilder sb = new StringBuilder();
            ExMessage = string.Empty;
            int count = 0;
            string appString = "";
            while (e != null)
            {
                if (count > 0)
                {
                    appString += "　";
                }
                ExMessage = e.Message;
                sb.AppendLine(appString + "异常消息：" + e.Message);
                sb.AppendLine(appString + "异常类型：" + e.GetType().FullName);
                sb.AppendLine(appString + "异常方法：" + (e.TargetSite == null ? null : e.TargetSite.Name));
                sb.AppendLine(appString + "异常源：" + e.Source);
                if (!isHideStackTrace && e.StackTrace != null)
                {
                    sb.AppendLine(appString + "异常堆栈：" + e.StackTrace);
                }
                if (e.InnerException != null)
                {
                    sb.AppendLine(appString + "内部异常：");
                    count++;
                }
                e = e.InnerException;
            }
            ErrorDetails = sb.ToString();
            sb.Clear();
        }

        #region 属性

        /// <summary>
        ///     用户信息，用于报告给用户的异常消息
        /// </summary>
        public string UserMessage { get; set; }

        /// <summary>
        ///     直接的Exception异常信息，即e.Message属性值
        /// </summary>
        public string ExMessage { get; private set; }

        /// <summary>
        ///     异常输出的详细描述，包含异常消息，规模信息，异常类型，异常源，引发异常的方法及内部异常信息
        /// </summary>
        public string ErrorDetails { get; private set; }

        #endregion

        #endregion
    }
}