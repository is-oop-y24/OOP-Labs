using Spectre.Console.Cli;

namespace Shops.Commands
{
    public class AddToPurchaseCommandSettings : CommandSettings
    {
        [CommandArgument(0, "[productId]")]
        public int ProductId { get; init; }

        [CommandArgument(1, "[quantity]")]
        public int Quantity { get; init; }
    }
}