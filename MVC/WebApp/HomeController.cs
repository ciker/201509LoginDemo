using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using Mvc;

namespace WebApp
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return Content("Hello World");
        }
    }
}