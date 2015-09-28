using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace UrlRouting
{
    public class RouteCollection : Dictionary<string, Route>
    {
        public RouteData GetRouteData(HttpContext httpContext)
        {
            return new RouteData(){RouteHandler = new MvcRouteHandler() };
        }
    }
}
