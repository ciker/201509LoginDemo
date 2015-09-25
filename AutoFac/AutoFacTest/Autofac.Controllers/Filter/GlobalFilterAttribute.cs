using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Autofac.CoreService;

namespace Autofac.Controllers.Filter
{
    public class GlobalFilterAttribute : AuthorizationFilterAttribute
    {
        public IUserManage UserManage;

        public GlobalFilterAttribute()
        {

        }
        public GlobalFilterAttribute(IUserManage IUserManage)
        {
            UserManage = IUserManage;
        }
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var user = UserManage.LoadUser(0);
            if (user == null)
            {
                actionContext.Response = new System.Net.Http.HttpResponseMessage(HttpStatusCode.Unauthorized);//need authorize
            }
            base.OnAuthorization(actionContext);
        }
    }
}
