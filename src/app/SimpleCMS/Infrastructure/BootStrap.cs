using System.Web.Mvc;
using System.Web.Routing;
using Castle.Windsor;
using Castle.Windsor.Installer;
using RestfulRouting;

namespace SimpleCMS.Infrastructure
{
    public class BootStrap
    {
        private static IWindsorContainer container;

        public static void Container()
        {
            container = new WindsorContainer().Install(FromAssembly.This());
            var controllerFactory = new WindsorControllerFactory(container.Kernel);
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);
        }

        public static void Mvc()
        {
            AreaRegistration.RegisterAllAreas();
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes();
            EnablingUnobtrusiveAjax();
        }

        public static void EnablingUnobtrusiveAjax()
        {
            HtmlHelper.ClientValidationEnabled = true;
            HtmlHelper.UnobtrusiveJavaScriptEnabled = true;
        }

        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes()
        {
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RestfulRoutingRazorViewEngine());
            RouteTable.Routes.MapRoutes<Routes>();
        }

        public static void Dispose()
        {
            container.Dispose();
        }
    }
}