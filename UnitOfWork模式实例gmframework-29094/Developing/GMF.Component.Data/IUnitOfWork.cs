// 源文件头信息：
// <copyright file="IUnitOfWork.cs">
// Copyright(c)2012-2013 GMFCN.All rights reserved.
// CLR版本：4.0.30319.239
// 开发组织：郭明锋@中国
// 公司网站：http://www.gmfcn.net
// 所属工程：GMF.Component.Data
// 最后修改：郭明锋
// 最后修改：2013/05/22 23:13
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace GMF.Component.Data
{
    /// <summary>
    ///     业务单元操作接口
    /// </summary>
    public interface IUnitOfWork
    {
        #region 属性

        /// <summary>
        ///     获取 当前单元操作是否已被提交
        /// </summary>
        bool IsCommitted { get; }

        #endregion

        #region 方法

        /// <summary>
        ///     提交当前单元操作的结果
        /// </summary>
        /// <param name="validateOnSaveEnabled">保存时是否自动验证跟踪实体</param>
        /// <returns></returns>
        int Commit(bool validateOnSaveEnabled = true);

        /// <summary>
        ///     把当前单元操作回滚成未提交状态
        /// </summary>
        void Rollback();

        #endregion
    }
}