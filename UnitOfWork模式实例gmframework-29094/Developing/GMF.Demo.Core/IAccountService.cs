// 源文件头信息：
// <copyright file="IAccountService.cs">
// Copyright(c)2012-2013 GMFCN.All rights reserved.
// CLR版本：4.0.30319.239
// 开发组织：郭明锋@中国
// 公司网站：http://www.gmfcn.net
// 所属工程：GMF.Demo.Core
// 最后修改：郭明锋
// 最后修改：2013/05/27 23:06
// </copyright>

using System.Linq;

using GMF.Component.Tools;
using GMF.Demo.Core.Data.Repositories;
using GMF.Demo.Core.Models;
using GMF.Demo.Core.Models.Account;
using GMF.Demo.Core.Models.Security;


namespace GMF.Demo.Core
{
    /// <summary>
    ///     账户模块核心业务契约
    /// </summary>
    public interface IAccountContract
    {
        #region 属性

        /// <summary>
        /// 获取 用户信息查询数据集
        /// </summary>
        IQueryable<Member> Members { get; }

        /// <summary>
        /// 获取 用户扩展信息查询数据集
        /// </summary>
        IQueryable<MemberExtend> MemberExtends { get; }

        /// <summary>
        /// 获取 登录记录信息查询数据集
        /// </summary>
        IQueryable<LoginLog> LoginLogs { get; }

        /// <summary>
        /// 获取 角色信息查询数据集
        /// </summary>
        IQueryable<Role> Roles { get; }

        #endregion

        #region 公共方法

        /// <summary>
        ///     用户登录
        /// </summary>
        /// <param name="loginInfo">登录信息</param>
        /// <returns>业务操作结果</returns>
        OperationResult Login(LoginInfo loginInfo);

        #endregion
    }
}