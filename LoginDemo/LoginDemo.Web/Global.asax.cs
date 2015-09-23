using System;
using System.Linq;
using System.Reflection;
//using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
//using Autofac.Configuration;
using Autofac.Integration.Mvc;
using LoginDemo.Commom;

namespace LoginDemo.Web
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            //IOC
            var builder = new ContainerBuilder();
            var baseType = typeof(IDependency);
            //function 0
            //builder.RegisterControllers(Assembly.GetExecutingAssembly());
            //function 1
            //AppDomain.CurrentDomain.GetAssemblies().Where(assembly =>
            //{
            //    assembly.
            //});
            //var currentDomin = AppDomain.CurrentDomain;
            //currentDomin.AssemblyResolve += CurrentDominOnAssemblyResolve;

            #region builder container

            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            builder.RegisterControllers(assemblies.ToArray());
            builder.RegisterAssemblyTypes(assemblies).Where(t => baseType.IsAssignableFrom(t) && t != baseType)
                .AsImplementedInterfaces().InstancePerLifetimeScope();

            //function 2
            //builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies()).Where(t => t.Name.EndsWith("BLL")).AsImplementedInterfaces();
            //builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies()).Where(t => t.Name.EndsWith("DAL")).AsImplementedInterfaces();

            //function 3
            //builder.RegisterModule(new ConfigurationSettingsReader("autofac"));


            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            #endregion

            #region config
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            #endregion
        }

        private Assembly CurrentDominOnAssemblyResolve(object sender, ResolveEventArgs args)
        {
            var strFielName = args.Name.Split(',')[0];
            return Assembly.LoadFile(string.Format(AppDomain.CurrentDomain.BaseDirectory + @"{0}.dll", strFielName));
        }


    }
}
