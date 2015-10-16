// 源文件头信息：
// <copyright file="AccountController.cs">
// Copyright(c)2012-2013 GMFCN.All rights reserved.
// CLR版本：4.0.30319.239
// 开发组织：郭明锋@中国
// 公司网站：http://www.gmfcn.net
// 所属工程：GMF.Demo.Site.Web
// 最后修改：郭明锋
// 最后修改：2013/05/15 0:41
// </copyright>

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using GMF.Component.Tools;
using GMF.Demo.Site.Helper.Logging;
using GMF.Demo.Site.Impl;
using GMF.Demo.Site.Models;


namespace GMF.Demo.Site.Web.Controllers
{
    [Export]
    public class AccountController : Controller
    {
        #region 属性

        [Import]
        public IAccountSiteContract AccountContract { get; set; }

        #endregion

        #region 视图功能

        public ActionResult Login()
        {
            string returnUrl = Request.Params["returnUrl"];
            returnUrl = returnUrl ?? Url.Action("Index", "Home", new { area = "" });
            LoginModel model = new LoginModel
            {
                ReturnUrl = returnUrl
            };
            return View(model);
        }

        [HttpPost]
        
        public ActionResult Login(LoginModel model)
        {
            try
            {
                OperationResult result = AccountContract.Login(model);
                string msg = result.Message ?? result.ResultType.ToDescription();
                if (result.ResultType == OperationResultType.Success)
                {
                    return Redirect(model.ReturnUrl);
                }
                ModelState.AddModelError("", msg);
                return View(model);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                return View(model);
            }
        }

        public ActionResult Logout( )
        {
            string returnUrl = Request.Params["returnUrl"];
            returnUrl = returnUrl ?? Url.Action("Index", "Home", new { area = "" });
            if (User.Identity.IsAuthenticated)
            {
                AccountContract.Logout();
            }
            return Redirect(returnUrl);
        }

        #endregion
    }
}