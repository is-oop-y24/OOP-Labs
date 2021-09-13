using Microsoft.Extensions.DependencyInjection;

namespace Shops
{
    internal class Program
    {
        private static void Main()
        {
            new Client(new ShopManager(), new ConsoleUI()).Run();
        }
    }
}
