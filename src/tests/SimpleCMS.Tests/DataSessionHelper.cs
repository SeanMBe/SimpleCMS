using NHibernate;
using SimpleCMS.Data;

namespace SimpleCMS.Tests
{
    public class DataSessionHelper
    {
        protected static DataSession DataSession;
        protected static ISessionFactory SessionFactory;
        public static ISession GetSession()
        {
            if (DataSession == null)
                DataSession = DataSession.InMemoryDataSession();
            if (SessionFactory == null)
                SessionFactory = DataSession.SessionFactory;
            return DataSession.BuildSchema();
        }
    }
}