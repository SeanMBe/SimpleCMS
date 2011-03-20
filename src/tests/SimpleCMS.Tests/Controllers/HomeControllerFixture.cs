using System.Web.Mvc;
using NHibernate;
using NUnit.Framework;
using SimpleCMS.Controllers;
using SimpleCMS.Tests.Data;

namespace SimpleCMS.Tests.Controllers
{
    [TestFixture]
    public class HomeControllerFixture
    {
        private ISession session;
        private HomeController controller;

        [SetUp]
        public void Setup()
        {
            session = DataHelper.GetSession();
            controller = new HomeController(session);
        }

        [Test]
        public void Index_ShouldRenderSuccessful()
        {
            var result = (ViewResult)controller.Index();

            var model = result.ViewData.Model;
            Assert.IsNotNull(model);
        }
    }
}
