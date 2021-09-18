using Spectre.Console.Cli;

namespace Shops.Commands
{
    public class AddToSupplyCommandSettings : CommandSettings
    {
        [CommandArgument(0, "[shopId]")]
        public int ShopId { get; init; }

        [CommandArgument(1, "[productId]")]
        public int ProductId { get; init; }

        [CommandArgument(2, "[productWorth]")]
        public int ProductWorth { get; init; }

        [CommandArgument(3, "[quantity]")]
        public int Quantity { get; init; }
    }
}