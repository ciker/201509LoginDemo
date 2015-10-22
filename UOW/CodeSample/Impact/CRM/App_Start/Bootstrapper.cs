using System.Data.Entity;
using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using CRM.Api;
using Domain;
using Infrastructure;
using Service;
using WebMatrix.WebData;

namespace CRM.App_Start
{

    public static class Bootstrapper
    {
        public static void Start()
        {
            InitializeDatabase();
            SetAutofacContainer();
        }
        private static void InitializeDatabase()
        {

            Database.SetInitializer<CRMContext>(new CRMContextInitializer());
            var context = new CRMContext();
            context.Database.Initialize(true);
        }

        private static void SetAutofacContainer()
        {
            //Create Autofac builder
            var builder = new ContainerBuilder();
            //Now register all depedencies to your custom IoC container

            //register mvc controller
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            builder.RegisterAssemblyTypes(typeof(MvcApplication).Assembly)
               .AsImplementedInterfaces();

            builder.RegisterModelBinderProvider();

            // Register the Web API controllers.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<CRMContext>().As<DbContext>().InstancePerDependency();
            builder.Register(c => new CRMContext()).InstancePerDependency();

            builder.RegisterGeneric(typeof(Repository<>))
                  .As(typeof(IRepository<>)).InstancePerDependency();

            //Model
            builder.RegisterAssemblyTypes(typeof(Contact).Assembly)
                   .Where(t => t.Name.EndsWith("Domain"))
                   .AsImplementedInterfaces().InstancePerDependency();


            //Infrastructure
            builder.RegisterAssemblyTypes(typeof(CRMContext).Assembly)
                   .Where(t => t.Name.EndsWith("Infrastructure"))
                   .AsImplementedInterfaces().InstancePerDependency();

            //Service
            builder.RegisterAssemblyTypes(typeof(AccountService).Assembly)
                   .Where(t => t.Name.EndsWith("Service"))
                   .AsImplementedInterfaces().InstancePerDependency();

            var containerBuilder = builder.Build();

            //MVC resolver
            DependencyResolver.SetResolver(new AutofacDependencyResolver(containerBuilder));


            // Create the depenedency resolver for Web Api
            var resolverWebApi = new AutofacWebApiDependencyResolver(containerBuilder);

            // Configure Web API with the dependency resolver.
            GlobalConfiguration.Configuration.DependencyResolver = resolverWebApi;



          


        }
    }
}

