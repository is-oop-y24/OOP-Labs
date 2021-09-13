using System;
using Microsoft.Extensions.DependencyInjection;
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
            
        }
        
    }
}