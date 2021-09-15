using System;
using Spectre.Console.Cli;

namespace Shops.Commands
{
    public class RegisterProductCommand : Command<RegisterProductCommandSettings>
    {
        private readonly IShopManager _shopManager;
        private readonly IUserInterface _userInterface;

        public RegisterProductCommand(IShopManager shopManager, IUserInterface userInterface)
        {
            _shopManager = shopManager;
            _userInterface = userInterface;
        }


        public override int Execute(CommandContext context, RegisterProductCommandSettings settings)
        {
            throw new NotImplementedException();
        }
    }
}