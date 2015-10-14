using InstallWebApiOwinIIS.Config;
using Owin;
using System.Web.Http;

namespace InstallWebApiOwinIIS
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            WebApiConfig.Register(config);

            app.UseWebApi(config);

            // met.1
            //app.Run(context =>
            //{
            //    context.Response.ContentType = "text/plain";
            //    return context.Response.WriteAsync("Hello World111!");
            //});

            // met.2 (async)
            //app.Run(async context =>
            //{
            //    context.Response.ContentType = "text/plain";
            //    await context.Response.WriteAsync("Hello World222!");
            //});
        }
    }
}