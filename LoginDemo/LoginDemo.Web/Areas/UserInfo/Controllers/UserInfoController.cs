using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using LoginDemo.BLL.Interface;
using LoginDemo.Entity.UserAccount.QueryParameter;
using LoginDemo.ViewModels.UserInfo;
using LoginDemo.Web.Controllers;

namespace LoginDemo.Web.Areas.UserInfo.Controllers
{
    public class UserInfoController : BaseController
    {

        #region properties
        private readonly IUserAccountBLL _userAccountBll;
        public IUserAccountBLL UserAccountBll
        {
            get { return _userAccountBll; }
        }
        #endregion

        #region constructor
        public UserInfoController(IUserAccountBLL iUserAccountBll)
        {
            _userAccountBll = iUserAccountBll;
        }
        #endregion
        //
        // GET: /UserInfo/UserInfo/
        public ActionResult Index(UserInfoQueryParameter userInfoQueryParameter)
        {
            userInfoQueryParameter.IsPage = true;
            var data = UserAccountBll.Query(userInfoQueryParameter);
            return View(data);
        }


        #region Register
        [HttpGet]
        public ActionResult Register()
        {
            return View("~/Areas/UserInfo/Views/UserInfo/_register.cshtml");
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Register(UserInfoViewModels userInfo)
        {
            var response = UserAccountBll.Register(userInfo);
            return response.IsSuccess ? AlertSuccessJsonResult(response, "register success")
                                      : AlertErrorJsonResult("", response.Message);
        }

        #endregion

        #region Login
        [HttpGet]
        public ActionResult Login()
        {
            return View("~/Areas/UserInfo/Views/UserInfo/_login.cshtml");
        }


        [HttpPost]
        public ActionResult Login(string Account, string Password)
        {
            var res = UserAccountBll.Login(new UserInfoViewModels() { Account = Account, Password = Password });
            return res.IsSuccess ? AlertSuccessJsonResult("", "Login success") : AlertErrorJsonResult("", res.Message);
        }

        #endregion
    }
}
