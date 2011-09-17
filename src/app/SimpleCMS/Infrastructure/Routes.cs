using System;
using System.Linq;
using System.Web.Routing;
using RestfulRouting;
using SimpleCMS.Controllers;

namespace SimpleCMS.Infrastructure {
    public class Routes : RouteSet {
        public override void Map(IMapper map) {
            map.Root<HomeController>(x => x.Index());
            map.Resources<PostsController>(posts => {
			    posts.As("posts");
                //posts.Collection(x => {
                //    x.Get("latest");
                //    x.Post("someaction");
                //});
                //posts.Member(x => x.Put("move"));
		    });
            map.Resource<AccountsController>(accounts => {
                accounts.As("accounts");
            });
        }
    }

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