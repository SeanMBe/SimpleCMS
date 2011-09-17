using NHibernate;
using SimpleCMS.Core;
using SimpleCMS.Core.Data;
using StructureMap;

namespace SimpleCMS.Infrastructure {
    public static class ContainerBuilder {
        public static void Build() {
            ObjectFactory.Initialize(x => {
                x.Scan(scanner => {
                    scanner.TheCallingAssembly();
                    scanner.WithDefaultConventions();
                });
                x.Scan(scanner => {
                    scanner.AssemblyContainingType<Repository>();
                    scanner.WithDefaultConventions();
                });
                x.For<ISessionFactory>()
                    .Singleton()
                    .Use(GetSessionFactory());
                x.For<ISession>()
                    .HybridHttpOrThreadLocalScoped()
                    .Use(y => y.GetInstance<ISessionFactory>().OpenSession());
            });
        }

        private static ISessionFactory GetSessionFactory() {
            var provider = DataProvider.InMemory();
            if (AppSettings.Environment.Release) {
                provider = DataProvider.MySql();
            }
            if (AppSettings.Environment.Debug) {
                provider = DataProvider.File();
            }
            return provider.SessionFactory;
        }
    }
}