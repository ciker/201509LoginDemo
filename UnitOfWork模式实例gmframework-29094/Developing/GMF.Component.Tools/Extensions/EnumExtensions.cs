// 源文件头信息：
// <copyright file="EnumExtensions.cs">
// Copyright(c)2012-2013 GMFCN.All rights reserved.
// CLR版本：4.0.30319.239
// 开发组织：郭明锋@中国
// 公司网站：http://www.gmfcn.net
// 所属工程：GMF.Component.Tools
// 最后修改：郭明锋
// 最后修改：2013/05/15 0:51
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;


namespace GMF.Component.Tools
{
    /// <summary>
    ///     枚举扩展方法类
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        ///     获取枚举项的Description特性的描述文字
        /// </summary>
        /// <param name="enumeration"> </param>
        /// <returns> </returns>
        public static string ToDescription(this Enum enumeration)
        {
            Type type = enumeration.GetType();
            MemberInfo[] members = type.GetMember(enumeration.CastTo<string>());
            if (members.Length > 0)
            {
                return members[0].ToDescription();
            }
            return enumeration.CastTo<string>();
        }
    }
}