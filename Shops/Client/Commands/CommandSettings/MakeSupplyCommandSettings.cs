using Spectre.Console.Cli;

namespace Shops.Commands
{
    public class MakeSupplyCommandSettings : CommandSettings
    {
        [CommandArgument(0, "[shopName]")]
        public int ShopId { get; init; }
    }
}