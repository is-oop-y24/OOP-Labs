using Banks.BusinessLogic.Tools;
using Spectre.Console.Cli;

namespace Banks.Commands
{
    public class RefreshCommand : Command<RefreshCommand.Settings>
    {
        private readonly ICentralBank _centralBank;
        private readonly IUserInterface _userInterface;

        public RefreshCommand(IUserInterface userInterface, ICentralBank centralBank)
        {
            _centralBank = centralBank;
            _userInterface = userInterface;
        }

        public override int Execute(CommandContext context, Settings settings)
        {
            using (_centralBank)
                _centralBank.Refresh();

            _userInterface.WriteMessage("Successfully refreshed.");
            return 0;
        }

        public class Settings : CommandSettings
        {
        }
    }
}