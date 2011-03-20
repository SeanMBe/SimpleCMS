using Moq;
using NUnit.Framework;
using SimpleCMS.Data;
using SimpleCMS.Services;

namespace SimpleCMS.Tests.Services
{
    [TestFixture]
    public class UserFixture
    {
        private Mock<IRepository> repository;
        private AccountService accountService;

        [SetUp]
        public void Setup()
        {
            repository = new Mock<IRepository>();
            accountService = new AccountService(repository.Object);
        }

        [Test]
        public void GenerateSalt_ShouldBeRandom()
        {
            var salt1 = accountService.GenerateSalt(30);
            var salt2 = accountService.GenerateSalt(30);

            Assert.That(salt1, Is.Not.EqualTo(salt2));
        }

        [Test]
        public void GenerateSaltedPassword_ShouldReturnTheSameResult()
        {
            const string password = "the password";
            var salt = accountService.GenerateSalt(30);

            var saltedPassword1 = accountService.GenerateSaltedPassword(password, salt);
            var saltedPassword2 = accountService.GenerateSaltedPassword(password, salt);

            Assert.That(saltedPassword1, Is.EqualTo(saltedPassword2));
        }
    }
}
