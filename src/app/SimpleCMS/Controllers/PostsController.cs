using System.Web.Mvc;
using SimpleCMS.Data;
using SimpleCMS.Models;

namespace SimpleCMS.Controllers
{   
    public class PostsController : Controller
    {
        readonly IRepository repository;

        public PostsController(IRepository repository)
        {
            this.repository = repository;
        }

        public ViewResult Index()
        {
            var posts = repository.FindAll<Post>();
            return View(posts);
        }

        public ActionResult Create()
        {
            ViewBag.Authors = repository.FindAll<Account>();
            return View();
        } 

        [HttpPost]
        public ActionResult Create(Post post, int authorId)
        {
            if (ModelState.IsValid)
            {
                post.Author = repository.Find<Account>(x => x.Id == authorId);
                repository.Save(post);
				return RedirectToAction("Index");  
            }

            return View(post);
        }
        
        public ActionResult Edit(int id)
        {
            ViewBag.Authors = repository.FindAll<Account>();
            var post = repository.Find<Post>(x => x.Id == id);
			return View(post);
        }

        [HttpPost]
        public ActionResult Edit(Post post, int authorId)
        {
            if (ModelState.IsValid)
            {
                post.Author = repository.Find<Account>(x => x.Id == authorId);
                repository.Save(post);
                return RedirectToAction("Index");
            }
            ViewBag.Authors = repository.FindAll<Account>();
            var refreshedPost = repository.Find<Post>(x => x.Id == post.Id);
            return View(refreshedPost);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            repository.Delete<Post>(id);
            return RedirectToAction("Index");
        }
    }
}