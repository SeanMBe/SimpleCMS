using System;
using System.Web.Mvc;
using SimpleCMS.Core.Logging;
using SimpleCMS.Core.Services;

namespace SimpleCMS.Controllers
{
    public class ErrorsController : Controller {
        static readonly ILogger logger = LogService.GetCurrentClassLogger();
        public ActionResult Http500(Exception exception) {
            logger.Error(exception);
            return View("500", new ExceptionDetails(exception));
        }

        public ActionResult HttpFriendly500(Exception exception) {
            logger.Error(exception);
            return View("Friendly500", new ExceptionDetails(exception));
        }

        public ActionResult Http404() {
            return View("404");
        }

        public ActionResult Http403() {
            return View("403");
        }
    }

    public class ExceptionDetails
    {
        public string Message { get; set; }
        public string Description { get; set; }
        public string Details { get; set; }
        public string StackTrace { get; set; }
        public string InnerStackTrace { get; set; }

        public ExceptionDetails(Exception exception)
        {
            var baseException = exception.GetBaseException();
            Message = baseException.Message;
            Description = "An unhandled exception occurred during the execution of the current web request. Please review the stack trace for more information about the error and where it originated in the code.";
            Details = string.Format("{0}: {1}", baseException.GetType(), baseException.Message);
            InnerStackTrace = baseException.StackTrace;
            StackTrace = exception.StackTrace;
        }
    }
}