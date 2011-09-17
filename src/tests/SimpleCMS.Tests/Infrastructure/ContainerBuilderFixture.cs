using NUnit.Framework;
using SimpleCMS.Controllers;
using SimpleCMS.Core.Logging;
using SimpleCMS.Infrastructure;
using StructureMap;

namespace SimpleCMS.Tests.Infrastructure
{
    [TestFixture]
    public class ContainerBuilderFixture {
        public ContainerBuilderFixture() {
            ContainerBuilder.Build();
        }

        [Test]
        public void GetInstance_ShouldResolveController()
        {
            var postsController = ObjectFactory.GetInstance<PostsController>();
            Assert.That(postsController, Is.Not.Null);
        }
    }
}