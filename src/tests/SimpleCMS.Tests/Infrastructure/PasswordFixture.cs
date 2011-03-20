using NUnit.Framework;
using SimpleCMS.Infrastructure;

namespace SimpleCMS.Tests.Infrastructure
{
    [TestFixture]
    public class PasswordFixture
    {
        [Test]
        public void GenerateSalt_ShouldBeRandom()
        {
            var salt1 = Password.GenerateSalt(30);
            var salt2 = Password.GenerateSalt(30);

            Assert.That(salt1, Is.Not.EqualTo(salt2));
        }

        [Test]
        public void GenerateSaltedPassword_ShouldReturnTheSameResult()
        {
            const string password = "the password";
            var salt = Password.GenerateSalt(30);

            var saltedPassword1 = Password.GenerateSaltedPassword(password, salt);
            var saltedPassword2 = Password.GenerateSaltedPassword(password, salt);

            Assert.That(saltedPassword1, Is.EqualTo(saltedPassword2));
        }
    }
}
