using LoginDemo.BLL.Interface;
using LoginDemo.Entity;
using System.Web.Mvc;
using LoginDemo.Web.Controllers;

namespace LoginDemo.Web.Areas.Users.Controllers
{
    public class UserController : BaseController
    {
        #region properties
        private readonly IUserBLL _iUserBll;
        #endregion

        #region constructor
        public UserController(IUserBLL userbll)
        {
            _iUserBll = userbll;
        }
        #endregion

        #region List
        //
        // GET: /user/Users/
        [HttpGet]
        public ActionResult UserList(UserQueryParameter para)
        {
            para.IsPage = true;
            var data = _iUserBll.GetUserListbyParameter(para);
            return View(data);
        }

        #endregion

        #region Register
        [HttpGet]
        public ActionResult Register()
        {
            return View("~/Areas/Users/Views/user/_EditUser.cshtml");
        }

        [HttpPost]
        [ValidateInput(true)]
        public ActionResult RegisterUser(User user)
        {
            var response = _iUserBll.Register(user);
            if (response.IsSuccess)
            {
                return AlertSuccessJsonResult(response, "register success");
            }
            else
            {
                return AlertErrorJsonResult("", response.Message);
            }
        }
        #endregion

        #region Login
        [HttpGet]
        public ActionResult Login()
        {
            return View("~/Areas/Users/Views/user/_login.cshtml");
        }


        [HttpPost]
        public ActionResult Login(string userName, string userPwd)
        {
            var res = _iUserBll.Login(new User() { UserName = userName, UserPWD = userPwd });
            if (res.IsSuccess)
            {
                return AlertSuccessJsonResult("", "Login success");
            }
            else
            {
                return AlertErrorJsonResult("", res.Message);
            }

        }
        #endregion
    }

}