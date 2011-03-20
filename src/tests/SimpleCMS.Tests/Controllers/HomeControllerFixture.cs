using System.Collections.Generic;
using System.Web.Mvc;
using Moq;
using NUnit.Framework;
using SimpleCMS.Controllers;
using SimpleCMS.Data;
using SimpleCMS.Models;

namespace SimpleCMS.Tests.Controllers
{
    [TestFixture]
    public class HomeControllerFixture
    {
        private Mock<IRepository> repository;
        private HomeController controller;

        [SetUp]
        public void Setup()
        {
            repository = new Mock<IRepository>();
            controller = new HomeController(repository.Object);
        }

        [Test]
        public void Index_ShouldRenderSuccessful()
        {
            var posts = new List<Post>();
            repository
                .Setup(x => x.FindAll<Post>(post => post.CreatedDate, true))
                .Returns(posts);

            var result = (ViewResult)controller.Index();

            var model = result.ViewData.Model;
            Assert.IsNotNull(model);
        }
    }
}
