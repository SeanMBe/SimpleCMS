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

        //TODO: Find a better solution for this
        public ActionResult BuildSchema()
        {
            const string body =
                @"Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";

            ComponentsInstaller.GetDataSession().BuildSchema(repository.Session);

            var user = repository.Save(new Account { Email = "Tom Bombadil" });
            repository.Save(new Account { Email = "Bilbo Bagins" });
            
            repository.Save(new Post { Title = "Sample Post", Body = body, Author = user });

            return RedirectToAction("Index");
        }
    }
}
