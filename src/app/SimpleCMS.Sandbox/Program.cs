using System.Configuration;
using FluentNHibernate.Cfg.Db;
using SimpleCMS.Data;

namespace SimpleCMS.Sandbox
{
    public class Program
    {
        static void Main(string[] args)
        {
            var connectionString = ConfigurationManager.AppSettings["sql_lite"];
            var dataSession = new DataSession(SQLiteConfiguration
                .Standard
                .ConnectionString(connectionString));
            var sessionFactory = dataSession.SessionFactory;
            var session = sessionFactory.OpenSession();

            dataSession.BuildSchema(session);
        }
    }
}
