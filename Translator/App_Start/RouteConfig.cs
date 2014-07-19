using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Translator
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Translate", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Admin",
                url: "admin/{action}",
                defaults: new { controller = "Admin", action = "Users", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Users",
                url: "admin/user/{id}/{action}",
                defaults: new { controller = "Admin" }
            );
        }
    }
}
