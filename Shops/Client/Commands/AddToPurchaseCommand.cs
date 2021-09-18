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
            if (_customer.CurrentPurchase == null)
                _customer.NewPurchase();

            _customer.CurrentPurchase.AddProductPurchase(
                new ProductPurchase(new ProductId(settings.ProductId), settings.Quantity));
            return 0;
        }
    }
}