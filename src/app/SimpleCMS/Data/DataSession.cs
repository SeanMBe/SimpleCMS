using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using SimpleCMS.Models;

namespace SimpleCMS.Data
{
    public class DataSession
    {
        public Configuration Configuration { get; private set; }
        public ISessionFactory SessionFactory { get; private set; }

        public DataSession(IPersistenceConfigurer dbType)
        {
            SessionFactory = Fluently
                .Configure()
                .Database(dbType)
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<DataModel>())
                .ExposeConfiguration(c => c.Properties.Add("hbm2ddl.keywords", "none")) //mysql
                .ExposeConfiguration(c => c.Properties.Add("show_sql", "false"))
                .ExposeConfiguration(cfg => Configuration = cfg)
                .BuildSessionFactory();
        }

        /// <summary>
        /// Session is required because we want to ensure
        /// that we are using the same connection
        /// </summary>
        public void BuildSchema(ISession session)
        {
            var export = new SchemaExport(Configuration);
            export.Execute(true, true, false, session.Connection, null);
        }

        public static DataSession InMemoryDataSession()
        {
            return new DataSession(SQLiteConfiguration.Standard.InMemory());
        }

        public static DataSession FileDataSession()
        {
            return new DataSession(SQLiteConfiguration
                        .Standard
                        .ConnectionString(c => c.FromConnectionStringWithKey("db_connection")));
        }

        public static DataSession MySqlDataSession()
        {
            return new DataSession(MySQLConfiguration
                        .Standard
                        .ConnectionString(c => c.FromConnectionStringWithKey("db_connection")));
        }
    }
}