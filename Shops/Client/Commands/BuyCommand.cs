using System;
using Shops.Tools;
using Spectre.Console.Cli;

namespace Shops.Commands
{
    public class BuyCommand : Command<BuyCommandSettings>
    {
        private readonly IShopManager _shopManager;
        private readonly IUserInterface _userInterface;
        private readonly Customer _customer;

        public BuyCommand(IShopManager shopManager, IUserInterface userInterface, Customer customer)
        {
            _shopManager = shopManager;
            _userInterface = userInterface;
            _customer = customer;
        }

        public override int Execute(CommandContext context, BuyCommandSettings settings)
        {
            Shop shop = _shopManager.GetShop(new ShopId(settings.ShopId));
            shop.BuyProducts(_customer.CurrentPurchase);
            _userInterface.WriteLine($"Purchase successfully made.\n Your balance now {_customer.Balance}");

            return 0;
        }
    }
}