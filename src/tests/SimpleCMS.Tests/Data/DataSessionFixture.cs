using NUnit.Framework;
using SimpleCMS.Data;

namespace SimpleCMS.Tests.Data
{
    [TestFixture]
    public class DataSessionFixture
    {
        [Test]
        public void Constructor_ShouldSetConfigurationAndSessionFactory()
        {
            var dataSession = DataSession.InMemoryDataSession();

            Assert.That(dataSession.Configuration, Is.Not.Null);
            Assert.That(dataSession.SessionFactory, Is.Not.Null);
        }
    }
}
