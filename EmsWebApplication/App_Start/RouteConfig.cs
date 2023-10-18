using System.Web.Mvc;
using System.Web.Routing;

namespace EmsWebApplication
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // Define a custom route for the AddToCart action.
            routes.MapRoute(
                name: "AddToCart",
                url: "Cart/AddToCart/{ticketId}",
                defaults: new { controller = "Cart", action = "AddToCart", ticketId = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
