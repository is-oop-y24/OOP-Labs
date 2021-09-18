using Spectre.Console.Cli;

namespace Shops.Commands
{
    public class BuyCommandSettings : CommandSettings
    {
        [CommandArgument(0, "[shopId]")] 
        public int ShopId { get; set; }
    }
}