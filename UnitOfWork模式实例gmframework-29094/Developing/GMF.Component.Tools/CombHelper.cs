﻿// 源文件头信息：
// <copyright file="CombHelper.cs">
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
    ///     COMB（有序的Guid，Guid与时间混合型）类型操作类
    /// </summary>
    public static class CombHelper
    {
        /// <summary>
        ///     返回Guid用于数据库操作，特定的时间代码可以提高检索效率
        /// </summary>
        /// <returns>COMB类型 Guid 数据</returns>
        public static Guid NewComb()
        {
            byte[] guidArray = Guid.NewGuid().ToByteArray();
            DateTime dtBase = new DateTime(1900, 1, 1);
            DateTime dtNow = DateTime.Now;
            //获取用于生成byte字符串的天数与毫秒数
            TimeSpan days = new TimeSpan(dtNow.Ticks - dtBase.Ticks);
            TimeSpan msecs = new TimeSpan(dtNow.Ticks - (new DateTime(dtNow.Year, dtNow.Month, dtNow.Day).Ticks));
            //转换成byte数组
            //注意SqlServer的时间计数只能精确到1/300秒
            byte[] daysArray = BitConverter.GetBytes(days.Days);
            byte[] msecsArray = BitConverter.GetBytes((long)(msecs.TotalMilliseconds / 3.333333));

            //反转字节以符合SqlServer的排序
            Array.Reverse(daysArray);
            Array.Reverse(msecsArray);

            //把字节复制到Guid中
            Array.Copy(daysArray, daysArray.Length - 2, guidArray, guidArray.Length - 6, 2);
            Array.Copy(msecsArray, msecsArray.Length - 4, guidArray, guidArray.Length - 4, 4);
            return new Guid(guidArray);
        }

        /// <summary>
        ///     从SQL Server 返回的Guid中生成时间信息
        /// </summary>
        public static DateTime GetDateFromComb(Guid id)
        {
            DateTime baseDate = new DateTime(1900, 1, 1);
            byte[] daysArray = new byte[4];
            byte[] msecsArray = new byte[4];
            byte[] guidArray = id.ToByteArray();

            // Copy the date parts of the guid to the respective byte arrays. 
            Array.Copy(guidArray, guidArray.Length - 6, daysArray, 2, 2);
            Array.Copy(guidArray, guidArray.Length - 4, msecsArray, 0, 4);

            // Reverse the arrays to put them into the appropriate order 
            Array.Reverse(daysArray);
            Array.Reverse(msecsArray);

            // Convert the bytes to ints 
            int days = BitConverter.ToInt32(daysArray, 0);
            int msecs = BitConverter.ToInt32(msecsArray, 0);

            DateTime date = baseDate.AddDays(days);
            date = date.AddMilliseconds(msecs * 3.333333);

            return date;
        }
    }
}