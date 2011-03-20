using System.Configuration;
using System.Web.Mvc;
using Castle.Core;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using SimpleCMS.Data;

namespace SimpleCMS.Infrastructure
{
    public class ComponentsInstaller : IWindsorInstaller
    {
        public static DataSession GetDataSession()
        {
            var connectionString = ConfigurationManager.AppSettings["sql_lite"];
            return new DataSession(SQLiteConfiguration
                .Standard
                .ConnectionString(connectionString));
        }

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            // Do not change this ordering
            container.Register(AllTypes
                .FromThisAssembly()
                .BasedOn<IController>()
                .If(t => t.Name.EndsWith("Controller"))
                .Configure(c => c.LifeStyle.Transient));

            container.Register(AllTypes
                .FromThisAssembly()
                .Pick()
                .Configure(c => c.LifeStyle.Is(LifestyleType.Transient))
                .WithService.FirstInterface());

            container.Register(Component
                .For<ISessionFactory>()
                .Instance(GetDataSession().SessionFactory)
                .LifeStyle.Singleton);

            container.Register(Component
                .For<ISession>()
                .UsingFactoryMethod(k => k.Resolve<ISessionFactory>().OpenSession())
                .LifeStyle.PerWebRequest);
        }
    }
}