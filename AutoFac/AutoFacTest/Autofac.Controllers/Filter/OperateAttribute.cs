using System.Web.Mvc;
using Autofac.CoreService;

namespace Autofac.Controllers.Filter
{
    public class OperateAttribute : ActionFilterAttribute
    {
        public IUserManage UserManage { get; set; }
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            //Do Something...
            var user =UserManage.LoadUser(0);
            if (user == null)
            {
                filterContext.Result = new JsonResult
                {
                    Data=new{message="不合法操作,未能进入"},
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                }; 
            }
        }
    }
}
