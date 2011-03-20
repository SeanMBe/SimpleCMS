using SimpleCMS.Infrastructure;

namespace SimpleCMS
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            BootStrap.Mvc();
            BootStrap.Container();
        }

        protected void Application_End()
        {
            BootStrap.Dispose();
        }
    }
}