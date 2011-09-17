using NHibernate;

namespace SimpleCMS.Core.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        public ISession CurrentSession { get; private set; }
        private readonly ITransaction transaction;

        public UnitOfWork(ISession session) {
            CurrentSession = session;
            transaction = CurrentSession.BeginTransaction();
        }

        public void Dispose() {
            CurrentSession.Close();
            CurrentSession = null;
        }

        public void Commit() {
            transaction.Commit();
        }
    }
}