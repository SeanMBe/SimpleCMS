using System;
using System.Configuration;
using System.Linq;
using System.Web.Routing;
using NHibernate;
using SimpleCMS.Core.Data;
using SimpleCMS.Core.Models;
using SimpleCMS.Infrastructure;

namespace SimpleCMS.Sandbox
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\n\n");

            if (args.ToList().Contains("db"))
            {
                DatabaseCreate();
            }
            if (args.ToList().Contains("routes"))
            {
                DisplayRoutes();
            }
        }

        static void DisplayRoutes()
        {
            BootStrap.RegisterRoutes();
            RouteTable.Routes.WriteRoutes(Console.WriteLine);
        }

        static void DatabaseCreate()
        {
            Console.WriteLine("Executing database generation...");

            var dataProvider = DataProvider.File();
            var session = dataProvider.BuildSchema();
            SeedData(session);

            Console.WriteLine("Database generation complete");
        }

        static void SeedData(ISession session)
        {
            Console.WriteLine("Seeding data...");

            var repository = new Repository(session);
            var user = repository.Save(new Account { Email = "Tom Bombadil" });
            repository.Save(new Account { Email = "Bilbo Bagins" });
            const string body =
                @"Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";
            repository.Save(new Post { Title = "Sample Post", Body = body, Author = user });
        }
    }
}
