using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using RestfulRouting;
using SimpleCMS.Controllers;
using SimpleCMS.Core;

namespace SimpleCMS.Infrastructure {
    public class BootStrap {
        public static void RegisterRoutes() {
            AreaRegistration.RegisterAllAreas();
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RestfulRoutingRazorViewEngine());
            RouteTable.Routes.MapRoutes<Routes>();
        }

        public static void ReturnThroughErrorController(HttpServerUtility server, HttpResponse response, HttpContext context) {
            var exception = server.GetLastError();
            server.ClearError();

            response.Clear();
            response.StatusCode = GetStatusCode(exception);

            var routeData = new RouteData();
            routeData.Values["controller"] = "Errors";
            routeData.Values["action"] = GetActionForStatusCode(response.StatusCode);
            routeData.Values["exception"] = exception;

            IController errorsController = new ErrorsController();
            var requestContext = new RequestContext(new HttpContextWrapper(context), routeData);
            errorsController.Execute(requestContext);
        }

        private static int GetStatusCode(Exception exception)
        {
            var statusCode = 500;
            var httpException = exception as HttpException;
            if (httpException != null)
            {
                statusCode = httpException.GetHttpCode();
            }
            return statusCode;
        }

        private static string GetActionForStatusCode(int statusCode)
        {
            if (statusCode == 404) return "Http404";
            if (statusCode == 403) return "Http403";
            if (AppSettings.Environment.Release)
                return "HttpFriendly500";
            return "Http500";
        }
    }
}