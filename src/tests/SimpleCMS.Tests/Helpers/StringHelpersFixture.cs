using NUnit.Framework;
using SimpleCMS.Helpers;

namespace SimpleCMS.Tests.Helpers
{
    [TestFixture]
    public class StringHelpersFixture
    {
        [Test]
        public void Ellipsify_ShouldTruncateTextAndIncludedEllipses()
        {
            const string myString = "123456789";

            var result = myString.Ellipsify(2);

            Assert.That(result, Is.EqualTo("12..."));
        }
    }
}
