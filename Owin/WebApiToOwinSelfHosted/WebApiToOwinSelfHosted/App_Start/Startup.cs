using Microsoft.Owin;
using Owin;
using System.Web.Http;
using WebApiToOwinSelfHosted.App_Start;

[assembly: OwinStartup(typeof(Startup))]
namespace WebApiToOwinSelfHosted.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var httpConfiguration = new HttpConfiguration();
            WebApiConfig.Register(httpConfiguration);
            app.UseWebApi(httpConfiguration);
        }
    }

}
