// 源文件头信息：
// <copyright file="IEntityMapper.cs">
// Copyright(c)2012-2013 GMFCN.All rights reserved.
// CLR版本：4.0.30319.239
// 开发组织：郭明锋@中国
// 公司网站：http://www.gmfcn.net
// 所属工程：GMF.Component.Data
// 最后修改：郭明锋
// 最后修改：2013/06/14 22:00
// </copyright>

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Linq;
using System.Text;


namespace GMF.Component.Data
{
    /// <summary>
    ///     实体映射接口
    /// </summary>
    [InheritedExport]
    public interface IEntityMapper
    {
        /// <summary>
        ///     将当前实体映射对象注册到当前数据访问上下文实体映射配置注册器中
        /// </summary>
        /// <param name="configurations">实体映射配置注册器</param>
        void RegistTo(ConfigurationRegistrar configurations);
    }
}