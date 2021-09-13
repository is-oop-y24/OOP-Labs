using Microsoft.Extensions.DependencyInjection;

namespace Shops
{
    internal class Program
    {
        private static void Main()
        {
            var services = new ServiceCollection();
            services.AddSingleton<IShopManager, ShopManager>();
            new Client(services).Run();
        }
    }
}
