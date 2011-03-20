using FluentNHibernate.Cfg.Db;
using NHibernate;
using SimpleCMS.Data;

namespace SimpleCMS.Tests.Data
{
    public class DataHelper
    {
        protected static DataSession DataSession;
        protected static ISessionFactory SessionFactory;
        public static ISession GetSession()
        {
            if (DataSession == null)
                DataSession = new DataSession(SQLiteConfiguration.Standard.InMemory());
            if (SessionFactory == null)
                SessionFactory = DataSession.SessionFactory;
            var session = SessionFactory.OpenSession();
            DataSession.BuildSchema(session);
            return session;
        }
    }
}