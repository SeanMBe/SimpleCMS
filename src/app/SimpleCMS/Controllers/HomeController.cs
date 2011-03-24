using System.Web.Mvc;
using SimpleCMS.Data;
using SimpleCMS.Infrastructure;
using SimpleCMS.Models;

namespace SimpleCMS.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository repository;

        public HomeController(IRepository repository)
        {
            this.repository = repository;
        }

        public ActionResult Index()
        {
            var posts = repository.FindAll<Post>(post => post.CreatedDate);

            return View(posts);
        }
    }
}
