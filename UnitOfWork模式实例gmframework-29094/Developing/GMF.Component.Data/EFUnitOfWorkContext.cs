// 源文件头信息：
// <copyright file="EFRepositoryContext.cs">
// Copyright(c)2012-2013 GMFCN.All rights reserved.
// CLR版本：4.0.30319.239
// 开发组织：郭明锋@中国
// 公司网站：http://www.gmfcn.net
// 所属工程：GMF.Component.Data
// 最后修改：郭明锋
// 最后修改：2013/06/14 23:06
// </copyright>

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;

using GMF.Component.Tools;


namespace GMF.Component.Data
{
    /// <summary>
    ///     数据单元操作类
    /// </summary>
    [Export(typeof(IUnitOfWork))]
    public class EFUnitOfWorkContext : UnitOfWorkContextBase
    {
        /// <summary>
        ///     获取 当前使用的数据访问上下文对象
        /// </summary>
        protected override DbContext Context
        {
            get
            {
                bool secondCachingEnabled = ConfigurationManager.AppSettings["EntityFrameworkCachingEnabled"].CastTo(false);
                return secondCachingEnabled ? EFCachingDbContext.Value : EFDbContext.Value;
            }
        }

        [Import("EF", typeof (DbContext))]
        private Lazy<EFDbContext> EFDbContext { get; set; }

        [Import("EFCaching", typeof(DbContext))]
        private Lazy<EFCachingDbContext> EFCachingDbContext { get; set; }
    }
}