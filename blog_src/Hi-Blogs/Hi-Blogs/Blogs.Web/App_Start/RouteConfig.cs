using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Blogs.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //第一种情况：以 域名 + 用户名 + id + html 后缀组合（默认控制器）
            routes.MapRoute(
               name: "UserPage",
               url: "{name}/{id}.html/{controller}/{action}",
               defaults: new { action = "UserBlog", controller = "UserBlog", name = "test", id = 1 }
           );
            //第二种情况：以 域名 + 用户名 + action  + id + html 后缀组合
            routes.MapRoute(
             name: "UserTagOrType",
             url: "{name}/{action}/{id}.html/{controller}",
             defaults: new { action = "UserBlogList", controller = "UserBlog" }
          );
            //add 
            routes.MapRoute(
            name: "test1",
            url: "{name}/{action}/{typeId}/{id}.html/{controller}",
            defaults: new { action = "UserBlogList", controller = "UserBlog" }
       );
            //第三种情况：以 
            routes.MapRoute(
            name: "ControllerAction",
            url: "{controller}/{action}/{id}",
            defaults: new { id = UrlParameter.Optional },
            namespaces: new string[1] { "Blogs.Controllers" }//到知道命名空间下 找控制器
      );
            //第四种情况：以 域名 + 用户名
            routes.MapRoute(
             name: "UserIndex",
             url: "{name}/{controller}/{action}/{id}",
             defaults: new { action = "UserBlogList", controller = "UserBlog", id = UrlParameter.Optional }
          );
            //第五种情况：之输入 域名
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}",
                defaults: new { controller = "Home", action = "Index" }
                //, constraints: new { id = "[a-z]" }//约束
           );

        }
    }
}