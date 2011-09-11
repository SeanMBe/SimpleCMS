using System.Configuration;
using System.Web.Mvc;
using Castle.Core;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using NHibernate;
using SimpleCMS.Core.Data;

namespace SimpleCMS.Infrastructure
{
    public class ComponentsInstaller : IWindsorInstaller
    {
        public static DataProvider GetDataSession()
        {
            var environment = ConfigurationManager.AppSettings["Environment"];

            if (string.IsNullOrEmpty(environment))
                return DataProvider.FileDataSession();

            switch (environment)
            {
                case "Release":
                    return DataProvider.MySqlDataSession();
                default:
                    return DataProvider.FileDataSession();
            }
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

            container.Register(AllTypes
                .FromAssemblyContaining<Repository>()
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