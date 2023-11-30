using System.Web.Mvc;
using System.Web.Routing;

namespace wp2
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // Default routing
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "sys", action = "start", id = UrlParameter.Optional }
            );

            // Special localisation route mapping - expects specific language/culture code as first param
            routes.MapRoute(
                name: "Localisation",
                url: "{lang}/{controller}/{action}/{id}",
                defaults: new { controller = "sys", action = "start", id = UrlParameter.Optional },
                constraints: new { lang = @"[a-z]{2}|[a-z]{2}-[a-zA-Z]{2}" }
            );

            ////Global catchall
            //routes.MapRoute(
            //    name: "Default",
            //    url: "{culture}/{controller}/{action}/{id}",
            //    defaults: new { culture = "en", controller = "sys", action = "start", id = UrlParameter.Optional }
            //);

            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "sys", action = "start", id = UrlParameter.Optional }
            //);

            //routes.MapRoute(
            //  name: "Login",
            //  url: "{id}",
            //  defaults: new { controller = "sys", action = "start", id = UrlParameter.Optional }
            //);

            ////This is for the folks who didn't put a language in their url.
            //routes.MapRoute(
            //  "Catchall",
            //  "{*catchall}",
            // defaults: new { culture = "en", controller = "sys", action = "start", id = UrlParameter.Optional }
            //);

        }
    }
}
