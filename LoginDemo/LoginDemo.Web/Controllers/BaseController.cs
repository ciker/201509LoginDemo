using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using LoginDemo.Commom;

namespace LoginDemo.Web.Controllers
{
    public class BaseController : Controller
    {

        public JsonResult AlertSuccessJsonResult(object obj, string message)
        {
            return AlertJsonResult(obj, 1, true, message);
        }
        public JsonResult AlertErrorJsonResult(object obj, string message)
        {
            return AlertJsonResult(obj, 0, false, message);
        }

        private JsonResult AlertJsonResult(object obj, int actionType, bool isSuccess, string message = "")
        {
            return Json(new { data = obj, action = actionType, success = isSuccess, msg = message });
        }
    }
}