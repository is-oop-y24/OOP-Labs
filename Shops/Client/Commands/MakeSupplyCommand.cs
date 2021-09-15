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
            shop.MakeSupply(shop.CurrentSupply);
            _userInterface.WriteLine($"Supply in shop {shop.Name} is successfully made.");
            return 0;
        }
    }
}