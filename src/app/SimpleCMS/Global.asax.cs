using System;
using System.Web;
using System.Web.Mvc;
using SimpleCMS.Core.Infrastructure;

namespace SimpleCMS {
    public class MvcApplication : HttpApplication {
        protected void Application_Start() {
            BootStrap.RegisterRoutes();
            Ioc.BuildContainer();
            ControllerBuilder.Current.SetControllerFactory(new CustomControllerFactory());
        }

        protected void Application_EndRequest(object sender, EventArgs e) {
            Ioc.EndRequest();
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            BootStrap.ReturnThroughErrorController(Server, Response, Context);
        }
    }
}