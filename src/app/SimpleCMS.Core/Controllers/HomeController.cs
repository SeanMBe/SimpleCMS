using System.Web.Mvc;

namespace SimpleCMS.Core.Controllers
{
    public class HomeController: Controller
    {
        public ViewResult Index() {
            return View();
        }
    }
}