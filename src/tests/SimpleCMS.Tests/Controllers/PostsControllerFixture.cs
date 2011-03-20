using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using SimpleCMS.Controllers;
using SimpleCMS.Data;
using SimpleCMS.Models;

namespace SimpleCMS.Tests.Controllers
{
    [TestFixture]
    public class PostsControllerFixture
    {
        private Mock<IRepository> repository;
        private PostsController controller;

        [SetUp]
        public void Setup()
        {
            repository = new Mock<IRepository>();
            controller = new PostsController(repository.Object);
        }

        [Test]
        public void Index_ShouldReturnAllPosts()
        {
            var allPosts = new List<Post>();
            repository.Setup(x => x.FindAll<Post>()).Returns(allPosts);

            var result = controller.Index();

            Assert.IsTrue(result.ViewData.Model.Equals(allPosts));
        }

        //TODO: Test Create, Create, Edit, Edit, Delete
    }
}
