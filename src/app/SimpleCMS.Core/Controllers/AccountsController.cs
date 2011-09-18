using System.Web.Mvc;
using SimpleCMS.Core.Data;
using SimpleCMS.Core.Models;

namespace SimpleCMS.Core.Controllers
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
            var accounts = repository.FindAll<Account>();
            return View(accounts);
        }

        public ViewResult Show(int id) {
            var account = repository.Find<Account>(id);
            return View(account);
        }

        public ActionResult Create(Account account)
        {
            if (ModelState.IsValid)
            {
                repository.Save(account);
                return RedirectToAction("Index");  
            }

            return RedirectToAction("New");
        }

        public ActionResult New() {
            return View();
        }

        public ActionResult Edit(int id)
        {
            var post = repository.Find<Account>(x => x.Id == id);
			return View(post);
        }

        public ActionResult Update(Account account)
        {
            if (ModelState.IsValid)
            {
                repository.Save(account);
                return RedirectToAction("Show", account.Id);
            }
            return View("Edit", account);
        }

        public ActionResult Destroy(int id)
        {
            repository.Delete<Account>(id);
            return RedirectToAction("Index");
        }
    }
}
