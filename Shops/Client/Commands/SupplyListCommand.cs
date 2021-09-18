using Spectre.Console;
using Spectre.Console.Cli;

namespace Shops.Commands
{
    public class SupplyListCommand : Command<SupplyListCommandSettings>
    {
        private readonly IShopManager _shopManager;
        private readonly IUserInterface _userInterface;

        public SupplyListCommand(IShopManager shopManager, IUserInterface userInterface)
        {
            _shopManager = shopManager;
            _userInterface = userInterface;
        }

        public override int Execute(CommandContext context, SupplyListCommandSettings settings)
        {
            var table = new Table();
            table.AddColumn("Product ID");
            table.AddColumn("Quantity");
            table.AddColumn("Worth");

            Shop shop = _shopManager.GetShop(new ShopId(settings.ShopId));

            foreach (ProductSupply productSupply in shop.CurrentSupply.ProductSupplies)
            {
                table.AddRow(
                    productSupply.ProductId.GetId().ToString(),
                    productSupply.Quantity.ToString(),
                    productSupply.Worth.ToString());
            }

            _userInterface.ShowTable(table);

            return 0;
        }
    }
}