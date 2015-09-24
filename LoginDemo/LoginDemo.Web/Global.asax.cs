using System;
using System.Linq;
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

            #region ==autofac文档：属性注入==

            //    AutoFac文档
            //    目录
            //    开始
            //    Registering components
            //    控制范围和生命周期
            //    用模块结构化Autofac
            //    xml配置
            //    与.net集成
            //    深入理解Autofac
            //    指导
            //    关于
            //    词汇表
            //    属性注入
            //    属性注入使用可写属性而不是构造函数参数实现注入。

            //    介绍
            //    如果component是一个委托，使用一个对象初始化：

            //    1
            //    builder.Register(c => new A { B = c.Resolve<B>() });
            //    为了提供循环依赖（就是当A使用B的时候B已经初始化），需要使用OnActivated事件接口：

            //    1
            //    builder.Register(c => new A()).OnActivated(e => e.Instance.B = e.Context.Resolve<B>());
            //    通过发射，使用PropertiesAutowired（）修饰符注入属性。

            //    1
            //    builder.RegisterType<A>().PropertiesAutowired();
            //    如果你预先知道属性的名字和值，你可以使用

            //    1
            //    builder.WithProperty("propertyName", propertyValue)。
            #endregion
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

    }
}
