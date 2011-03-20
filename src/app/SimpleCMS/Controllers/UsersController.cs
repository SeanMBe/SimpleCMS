using System.Web.Mvc;
using SimpleCMS.Data;
using SimpleCMS.Models;

namespace SimpleCMS.Controllers
{
    public class UsersController : Controller
    {
        readonly IRepository repository;

        public UsersController(IRepository repository)
        {
            this.repository = repository;
        }

        public ViewResult Index()
        {
            var posts = repository.FindAll<User>();
            return View(posts);
        }

        public ActionResult Create()
        {
            return View();
        } 

        [HttpPost]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                repository.Save(user);
				return RedirectToAction("Index");  
            }

            return View(user);
        }
        
        public ActionResult Edit(int id)
        {
            var post = repository.Find<User>(x => x.Id == id);
			return View(post);
        }

        [HttpPost]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                repository.Save(user);
                return RedirectToAction("Index");
            }
            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            repository.Delete<User>(id);
            return RedirectToAction("Index");
        }
    }
}
