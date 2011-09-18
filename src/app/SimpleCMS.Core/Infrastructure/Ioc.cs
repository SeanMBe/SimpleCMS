using System;
using NHibernate;
using SimpleCMS.Core.Data;
using StructureMap;

namespace SimpleCMS.Core.Infrastructure
{
    public class Ioc
    {
        private static IContainer container;

        public static void BuildContainer()
        {
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

            container = ObjectFactory.Container;
        }

        public static void RegisterType<T1, T2>() {
            container.Configure(x => x.For(typeof(T1)).Use(typeof(T2)));
        }

        public static T Resolve<T>() {
            return container.GetInstance<T>();
        }

        public static object Resolve(Type T) {
            return container.GetInstance(T);
        }

        public static void EndRequest()
        {
            ObjectFactory.ReleaseAndDisposeAllHttpScopedObjects();
        }

        public static void Dispose() {
            container.Dispose();
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