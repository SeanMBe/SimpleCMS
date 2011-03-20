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
    }
}