using System;
using SimpleCMS.Core.Logging;
using SimpleCMS.Core.Services;
using SimpleCMS.Infrastructure;

namespace SimpleCMS
{
    public class MvcApplication : System.Web.HttpApplication
    {
        static readonly ILogger logger = LogService.GetCurrentClassLogger();

        protected void Application_Start()
        {
            BootStrap.Mvc();
            BootStrap.Container();
        }

        protected void Application_End()
        {
            BootStrap.Dispose();
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            logger.Error("Application error", Server.GetLastError());
        }
    }
}