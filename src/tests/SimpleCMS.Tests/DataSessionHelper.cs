using NHibernate;
using SimpleCMS.Core.Data;

namespace SimpleCMS.Tests
{
    public class DataSessionHelper
    {
        protected static DataProvider DataProvider;
        protected static ISessionFactory SessionFactory;
        public static ISession GetSession()
        {
            if (DataProvider == null)
                DataProvider = DataProvider.InMemoryDataSession();
            if (SessionFactory == null)
                SessionFactory = DataProvider.SessionFactory;
            return DataProvider.BuildSchema();
        }
    }
}