using System.Web.Mvc;
using System.Web.Routing;
using RestfulRouting;
using StructureMap;

namespace SimpleCMS.Infrastructure {
    public class BootStrap {
        public static void ApplicationStart() {
            AreaRegistration.RegisterAllAreas();
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes();
            EnablingUnobtrusiveAjax();
            ContainerBuilder.Build();
            SetupControllerFactory();
        }

        public static void ApplicationEnd() {
        }

        public static void EndRequest() {
            ObjectFactory.ReleaseAndDisposeAllHttpScopedObjects();
        }

        public static void RegisterRoutes() {
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RestfulRoutingRazorViewEngine());
            RouteTable.Routes.MapRoutes<Routes>();
            //RouteTable.Routes.IgnoreRoute("{Content}/{*pathInfo}");
            //RouteTable.Routes.IgnoreRoute("{*favicon}", new {favicon = @"(.*/)?favicon.ico(/.*)?"});
        }

        static void SetupControllerFactory() {
            var controllerFactory = new CustomControllerFactory();
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);
        }

        static void EnablingUnobtrusiveAjax() {
            HtmlHelper.ClientValidationEnabled = true;
            HtmlHelper.UnobtrusiveJavaScriptEnabled = true;
        }

        static void RegisterGlobalFilters(GlobalFilterCollection filters) {
            filters.Add(new HandleErrorAttribute());
        }
    }
}