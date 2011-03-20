using System.Web.Mvc;
using NHibernate;
using SimpleCMS.Data;
using SimpleCMS.Infrastructure;
using SimpleCMS.Models;

namespace SimpleCMS.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISession session;

        public HomeController(ISession session)
        {
            this.session = session;
        }

        public ActionResult Index()
        {
            var posts = session
                .QueryOver<Post>()
                .OrderBy(post => post.CreatedDate).Asc
                .List();

            return View(posts);
        }

        //TODO: Find a better solution for this
        public ActionResult BuildSchema()
        {
            ComponentsInstaller.GetDataSession().BuildSchema(session);

            var repository = new Repository(session);
            var user = new User { UserName = "Tom Bombadil" };
            repository.Save(user);
            repository.Save(new Post { Title = "Title", Body = "Body", Author = user });

            return RedirectToAction("Index");
        }
    }
}
