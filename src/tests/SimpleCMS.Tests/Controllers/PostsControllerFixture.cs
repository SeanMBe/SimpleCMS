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
    public class PostsControllerFixture
    {
        private IRepository repository;
        private PostsController controller;

        [SetUp]
        public void Setup()
        {
            repository = MockRepository.GenerateStub<IRepository>();
            controller = new PostsController(repository);
        }

        [Test]
        public void Index_ShouldReturnAllPosts()
        {
            var allPosts = new List<Post>();
            repository.Stub(p => p.FindAll<Post>()).Return(allPosts);

            var result = controller.Index();

            Assert.IsTrue(result.ViewData.Model.Equals(allPosts));
        }

        [Test]
        public void Create_ShouldIncludeAuthorsInViewBag()
        {
            var allAccounts = new List<Account>();
            repository.Stub(p => p.FindAll<Account>()).Return(allAccounts);

            var result = (ViewResult)controller.New();

            Assert.IsTrue(result.ViewBag.Authors.Equals(allAccounts));
        }

        [Test]
        public void Create_ShouldFindAndSetTheAuthor()
        {
            const int authorId = 1;
            var post = new Post();
            repository.Stub(p => p.Find<Account>(x => true)).IgnoreArguments().Return(new Account());
            repository.Stub(p => p.Save(post)).Return(post);

            var result = controller.Create(post, authorId);

            Assert.IsTrue(controller.ModelState.IsValid);
            Assert.That(result.View(), Is.EqualTo("Show"));
        }

        [Test]
        public void Create_ShouldValidateThePost()
        {
            const int authorId = 1;
            var post = new Post ();
            controller.SetupContext(post);

            controller.Create(post, authorId);

            Assert.IsFalse(controller.ModelState.IsValid);
        }

        [Test]
        public void Edit_ShouldIncludeAuthorsInViewBagAndSetPost()
        {
            const int postId = 1;
            var post = new Post();
            var allAccounts = new List<Account>();
            repository.Stub(p => p.FindAll<Account>()).Return(allAccounts);
            repository.Stub(p => p.Find<Post>(x => true)).IgnoreArguments().Return(post);

            var result = (ViewResult)controller.Edit(postId);

            Assert.IsTrue(result.ViewBag.Authors.Equals(allAccounts));
            Assert.IsTrue(result.Model.Equals(post));
        }

        [Test]
        public void Edit_ShouldFindAndSetTheAuthor()
        {
            const int authorId = 1;
            var post = new Post();
            repository.Stub(p => p.Find<Account>(x => true)).IgnoreArguments().Return(new Account());
            repository.Stub(p => p.Save(post)).Return(post);

            var result = controller.Update(post, authorId);

            Assert.IsTrue(controller.ModelState.IsValid);
            Assert.That(result.View(), Is.EqualTo("Show"));
        }

        [Test]
        public void Edit_ShouldValidateThePost()
        {
            const int authorId = 1;
            var post = new Post();
            controller.SetupContext(post);

            controller.Update(post, authorId);

            Assert.IsFalse(controller.ModelState.IsValid);
        }

        [Test]
        public void Delete_ShouldRemoveThePostById()
        {
            const int postId = 1;
            repository.Stub(p => p.Delete<Post>(postId));
            var result = controller.Destroy(postId);

            Assert.That(result.View(), Is.EqualTo("Show"));
        }
    }
}
