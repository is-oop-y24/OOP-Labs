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
            throw new System.NotImplementedException();
        }
    }
}