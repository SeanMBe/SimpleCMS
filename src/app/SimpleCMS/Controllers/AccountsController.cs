using System.Web.Mvc;
using SimpleCMS.Data;
using SimpleCMS.Models;

namespace SimpleCMS.Controllers
{
    public class AccountsController : Controller
    {
        readonly IRepository repository;

        public AccountsController(IRepository repository)
        {
            this.repository = repository;
        }

        public ViewResult Index()
        {
            var posts = repository.FindAll<Account>();
            return View(posts);
        }

        public ActionResult Create()
        {
            return View();
        } 

        [HttpPost]
        public ActionResult Create(Account user)
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
            var post = repository.Find<Account>(x => x.Id == id);
			return View(post);
        }

        [HttpPost]
        public ActionResult Edit(Account user)
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
            repository.Delete<Account>(id);
            return RedirectToAction("Index");
        }
    }
}
