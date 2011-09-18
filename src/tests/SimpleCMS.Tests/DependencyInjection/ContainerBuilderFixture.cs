using NUnit.Framework;
using SimpleCMS.Core;
using SimpleCMS.Core.Controllers;
using StructureMap;

namespace SimpleCMS.Tests.DependencyInjection
{
    [TestFixture]
    public class ContainerBuilderFixture {
        public ContainerBuilderFixture() {
            Ioc.BuildContainer();
        }

        [Test]
        public void GetInstance_ShouldResolveController()
        {
            var postsController = ObjectFactory.GetInstance<PostsController>();
            Assert.That(postsController, Is.Not.Null);
        }
    }
}