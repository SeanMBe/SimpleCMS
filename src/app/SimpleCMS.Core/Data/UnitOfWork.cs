using NHibernate;

namespace SimpleCMS.Core.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ISessionFactory sessionFactory;
        private readonly ITransaction transaction;

        public UnitOfWork(ISessionFactory sessionFactory) {
            this.sessionFactory = sessionFactory;
            CurrentSession = this.sessionFactory.OpenSession();
            transaction = CurrentSession.BeginTransaction();
        }

        public ISession CurrentSession { get; private set; }

        public void Dispose() {
            CurrentSession.Close();
            CurrentSession = null;
        }

        public void Commit() {
            transaction.Commit();
        }
    }
}