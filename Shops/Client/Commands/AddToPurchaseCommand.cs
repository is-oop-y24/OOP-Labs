using Spectre.Console.Cli;

namespace Shops.Commands
{
    public class AddToPurchaseCommand : Command<AddToPurchaseCommandSettings>
    {
        private readonly IShopManager _shopManager;
        private readonly IUserInterface _userInterface;
        private readonly Customer _customer;

        public AddToPurchaseCommand(IShopManager shopManager, IUserInterface userInterface, Customer customer)
        {
            _shopManager = shopManager;
            _userInterface = userInterface;
            _customer = customer;
        }

        public override int Execute(CommandContext context, AddToPurchaseCommandSettings settings)
        {
            _customer.CurrentPurchase.AddProductPurchase(
                new ProductPurchase(new ProductId(settings.ProductId), settings.Quantity));
            _userInterface.WriteLine($"Product {settings.ProductId} successfully added to purchase.");
            return 0;
        }
    }
}