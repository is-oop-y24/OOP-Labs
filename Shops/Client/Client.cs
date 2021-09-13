using System;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Shops.Commands;
using Shops.Tools;
using Spectre.Console.Cli;

namespace Shops
{
    public class Client
    {
        private ServiceCollection _services = new ServiceCollection();

        public Client(IShopManager shopManager, IUserInterface userInterface)
        {
            _services.Add(new ServiceDescriptor(
                typeof(IShopManager),
                shopManager.GetType(),
                ServiceLifetime.Singleton));

            _services.Add(new ServiceDescriptor(
                typeof(IUserInterface),
                userInterface.GetType(),
                ServiceLifetime.Singleton));
        }

        public void Run()
        {
            var registrar = new TypeRegistrar(_services);
            var app = new CommandApp(registrar);
            app.Configure(config =>
                {
                    config.AddCommand<AddShopCommand>("/add-shop");
                });

            int executionResult = 0;
            while (executionResult == 0)
                executionResult = app.Run(Console.ReadLine().Split());
        }
    }
}