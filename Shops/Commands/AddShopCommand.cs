using System;
using Spectre.Console.Cli;

namespace Shops.Commands
{
    public class AddShopCommand : Command<AddShopCommandSettings>
    {
        private readonly IShopManager _shopManager;

        public AddShopCommand(IShopManager shopManager)
        {
            _shopManager = shopManager;
        }

        public override int Execute(CommandContext context, AddShopCommandSettings settings)
        {
            _shopManager.CreateShop(settings.ShopName, new Address(settings.ShopAddress));
            return 0;
        }
    }
}