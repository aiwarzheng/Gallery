using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Gallery.WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.RouteExistingFiles = true;

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.IgnoreRoute("{*assets}", new { assets = @"(.*/)*(.*\.(css|js|gif|jpg|jpeg|png|svg|woff|ttf|eot|woff2))" });  //ignore all resource files

            routes.MapRoute(
                "api",
                "api/{action}",
                new { controller = "Api", action = "Images" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{action}/{id}",
                defaults: new { controller = "Gallery", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
