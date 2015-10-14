using Newtonsoft.Json.Serialization;
using System.Web.Http;

namespace InstallWebApiOwinIIS.Config
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // configure Json formatter (optional)
            config.Formatters.Remove(config.Formatters.XmlFormatter);
            var json = config.Formatters.JsonFormatter;
            json.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();


            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
               name: "Home",
               //routeTemplate: "{*anything}",
               routeTemplate: "{controller}",
               defaults: new { controller = "Home" });
        }
    }
}