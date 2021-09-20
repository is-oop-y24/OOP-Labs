using System;
using Shops.Tools;
using Spectre.Console.Cli;

namespace Shops.Commands
{
    public class MakeSupplyCommand : Command<MakeSupplyCommandSettings>
    {
        private IShopManager _shopManager;
        private IUserInterface _userInterface;

        public MakeSupplyCommand(IShopManager shopManager, IUserInterface userInterface)
        {
            _shopManager = shopManager;
            _userInterface = userInterface;
        }

        public override int Execute(CommandContext context, MakeSupplyCommandSettings settings)
        {
            Shop shop = _shopManager.GetShop(new ShopId(settings.ShopId));
            var supply = new Supply();
            supply.AddProduct(new ProductSupply(
                new ProductId(settings.ProductId), settings.Quantity, settings.ProductWorth));
            _userInterface.WriteLine($"Product {settings.ProductId} is successfully supplied to {shop.Name}.");
            return 0;
        }
    }
}