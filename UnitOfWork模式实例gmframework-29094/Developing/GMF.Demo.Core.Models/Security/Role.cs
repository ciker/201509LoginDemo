// 源文件头信息：
// <copyright file="Role.cs">
// Copyright(c)2012-2013 GMFCN.All rights reserved.
// CLR版本：4.0.30319.239
// 开发组织：郭明锋@中国
// 公司网站：http://www.gmfcn.net
// 所属工程：GMF.Demo.Core.Models
// 最后修改：郭明锋
// 最后修改：2013/05/21 15:26
// </copyright>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using GMF.Component.Tools;
using GMF.Demo.Core.Models.Account;


namespace GMF.Demo.Core.Models.Security
{
    /// <summary>
    ///     实体类——角色信息
    /// </summary>
    [Description("角色信息")]
    public class Role : EntityBase<Guid>
    {
        public Role()
        {
            Id = CombHelper.NewComb();
        }

        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        [StringLength(100)]
        public string Description { get; set; }

        /// <summary>
        /// 获取或设置 角色类型
        /// </summary>
        public RoleType RoleType
        {
            get { return (RoleType)RoleTypeNum; }
            set { RoleTypeNum = (int)value; }
        }

        /// <summary>
        /// 获取或设置 角色类型的数值表示，用于数据库存储
        /// </summary>
        public int RoleTypeNum { get; set; }

        /// <summary>
        ///     获取或设置 拥有此角色的用户信息集合
        /// </summary>
        public virtual ICollection<Member> Members { get; set; }
    }
}
