using System;
using SimpleCMS.Core.Logging;
using SimpleCMS.Core.Services;
using SimpleCMS.Infrastructure;

namespace SimpleCMS {
    public class MvcApplication : System.Web.HttpApplication {
        static readonly ILogger logger = LogService.GetCurrentClassLogger();

        protected void Application_Start() {
            BootStrap.ApplicationStart();
        }

        protected void Application_End() {
            BootStrap.ApplicationEnd();
        }

        protected void Application_EndRequest(object sender, EventArgs e) {
            BootStrap.EndRequest();
        }

        protected void Application_Error(object sender, EventArgs e) {
            logger.Error("Application error", Server.GetLastError());
        }
    }
}