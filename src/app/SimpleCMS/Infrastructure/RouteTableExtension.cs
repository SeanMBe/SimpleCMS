using System;
using System.Linq;
using System.Web.Routing;

namespace SimpleCMS.Infrastructure
{
    public static class RouteTableExtension {
        public static void WriteRoutesToConsole(this RouteCollection routes) {
            foreach (var route in RouteTable.Routes) {
                Console.WriteLine(BuildRoute(route));
            }
        }

        static string BuildRoute(RouteBase routeBase) {
            var route = ((Route)routeBase);
            var allowedMethods = ((HttpMethodConstraint)route.Constraints["httpMethod"]).AllowedMethods;
            return string.Format("{0} {1} => {2}#{3}",
                                 allowedMethods.ElementAt(0),
                                 route.Url,
                                 route.Defaults["controller"],
                                 route.Defaults["action"]);
        }
    }
}