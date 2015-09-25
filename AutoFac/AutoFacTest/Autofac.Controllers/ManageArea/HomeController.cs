using System.Web.Mvc;
using Autofac.Controllers.Filter;
using Autofac.CoreService;
using Autofac.DataModel;
using Autofac.ViewModel;

namespace Autofac.Controllers.ManageArea
{

    [GlobalFilterAttribute]
    public class HomeController : Controller
    {
        private readonly IUserManage _userManage;
        private IRoleManage _roleManage;
        public HomeController(IUserManage userManage, IRoleManage roleManage)
        {
            _userManage = userManage;
            _roleManage = roleManage;
        }

        public ActionResult Index()
        {
            return View();
        }
        [System.Web.Http.HttpGet]
        public ActionResult UserList()
        {
            return Json(_userManage.Users(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult EditUser(int? id)
        {

            ViewBag.roles = _roleManage.GetListRoleIdName();
            if (id == null || id < 1)
            {
                ViewBag.user = Newtonsoft.Json.JsonConvert.SerializeObject(new Sys_User { });
                return View();
            }

            ViewBag.user = Newtonsoft.Json.JsonConvert.SerializeObject(_userManage.LoadUser(id ?? 0));
            return View();
        }



        [System.Web.Http.HttpPost]
        public ActionResult SaveUser(UserModel user)
        {
            string message = "";
            bool result = _userManage.SaveUser(user, out message);
            if (result)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("EditUser");
        }

        /// <summary>
        /// 过滤器
        /// </summary>
        /// <returns></returns>
        [Operate]
        public ActionResult FilteResult()
        {
            return View();
        }
    }
}
