// 源文件头信息：
// <copyright file="MemberExtend.cs">
// Copyright(c)2012-2013 GMFCN.All rights reserved.
// CLR版本：4.0.30319.239
// 开发组织：郭明锋@中国
// 公司网站：http://www.gmfcn.net
// 所属工程：GMF.Demo.Core.Models
// 最后修改：郭明锋
// 最后修改：2013/05/20 13:43
// </copyright>

using System;
using System.ComponentModel;

using GMF.Component.Tools;


namespace GMF.Demo.Core.Models.Account
{
    /// <summary>
    ///     实体类——用户扩展信息
    /// </summary>
    [Description("用户扩展信息")]
    public class MemberExtend : EntityBase<Guid>
    {
        /// <summary>
        /// 初始化一个 用户扩展实体类 的新实例
        /// </summary>
        public MemberExtend()
        {
            Id = CombHelper.NewComb();
        }

        public string Tel { get; set; }

        public MemberAddress Address { get; set; }

        public virtual Member Member { get; set; }
    }
}