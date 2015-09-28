using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Mvc;
using UrlRouting;

namespace UrlRouting
{
    public class MvcHandler : IHttpHandler
    {
        public bool IsReusable
        {
            get { throw new NotImplementedException(); }
        }
        public void ProcessRequest(HttpContext context)
        {
            #region 此处本来通过反射创建控制器
            IController controller = new Controller();
            #endregion
            controller.Execute(context);
        }
    }
}
