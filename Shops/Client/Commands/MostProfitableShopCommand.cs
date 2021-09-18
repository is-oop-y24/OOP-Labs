using System;
using Shops.Tools;
using Spectre.Console.Cli;

namespace Shops.Commands
{
    public class MostProfitableShopCommand : Command<MostProfitableShopCommandSettings>
    {
        private readonly IShopManager _shopManager;
        private readonly IUserInterface _userInterface;
        private readonly Customer _customer;

        public MostProfitableShopCommand(IShopManager shopManager, IUserInterface userInterface, Customer customer)
        {
            _shopManager = shopManager;
            _userInterface = userInterface;
            _customer = customer;
        }

        public override int Execute(CommandContext context, MostProfitableShopCommandSettings settings)
        {
            try
            {
                Shop shop = _shopManager.GetMostProfitableShop(_customer.CurrentPurchase);
                _userInterface.WriteLine($"The most profitable shop is {shop.Name} with ID: {shop.Id}.");
            }
            catch (ShopManagerException e)
            {
                _userInterface.WriteError(e.Message);
            }
            return 0;
        }
    }
}