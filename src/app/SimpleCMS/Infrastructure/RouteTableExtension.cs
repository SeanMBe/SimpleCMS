using System;
using System.Linq;
using System.Web.Routing;

namespace SimpleCMS.Infrastructure
{
    public static class RouteTableExtension {
        public static void WriteRoutes(this RouteCollection routes, Action<string> writeAction) {
            foreach (var route in RouteTable.Routes) {
                writeAction(BuildRoute(route));
            }
        }

        static string BuildRoute(RouteBase routeBase) {
            var route = ((Route)routeBase);
            var allowedMethods = ((HttpMethodConstraint)route.Constraints["httpMethod"]).AllowedMethods;
            return string.Format("{0} {1} => {2}#{3}",
                                 allowedMethods.ElementAt(0),
                                 string.IsNullOrEmpty(route.Url) ? "/" : route.Url,
                                 route.Defaults["controller"],
                                 route.Defaults["action"]);
        }
    }
}