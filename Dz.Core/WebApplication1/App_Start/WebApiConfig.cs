using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace WebApplication1
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API 配置和服务

            // Web API 路由
            config.MapHttpAttributeRoutes();

           // config.Routes.MapHttpRoute(
           //     name: Guid.NewGuid().ToString(),
           //     routeTemplate: "{controller}/{action}/{id}",
           //     defaults: new { action = "Get", id = RouteParameter.Optional }
           // );

           // config.Routes.MapHttpRoute(
           //    name: Guid.NewGuid().ToString(),
           //    routeTemplate: "api/{controller}/{action}/{id}",
           //    defaults: new { action = "Get", id = RouteParameter.Optional }
           //);

            config.Routes.MapHttpRoute(
               name: Guid.NewGuid().ToString(),
               routeTemplate: "api/{controller}/{action}",
               defaults: new { action = "Get"}
           );
        }
    }
}
