using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using SimpleCMS.Core.Models;

namespace SimpleCMS.Core.Data
{
    public class DataProvider
    {
        public Configuration Configuration { get; private set; }
        public ISessionFactory SessionFactory { get; private set; }

        public static DataProvider InMemoryDataSession()
        {
            return new DataProvider(SQLiteConfiguration.Standard.InMemory());
        }

        public static DataProvider FileDataSession()
        {
            return new DataProvider(SQLiteConfiguration
                        .Standard
                        .ConnectionString(c => c.FromConnectionStringWithKey("db_connection")));
        }

        public static DataProvider MySqlDataSession()
        {
            return new DataProvider(MySQLConfiguration
                        .Standard
                        .ConnectionString(c => c.FromConnectionStringWithKey("db_connection")));
        }

        protected DataProvider(IPersistenceConfigurer dbType)
        {
            var cfg = Fluently.Configure();
            cfg.Database(dbType);
            cfg.Mappings(m => m.FluentMappings.AddFromAssemblyOf<DataModel>());
            cfg.ExposeConfiguration(c =>
                                        {
                                            c.Properties.Add("hbm2ddl.keywords", "none"); //fix for mysql
                                            Configuration = c;
                                        });

            SessionFactory = cfg.BuildSessionFactory();
        }

        public ISession BuildSchema()
        {
            var session = SessionFactory.OpenSession();
            new SchemaExport(Configuration)
                .Execute(
                    script: true,
                    export: true,
                    justDrop: false,
                    connection: session.Connection,
                    exportOutput: null);
            session.Clear();

            return session;
        }

        public void ExportSchema(string exportFilePath)
        {
            new SchemaExport(Configuration)
                .SetOutputFile(exportFilePath)
                .Execute(script: true, export: true, justDrop: false);
        }
    }
}