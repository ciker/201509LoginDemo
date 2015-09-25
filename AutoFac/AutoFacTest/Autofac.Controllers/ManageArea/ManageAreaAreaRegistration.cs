using System.Web.Mvc;

namespace Autofac.Controllers.ManageArea
{
    public class ManageAreaAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "ManageArea";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "ManageArea_default",
                "ManageArea/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
