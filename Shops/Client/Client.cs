using System;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Shops.Commands;
using Shops.Tools;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Shops
{
    public class Client
    {
        public void Run()
        {
            var services = new ServiceCollection();
            services.AddScoped(typeof(IUserInterface), typeof(AnsiConsoleUI));
            services.AddSingleton(typeof(IShopManager), new ShopManager());
            services.AddSingleton(typeof(Customer), new Customer("Customer"));
            ServiceProvider provider = services.BuildServiceProvider();

            IUserInterface userInterface = provider.GetService<IUserInterface>();

            var registrar = new TypeRegistrar(services);
            var app = new CommandApp(registrar);
            app.Configure(config =>
                {
                    config.AddCommand<AddShopCommand>("/add-shop");
                    config.AddCommand<AddToPurchaseCommand>("/add-to-purchase");
                    config.AddCommand<MakeSupplyCommand>("/add-to-supply");
                    config.AddCommand<BuyCommand>("/buy");
                    config.AddCommand<MakeSupplyCommand>("/make-supply");
                    config.AddCommand<MostProfitableShopCommand>("/most-profitable-shop");
                    config.AddCommand<ProductListCommand>("/product-list");
                    config.AddCommand<PurchaseListCommand>("/purchase-list");
                    config.AddCommand<RegisterProductCommand>("/register-product");
                    config.AddCommand<ShopListCommand>("/shop-list");
                });

            while (true)
            {
                int executionResult = app.Run(Console.ReadLine().Split());
            }
        }
    }
}