using Spectre.Console.Cli;

namespace Shops.Commands
{
    public class AddShopCommandSettings : CommandSettings
    {
        [CommandArgument(0, "[shopName]")]
        public string ShopName { get; init; }

        [CommandArgument(1, "[shopAddress]")]
        public string ShopAddress { get; init; }
    }
}