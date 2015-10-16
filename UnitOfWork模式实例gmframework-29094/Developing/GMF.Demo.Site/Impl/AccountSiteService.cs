// 源文件头信息：
// <copyright file="AccountSiteService.cs">
// Copyright(c)2012-2013 GMFCN.All rights reserved.
// CLR版本：4.0.30319.239
// 开发组织：郭明锋@中国
// 公司网站：http://www.gmfcn.net
// 所属工程：GMF.Demo.Site
// 最后修改：郭明锋
// 最后修改：2013/05/20 13:06
// </copyright>

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;

using GMF.Component.Tools;
using GMF.Demo.Core.Impl;
using GMF.Demo.Core.Models;
using GMF.Demo.Core.Models.Account;
using GMF.Demo.Site.Models;


namespace GMF.Demo.Site.Impl
{
    /// <summary>
    ///     账户模块站点业务实现
    /// </summary>
    [Export(typeof(IAccountSiteContract))]
    internal class AccountSiteService : AccountService, IAccountSiteContract
    {
        /// <summary>
        ///     用户登录
        /// </summary>
        /// <param name="model">登录模型信息</param>
        /// <returns>业务操作结果</returns>
        public OperationResult Login(LoginModel model)
        {
            PublicHelper.CheckArgument(model, "model");
            LoginInfo loginInfo = new LoginInfo
            {
                Account = model.Account,
                Password = model.Password,
                IpAddress = HttpContext.Current.Request.UserHostAddress
            };
            OperationResult result = base.Login(loginInfo);
            if (result.ResultType == OperationResultType.Success)
            {
                Member member = (Member)result.AppendData;
                DateTime expiration = model.IsRememberLogin
                    ? DateTime.Now.AddDays(7)
                    : DateTime.Now.Add(FormsAuthentication.Timeout);
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, member.UserName, DateTime.Now, expiration,
                    true, member.NickName, FormsAuthentication.FormsCookiePath);
                HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket));
                if (model.IsRememberLogin)
                {
                    cookie.Expires = DateTime.Now.AddDays(7);
                }
                HttpContext.Current.Response.Cookies.Set(cookie);
                result.AppendData = null;
            }
            return result;
        }

        /// <summary>
        ///     用户退出
        /// </summary>
        public void Logout()
        {
            FormsAuthentication.SignOut();
        }
    }
}