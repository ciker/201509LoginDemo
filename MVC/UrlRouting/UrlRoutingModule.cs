using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace UrlRouting
{
    public class UrlRoutingModule : IHttpModule
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Init(HttpApplication context)
        {
            context.PostResolveRequestCache += context_PostResolveRequestCache;
            //throw new NotImplementedException();
        }

        void context_PostResolveRequestCache(object sender, EventArgs e)
        {
            var httpContext = HttpContext.Current;
            var routeData = RouteTable.Routes.GetRouteData(httpContext);
            if (null == routeData)
                return;
            var requestContext = new RequestContext { RouteData = routeData, HttpContext = httpContext };
            var handler = routeData.RouteHandler.GetHttpHandler(requestContext);
            httpContext.RemapHandler(handler);
        }
    }

    /// <summary>
    /// 包装请求上下文
    /// </summary>
    public class RequestContext
    {
        public RouteData RouteData { get; set; }
        public HttpContext HttpContext { get; set; }
    }
}
