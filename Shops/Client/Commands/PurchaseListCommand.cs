using Spectre.Console;
using Spectre.Console.Cli;

namespace Shops.Commands
{
    public class PurchaseListCommand : Command<PurchaseListCommandSettings>
    {
        private readonly IShopManager _shopManager;
        private readonly IUserInterface _userInterface;
        private readonly Customer _customer;

        public PurchaseListCommand(IShopManager shopManager, IUserInterface userInterface, Customer customer)
        {
            _shopManager = shopManager;
            _userInterface = userInterface;
            _customer = customer;
        }

        public override int Execute(CommandContext context, PurchaseListCommandSettings settings)
        {
            var table = new Table();
            table.AddColumn("Product ID");
            table.AddColumn("Supply quantity");
            foreach (ProductPurchase productPurchase in _customer.CurrentPurchase.ProductPurchases)
            {
                table.AddRow(productPurchase.ProductId.GetId().ToString(),
                    productPurchase.Quantity.ToString());
            }
            return 0;
        }
    }
}