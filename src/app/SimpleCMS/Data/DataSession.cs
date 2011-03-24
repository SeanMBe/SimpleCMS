using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using SimpleCMS.Models;
using Configuration = NHibernate.Cfg.Configuration;

namespace SimpleCMS.Data
{
    public class DataSession
    {
        public Configuration Configuration { get; private set; }
        public ISessionFactory SessionFactory { get; private set; }

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

        protected DataSession(IPersistenceConfigurer dbType)
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

        /// <summary>
        /// </summary>
        /// <returns>Shared for testing purposes</returns>
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