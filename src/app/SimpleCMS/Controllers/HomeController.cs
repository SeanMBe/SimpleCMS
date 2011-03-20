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
            ComponentsInstaller.GetDataSession().BuildSchema(repository.Session);

            var user = new User { UserName = "Tom Bombadil" };
            repository.Save(user);
            repository.Save(new Post { Title = "Title", Body = "Body", Author = user });

            return RedirectToAction("Index");
        }
    }
}
