using Spectre.Console.Cli;

namespace Shops.Commands
{
    public class RegisterProductCommandSettings : CommandSettings
    {
        [CommandArgument(0, "[productName]")]
        public string ProductName { get; init; }
    }
}