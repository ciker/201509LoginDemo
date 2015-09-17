using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LoginDemo.Web.Startup))]
namespace LoginDemo.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
