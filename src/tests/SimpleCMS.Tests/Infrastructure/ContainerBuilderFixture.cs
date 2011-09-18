using System;
using NUnit.Framework;
using SimpleCMS.Core.Controllers;
using SimpleCMS.Core.Infrastructure;
using StructureMap;

namespace SimpleCMS.Tests.Infrastructure
{
    [TestFixture]
    public class ContainerBuilderFixture {
        public ContainerBuilderFixture() {
            Ioc.BuildContainer();
            Console.WriteLine(ObjectFactory.WhatDoIHave());
        }

        [Test]
        public void GetInstance_ShouldResolveController()
        {
            var postsController = ObjectFactory.GetInstance<PostsController>();
            Assert.That(postsController, Is.Not.Null);
        }
    }
}