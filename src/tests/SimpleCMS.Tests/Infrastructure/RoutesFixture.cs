using System.Web.Mvc;
using MvcContrib.TestHelper;
using NUnit.Framework;
using SimpleCMS.Controllers;
using SimpleCMS.Infrastructure;

namespace SimpleCMS.Tests.Infrastructure
{
    [TestFixture]
    public class RoutesFixture {
        public RoutesFixture()
        {
            BootStrap.RegisterRoutes();
        }

        [Test]
        public void Root_ShouldReturnAllPosts() {
            var route = "~/".WithMethod(HttpVerbs.Get);
            route.ShouldMapTo<PostsController>(x => x.Show());
        }

        //Method    Path 	        Endpoint (controller#action)
        //GET       session 	    sessions#show
        //POST 	    session 	    sessions#create
        //GET 	    session/new 	sessions#new
        //GET 	    session/edit 	sessions#edit
        //PUT 	    session 	    sessions#update
        //DELETE 	session 	    sessions#destroy 

        [Test]
        public void Posts_Show()
        {
            var route = "~/post".WithMethod(HttpVerbs.Get);
            route.ShouldMapTo<PostsController>(x => x.Show());
        }

        [Test]
        public void Posts_Create() {
            var route = "~/post".WithMethod(HttpVerbs.Post);
            route.Values["post"] = null;
            route.Values["authorId"] = 0;
            route.ShouldMapTo<PostsController>(c => c.Create(null, 0));
        }

        [Test]
        public void Posts_New() {
            var route = "~/post/new".WithMethod(HttpVerbs.Get);
            route.ShouldMapTo<PostsController>(x => x.New());
        }

        [Test]
        public void Posts_Edit() {
            var route = "~/post/edit".WithMethod(HttpVerbs.Get);
            route.Values["id"] = 0;
            route.ShouldMapTo<PostsController>(x => x.Edit(0));
        }

        [Test]
        public void Posts_Update() {
            var route = "~/post".WithMethod(HttpVerbs.Put);
            route.Values["post"] = null;
            route.Values["authorId"] = 0;
            route.ShouldMapTo<PostsController>(x => x.Update(null, 0));
        }

        [Test]
        public void Posts_Destroy() {
            var route = "~/post".WithMethod(HttpVerbs.Delete);
            route.Values["id"] = 0;
            route.ShouldMapTo<PostsController>(x => x.Destroy(0));
        }
    }
}