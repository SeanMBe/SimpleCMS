using System.Collections.Generic;
using System.Web.Mvc;
using NUnit.Framework;
using Rhino.Mocks;
using SimpleCMS.Controllers;
using SimpleCMS.Core.Data;
using SimpleCMS.Core.Models;

namespace SimpleCMS.Tests.Controllers
{
    [TestFixture]
    public class HomeControllerFixture
    {
        private IRepository repository;
        private HomeController controller;

        [SetUp]
        public void Setup()
        {
            repository = MockRepository.GenerateStub<IRepository>();
            controller = new HomeController(repository);
        }

        [Test]
        public void Index_ShouldRenderSuccessful()
        {
            var posts = new List<Post>();
            repository.Stub(x => x.FindAll<Post>(post => post.CreatedDate)).IgnoreArguments().Return(posts);

            var result = controller.Index() as ViewResult;

            Assert.IsTrue(result.Model.Equals(posts));
        }
    }
}
