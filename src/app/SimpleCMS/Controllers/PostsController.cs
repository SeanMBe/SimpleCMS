using System.Collections.Generic;
using System.Web.Mvc;
using NHibernate.Criterion;
using SimpleCMS.Core.Data;
using SimpleCMS.Core.Logging;
using SimpleCMS.Core.Models;
using SimpleCMS.Core.Services;

namespace SimpleCMS.Controllers
{   
    public class PostsController : Controller
    {
        private ILogger logger = LogService.GetCurrentClassLogger();

        readonly IRepository repository;

        public PostsController(IRepository repository)
        {
            this.repository = repository;
        }

        public ViewResult Show()
        {
            var posts = repository.FindAll<Post>();
            return View(posts);
        }

        public ActionResult Create(Post post, int authorId) {
            if (ModelState.IsValid) {
                post.Author = repository.Find<Account>(x => x.Id == authorId);
                repository.Save(post);
                return RedirectToAction("Show");
            }

            return RedirectToAction("New");
        }

        public ActionResult New()
        {
            ViewBag.Authors = repository.FindAll<Account>();
            return View();
        }
        
        public ActionResult Edit(int id)
        {
            ViewBag.Authors = repository.FindAll<Account>();
            var post = repository.Find<Post>(x => x.Id == id);
			return View(post);
        }

        public ActionResult Update(Post post, int authorId)
        {
            if (ModelState.IsValid)
            {
                post.Author = repository.Find<Account>(x => x.Id == authorId);
                repository.Save(post);
                return RedirectToAction("Show");
            }

            ViewBag.Authors = repository.FindAll<Account>();
            var refreshedPost = repository.Find<Post>(x => x.Id == post.Id);

            return View("Edit", new { post = refreshedPost });
        }

        public ActionResult Destroy(int id)
        {
            repository.Delete<Post>(id);
            return RedirectToAction("Show");
        }

        public ViewResult Details(int id) {
            var post = repository.Find<Post>(id);
            return View(post);
        }

        public ViewResult Search(string query) {
            if (!string.IsNullOrEmpty(query)) {
                ViewBag.Query = query;
                var wildCardSearch = string.Format("%{0}%", query);

                var matchTitle = Restrictions.On<Post>(p => p.Title).IsLike(wildCardSearch);
                var matchBody = Restrictions.On<Post>(p => p.Body).IsLike(wildCardSearch);

                var posts = repository
                    .Session
                    .QueryOver<Post>()
                    .Where(matchTitle || matchBody)
                    .List();
                return View(posts);
            }
            return View(new List<Post>());
        }
    }
}