using Spectre.Console.Cli;

namespace Shops.Commands
{
    public class AddToSupplyCommand : Command<AddToSupplyCommandSettings>
    {
        private IShopManager _shopManager;
        private IUserInterface _userInterface;

        public AddToSupplyCommand(IShopManager shopManager, IUserInterface userInterface)
        {
            _shopManager = shopManager;
            _userInterface = userInterface;
        }

        public override int Execute(CommandContext context, AddToSupplyCommandSettings settings)
        {
            Shop shop = _shopManager.GetShop(new ShopId(settings.ShopId));
            shop.CurrentSupply.AddProduct(new ProductSupply(
                new ProductId(settings.ProductId), settings.Quantity, settings.ProductWorth));
            _userInterface.WriteLine($"Product {settings.ProductId} is successfully added to {shop.Name} supply");
            return 0;
        }
    }
}