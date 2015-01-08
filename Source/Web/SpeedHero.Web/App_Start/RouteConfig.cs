namespace SpeedHero.Web
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using System.Web.Routing;

    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Create post",
                url: "CreatePost",
                defaults: new { controller = "Post", action = "CreatePost" },
                namespaces: new[] { "SpeedHero.Web.Controllers" });

            routes.MapRoute(
                name: "Static pages",
                url: "{action}",
                defaults: new { controller = "Home" },
                namespaces: new[] { "SpeedHero.Web.Controllers" });

            routes.MapRoute(
                name: "Show post",
                url: "{action}/{id}",
                defaults: new { controller = "Post" },
                namespaces: new[] { "SpeedHero.Web.Controllers" });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "SpeedHero.Web.Controllers" });
        }
    }
}
