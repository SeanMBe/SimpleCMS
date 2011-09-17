//using System;
//using System.Collections.Generic;
//using System.Configuration;
//using System.IO;
//using FluentNHibernate.Cfg;
//using FluentNHibernate.Cfg.Db;
//using NHibernate;
//using NHibernate.Event;
//using Configuration = NHibernate.Cfg.Configuration;
//using Environment = NHibernate.Cfg.Environment;

//namespace SimpleCMS.Core.Data
//{
//    public class SessionFactoryConfigurator {
//        public enum Databases {
//            Phoenix16
//        }

//        public EmptyInterceptor DataInterceptor { get; set; }


//        public string ConnectionString { get; set; }
//        public string SqlServer { get; set; }
//        public string Database { get; set; }
//        public string Uid { get; set; }
//        public string Password { get; set; }
//        public ISessionFactory SessionFactory { get; set; }
//        private Configuration savedConfig;
//        public Configuration SavedConfig {
//            get { return savedConfig; }
//            set { savedConfig = value; }
//        }

//        private IPersistenceConfigurer DatabaseSetting { get; set; }
//        private readonly Action<MappingConfiguration> actionM = (m => {
//                                                                          m.HbmMappings.AddFromAssemblyOf<Environment>();
//                                                                          m.FluentMappings.AddFromAssemblyOf<Environment>();
//        });
//        private FluentConfiguration fluentConfiguration;

//        public SessionFactoryConfigurator() {
//            sendSqlToConsole = true;
//        }

//        public SessionFactoryConfigurator(EmptyInterceptor dataInterceptor)
//        {
//            DataInterceptor = dataInterceptor;
//        }

//        public void DisableSqlConsoleLogging() {
//            sendSqlToConsole = false;
//        }

//        protected SessionFactoryConfigurator(Databases database) {
//            Database = Enum.GetName(typeof(Databases), database);
//            SqlServer = ConfigurationManager.AppSettings.Get("ConnectionSQLServer");
//            Uid = ConfigurationManager.AppSettings.Get("ConnectionSQLuid");
//            Password = ConfigurationManager.AppSettings.Get("ConnectionSQLpwd");
            
//            ConnectionString = String.Format("Server={0}; User ID={1}; Password={2}; Database={3}", SqlServer, Uid, Password, Database);
//        }

//        public void UseFileDatabase() {
//            const string dbpath = @"C:\Temp\workflow_sqlite.sql";
//            Directory.CreateDirectory(Path.GetDirectoryName(dbpath));
//            DatabaseSetting = SQLiteConfiguration.Standard.ShowSql().UsingFile(dbpath);
//        }

//        public void UseInMemoryDatabase() {
//            DatabaseSetting = SQLiteConfiguration.Standard.ShowSql().InMemory();
//        }

//        public void InitDatabaseSettingsMsSql2008() {
//            DatabaseSetting = MsSqlConfiguration.MsSql2008.ConnectionString(ConnectionString);
//        }

//        public void InitSessionFactory() {
//            var additionalProperties = new Dictionary<string, string>{{"show_sql", "true"}};

//            fluentConfiguration = Fluently.Configure()
//                .Database(DatabaseSetting)
//                .Mappings(actionM)
//                .ExposeConfiguration(cfg => {
//                                                savedConfig = cfg;
//                                                savedConfig.AddProperties(additionalProperties);
//                                                savedConfig.EventListeners.PreInsertEventListeners = new IPreInsertEventListener[] {new AuditEventListener { AuditContext = SessionFactoryAuditContext}};
//                                                savedConfig.EventListeners.PreUpdateEventListeners = new IPreUpdateEventListener[] { new AuditEventListener { AuditContext = SessionFactoryAuditContext } };
//                });

//            SessionFactory = fluentConfiguration.BuildSessionFactory();  //_savedConfig set when this is called.
//        }

//        public void InitSessionFactory(Action<Configuration> exposeConfiguration) {
//            fluentConfiguration = Fluently.Configure().Database(DatabaseSetting).Mappings(actionM).ExposeConfiguration(exposeConfiguration);
//            SessionFactory = fluentConfiguration.BuildSessionFactory();  //_savedConfig set when this is called.
//        }

//        public Dictionary<Type, Func<ISessionFactory>> Actions = null;
//        private bool sendSqlToConsole;

//        private ISessionFactory CreateNewSessionFactoryForDatabase(Databases dbName) {
//            var sessionFactoryConfigurator = new SessionFactoryConfigurator(dbName);
//            sessionFactoryConfigurator.InitDatabaseSettingsMsSql2008();
//            sessionFactoryConfigurator.InitSessionFactory();
//            return sessionFactoryConfigurator.SessionFactory;
//        }

//        public ISessionFactory Phoenix16SessionFactory() {
//            return BuildSessionFactory("AppConnectionString");
//        }

//        private ISessionFactory BuildSessionFactory(string connectionStringKey) {
//            var additionalProperties = new Dictionary<string, string> {
//                                                                          {"show_sql", sendSqlToConsole ? "true" : "false"}, 
//                                                                          {"current_session_context_class", ConfigurationManager.AppSettings["hibernate_session_context_class"]},
//                                                                          {"session_factory_name", "Workflow"}
//                                                                      };

//            return Fluently.Configure()
//                .Database(MsSqlConfiguration.MsSql2008
//                              .ConnectionString(c => c.FromConnectionStringWithKey(connectionStringKey)))
//                .Mappings(m =>{
//                                  m.HbmMappings.AddFromAssemblyOf<Environment>();
//                                  m.FluentMappings.AddFromAssemblyOf<WorkflowDefinition>();
//                })
//                .ExposeConfiguration(cfg => {
//                                                if (DataInterceptor != null)
//                                                {
//                                                    cfg.SetInterceptor(DataInterceptor);
//                                                }
//                                                cfg.AddProperties(additionalProperties);
//                                                cfg.EventListeners.PreInsertEventListeners = new IPreInsertEventListener[] { new AuditEventListener { AuditContext = SessionFactoryAuditContext } };
//                                                cfg.EventListeners.PreUpdateEventListeners = new IPreUpdateEventListener[] { new AuditEventListener { AuditContext = SessionFactoryAuditContext } };
//                }).BuildSessionFactory();
//        }

//        public ISessionFactory LocalPhoenix16SessionFactory(){
//            return BuildSessionFactory("LocalAppConnectionString");
//        }

//        public class Dummy
//        {

//        }

//        public ISessionFactory PanaceaSessionFactory()
//        {

//            var fluent = Fluently.Configure()

//                .Database(MsSqlConfiguration.MsSql2008.ConnectionString(c => c.FromAppSetting("Main.ConnectionString")))

//                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Dummy>())

//                .ExposeConfiguration(cfg =>
//                                         {
//                                             cfg.SetProperty("current_session_context_class", "thread_static");// "web" 

//                                         });


//            return fluent.BuildSessionFactory();
//        }
//    }
//}