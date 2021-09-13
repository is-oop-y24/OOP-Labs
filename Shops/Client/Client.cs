using System;
using Microsoft.Extensions.DependencyInjection;
using Shops.Commands;
using Shops.Tools;
using Spectre.Console.Cli;

namespace Shops
{
    public class Client
    {
        private ServiceCollection _services;

        public Client(ServiceCollection services)
        {
            _services = services;
        }

        public void Run()
        {
            var registrar = new TypeRegistrar(_services);
            var app = new CommandApp(registrar);
            app.Configure(config =>
                {
                    config.AddCommand<AddShopCommand>("/add-shop");
                });
            app.Run(Console.ReadLine().Split());
        }
    }
}