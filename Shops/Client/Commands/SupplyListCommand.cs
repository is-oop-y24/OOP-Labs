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
            throw new System.NotImplementedException();
        }
    }
}