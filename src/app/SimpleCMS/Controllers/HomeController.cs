using System.Web.Mvc;
using SimpleCMS.Core.Data;
using SimpleCMS.Core.Models;
using SimpleCMS.Infrastructure;

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
