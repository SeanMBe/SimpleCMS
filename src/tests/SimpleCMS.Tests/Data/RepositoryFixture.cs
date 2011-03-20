using NHibernate;
using NUnit.Framework;
using SimpleCMS.Data;
using SimpleCMS.Models;

namespace SimpleCMS.Tests.Data
{
    [TestFixture]
    public class RepositoryFixture
    {
        private ISession session;
        private IRepository repository;

        [SetUp]
        public void Setup()
        {
            session = DataHelper.GetSession();
            repository = new Repository(session);
        }

        [Test]
        public void Find_ShouldFindTheRecordById()
        {
            var original = new User();
            repository.Save(original);
            session.Clear();

            var record = repository.Find<User>(1);
            Assert.IsTrue(record.Equals(original));
        }

        [Test]
        public void Find_ShouldFindTheRecordByCriteria()
        {
            var original = new User();
            repository.Save(original);
            session.Clear();

            var record = repository.Find<User>(x => x.Id == 1);
            Assert.IsTrue(record.Equals(original));
        }

        [Test]
        public void FindAll_ShouldReturnAllRecords()
        {
            repository.Save(new User());
            repository.Save(new User());
            repository.Save(new User());
            session.Clear();

            var recordCount = repository.FindAll<User>().Count;
            Assert.That(recordCount, Is.EqualTo(3));
        }

        [Test]
        public void FindAll_ShouldFindRecordsByCriteria()
        {
            var original = new User();
            repository.Save(original);
            session.Clear();

            var record = repository.FindAll<User>(x => x.Id == 1);
            Assert.IsTrue(record[0].Equals(original));
        }

        //TODO: Test the rest
        //IList<T> FindAllAscending(Expression<Func<T, bool>> criteria, Expression<Func<T, object>> projection);
        //IList<T> FindAllDescending(Expression<Func<T, bool>> criteria, Expression<Func<T, object>> projection);
        //void Save(T item);
        //void Delete(int id);
        //void Delete(T entity);
    }
}