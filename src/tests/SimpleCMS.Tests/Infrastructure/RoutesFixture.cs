using System;
using System.Web.Mvc;
using System.Web.Routing;
using MvcContrib.TestHelper;
using NUnit.Framework;
using SimpleCMS.Core.Controllers;
using SimpleCMS.Core.Infrastructure;
using SimpleCMS.Core.Models;

namespace SimpleCMS.Tests.Infrastructure {
    [TestFixture]
    public class RoutesFixture {
        public RoutesFixture() {
            BootStrap.RegisterRoutes();
            RouteTable.Routes.WriteRoutes(Console.WriteLine);
        }

        [Test]
        public void Root_ShouldReturnHomeIndex() {
            "~/".WithMethod(HttpVerbs.Get)
                .ShouldMapTo<HomeController>(x => x.Index());
        }

        [Test]
        public void GetPosts_ShouldMapToIndex() {
            "~/posts"
                .WithMethod(HttpVerbs.Get)
                .ShouldMapTo<PostsController>(x => x.Index());
        }

        [Test]
        public void PostPosts_ShouldMapToCreate() {
            var post = new Post();
            "~/posts"
                .WithMethod(HttpVerbs.Post)
                .WithValue("post", post)
                .WithValue("authorId", 1)
                .ShouldMapTo<PostsController>(c => c.Create(post, 1));
        }

        [Test]
        public void GetPostsNew_ShouldMapToNew() {
            "~/posts/new"
                .WithMethod(HttpVerbs.Get)
                .ShouldMapTo<PostsController>(x => x.New());
        }

        [Test]
        public void GetEditPost_ShouldMapToEdit() {
            "~/posts/{id}/edit"
                .WithMethod(HttpVerbs.Get)
                .WithValue("id", 1)
                .ShouldMapTo<PostsController>(x => x.Edit(1));
        }

        [Test]
        public void GetPost_ShouldMapToShow() {
            "~/posts/{id}"
                .WithMethod(HttpVerbs.Get)
                .WithValue("id", 1)
                .ShouldMapTo<PostsController>(x => x.Show(1));
        }

        [Test]
        public void PutPost_ShouldMapToUpdate() {
            var post = new Post();
            "~/posts/{id}"
                .WithMethod(HttpVerbs.Put)
                .WithValue("post", post)
                .WithValue("authorId", 1)
                .ShouldMapTo<PostsController>(c => c.Update(post, 1));
        }

        [Test]
        public void DeletePost_ShouldMapToDestroy() {
            "~/posts/{id}"
                .WithMethod(HttpVerbs.Delete)
                .WithValue("id", 1)
                .ShouldMapTo<PostsController>(x => x.Destroy(1));
        }
    }
}