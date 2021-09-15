using System;
using Spectre.Console.Cli;

namespace Shops.Commands
{
    public class ProductListCommand : Command<ProductListCommandSettings>
    {
        private readonly IShopManager _shopManager;
        private readonly IUserInterface _userInterface;

        public ProductListCommand(IShopManager shopManager, IUserInterface userInterface)
        {
            _shopManager = shopManager;
            _userInterface = userInterface;
        }

        public override int Execute(CommandContext context, ProductListCommandSettings settings)
        {
            throw new NotImplementedException();
        }
    }
}