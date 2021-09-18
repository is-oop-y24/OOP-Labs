using Spectre.Console;
using Spectre.Console.Cli;

namespace Shops.Commands
{
    public class ShopListCommand : Command<ShopListCommandSettings>
    {
        private readonly IShopManager _shopManager;
        private readonly IUserInterface _userInterface;

        public ShopListCommand(IShopManager shopManager, IUserInterface userInterface)
        {
            _shopManager = shopManager;
            _userInterface = userInterface;
        }

        public override int Execute(CommandContext context, ShopListCommandSettings settings)
        {
            var table = new Table();
            table.AddColumn("Shop ID");
            table.AddColumn("Name");
            table.AddColumn("Address");

            foreach (Shop shop in _shopManager.Shops)
            {
                table.AddRow(shop.Id.GetIntId().ToString(), shop.Name, shop.Address.GetAddressString());
            }
            _userInterface.ShowTable(table);
            return 0;
        }
    }
}