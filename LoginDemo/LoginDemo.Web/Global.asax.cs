using System;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;

namespace LoginDemo.Web
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            //IOC
            var builder = new ContainerBuilder();
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies()).Where(t => t.Name.EndsWith("BLL")).AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies()).Where(t => t.Name.EndsWith("DAL")).AsImplementedInterfaces();

            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
