using System.Web.Mvc;
using System.Web.Routing;
using MvcContrib.TestHelper;
using NUnit.Framework;
using SimpleCMS.Controllers;
using SimpleCMS.Core.Models;
using SimpleCMS.Infrastructure;

namespace SimpleCMS.Tests.Infrastructure {
    [TestFixture]
    public class RoutesFixture {
        public RoutesFixture() {
            BootStrap.RegisterRoutes();
            RouteTable.Routes.WriteRoutesToConsole();
        }

        [Test]
        public void Root_ShouldReturnAllPosts() {
            "~/".WithMethod(HttpVerbs.Get)
                .ShouldMapTo<PostsController>(x => x.Show());
        }

        //Method    Path 	        Endpoint (controller#action)
        //GET       session 	    sessions#show
        //POST 	    session 	    sessions#create
        //GET 	    session/new 	sessions#new
        //GET 	    session/edit 	sessions#edit
        //PUT 	    session 	    sessions#update
        //DELETE 	session 	    sessions#destroy 

        [Test]
        public void GetPost_ShouldMapToShow() {
            "~/post"
                .WithMethod(HttpVerbs.Get)
                .ShouldMapTo<PostsController>(x => x.Show());
        }

        [Test]
        public void PostPost_ShouldMapToCreate() {
            var post = new Post();
            "~/post"
                .WithMethod(HttpVerbs.Post)
                .WithValue("post", post)
                .WithValue("authorId", 1)
                .ShouldMapTo<PostsController>(c => c.Create(post, 1));
        }

        [Test]
        public void GetNewPost_ShouldMapToNew() {
            "~/post/new"
                .WithMethod(HttpVerbs.Get)
                .ShouldMapTo<PostsController>(x => x.New());
        }

        [Test]
        public void GetEditPost_ShouldMapToEdit() {
            "~/post/edit"
                .WithMethod(HttpVerbs.Get)
                .WithValue("id", 1)
                .ShouldMapTo<PostsController>(x => x.Edit(1));
        }

        [Test]
        public void PutPost_ShouldMapToUpdate() {
            var post = new Post();
            "~/post"
                .WithMethod(HttpVerbs.Put)
                .WithValue("post", post)
                .WithValue("authorId", 1)
                .ShouldMapTo<PostsController>(c => c.Update(post, 1));
        }

        [Test]
        public void DeletePost_ShouldMapToDestroy() {
            "~/post".WithMethod(HttpVerbs.Delete)
                .WithValue("id", 1)
                .ShouldMapTo<PostsController>(x => x.Destroy(1));
        }
    }

    public static class RouteDataExtension {
        public static RouteData WithValue(this RouteData routeData, string fieldName, object value) {
            routeData.Values[fieldName] = value;
            return routeData;
        }
    }
}