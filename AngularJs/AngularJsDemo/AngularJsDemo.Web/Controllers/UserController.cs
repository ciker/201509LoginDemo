using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AngularJsDemo.Web.DB.Models;

namespace AngularJsDemo.Web.Controllers
{
    //[RoutePrefix("Users")]
    public class UserController : Controller
    {
        protected List<UserInfo> users;

        public UserController()
        {
            users = new List<UserInfo>()
            {
                new UserInfo()
                {
                    ID = 1,
                    Name = "小马",
                    Email = "xiaoma@xiaoma.com",
                    Mobile = "13012340123"
                },
                new UserInfo()
                {
                    ID = 2,
                    Name = "小A",
                    Email = "xiaoa@xiaoa.com",
                    Mobile = "13101230123"
                }
            };
        }

        //
        // GET: /User/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetUserList()
        {
            var data = users;
            return Json(data, JsonRequestBehavior.AllowGet);

            
        }

        public ActionResult EditUser(UserInfo userInfo)
        {
            var user = users.Where(a => a.ID.Equals(userInfo.ID)).Single();
            users.Remove(user);
            user.Name = userInfo.Name;
            user.Email = userInfo.Email;
            user.Mobile = userInfo.Mobile;
            users.Add(user);
            return Json(users, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddUser(UserInfo userInfo)
        {
            users.Add(userInfo);
            return Json(users, JsonRequestBehavior.AllowGet);
        }
    }
}