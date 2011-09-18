using System.Web.Mvc;

namespace SimpleCMS.Tests.Helpers
{
    public static class ActionResultExtensions
    {
        public static string View(this ActionResult result)
        {
            if (result is ViewResult)
                return ((ViewResult) result).ViewName;
            if (result is RedirectToRouteResult)
                return ((RedirectToRouteResult)result).RouteValues["action"] as string;
            return string.Empty;
        }
    }
}