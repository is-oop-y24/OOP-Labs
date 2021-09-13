using System;
using Spectre.Console.Cli;

namespace Shops.Commands
{
    public class AddShopCommand : Command<AddShopCommandSettings>
    {
        private readonly IShopManager _shopManager;
        private readonly IUserInterface _userInterface;

        public AddShopCommand(IShopManager shopManager, IUserInterface userInterface)
        {
            _shopManager = shopManager;
            _userInterface = userInterface;
        }

        public override int Execute(CommandContext context, AddShopCommandSettings settings)
        {
            _shopManager.CreateShop(settings.ShopName, new Address(settings.ShopAddress));
            _userInterface.WriteLine($"Shop {settings.ShopName} successfully created.");
            return 0;
        }
    }
}