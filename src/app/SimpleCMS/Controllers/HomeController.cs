using System.Web.Mvc;

namespace SimpleCMS.Controllers
{
    public class HomeController: Controller
    {
        public ViewResult Index() {
            return View();
        }
    }
}