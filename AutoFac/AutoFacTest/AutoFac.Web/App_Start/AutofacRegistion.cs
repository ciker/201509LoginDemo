using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using Autofac;
using Autofac.Configuration;
using Autofac.Controllers.Filter;
using Autofac.Integration.Mvc;
using Autofac.Integration.Wcf;

namespace AutoFac.Web.App_Start
{
    /// <summary>
    /// 依赖注入Controller、FilterAtrribute、WCF
    /// </summary>
    public class AutofacRegistion
    {
        /// <summary>
        /// 创建 MVC容器（包含Filter）
        /// </summary>
        public static void BuildMvcContainer()
        {
            var builder = new ContainerBuilder();
            //注册Module方法2 在Web.config中配制方式
            builder.RegisterModule(new ConfigurationSettingsReader("autofacMvc"));
            //加载 *.Controllers 层的控制器,否则无法在其他层控制器构造注入，只能在web层注入
            Assembly[] asm = GetAllAssembly("*.Controllers.dll").ToArray();
            builder.RegisterAssemblyTypes(asm);
            //注册仓储
            Assembly[] asmRepository = GetAllAssembly("*.Repository.dll").ToArray();
            builder.RegisterAssemblyTypes(asmRepository)
               .Where(t => t.Name.EndsWith("Repository"))
               .AsImplementedInterfaces();

            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterModelBinders(Assembly.GetExecutingAssembly());
            builder.RegisterModelBinderProvider();


            //注册过滤器 
            builder.RegisterFilterProvider();
            builder.RegisterType<OperateAttribute>().PropertiesAutowired();//非global filter  属性注入
            builder.RegisterType<GlobalFilterAttribute>().SingleInstance();//global filter 注入
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
        /// <summary>
        ///创建WCF的容器，不存放Controller、Filter
        /// </summary>
        public static void BuildWcfContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new ConfigurationSettingsReader("autofacWcf"));
            builder.RegisterModelBinders(Assembly.GetExecutingAssembly());
            builder.RegisterModelBinderProvider();
            var container = builder.Build();
            //WCF IOC容器
            AutofacHostFactory.Container = container;
            //DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }

        #region 加载程序集
        public static List<Assembly> GetAllAssembly(string dllName)
        {
            List<string> pluginpath = FindPlugin(dllName);
            var list = new List<Assembly>();
            foreach (string filename in pluginpath)
            {
                try
                {
                    string asmname = Path.GetFileNameWithoutExtension(filename);
                    if (asmname != string.Empty)
                    {
                        Assembly asm = Assembly.LoadFrom(filename);
                        list.Add(asm);
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                }
            }
            return list;
        }
        //查找所有插件的路径
        private static List<string> FindPlugin(string dllName)
        {
            List<string> pluginpath = new List<string>();

            string path = AppDomain.CurrentDomain.BaseDirectory;
            string dir = Path.Combine(path, "bin");
            string[] dllList = Directory.GetFiles(dir, dllName);
            if (dllList.Length > 0)
            {
                pluginpath.AddRange(dllList.Select(item => Path.Combine(dir, item.Substring(dir.Length + 1))));
            }
            return pluginpath;
        }
        #endregion

    }
}