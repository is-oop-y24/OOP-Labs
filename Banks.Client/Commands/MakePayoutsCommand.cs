using Banks.BusinessLogic.Tools;
using Spectre.Console.Cli;

namespace Banks.Commands
{
    public class MakePayoutsCommand : Command<MakePayoutsCommand.Settings>
    {
        private readonly ICentralBank _centralBank;
        private readonly IUserInterface _userInterface;

        public MakePayoutsCommand(IUserInterface userInterface, ICentralBank centralBank)
        {
            _centralBank = centralBank;
            _userInterface = userInterface;
        }

        public override int Execute(CommandContext context, Settings settings)
        {
            using (_centralBank)
                _centralBank.MakePayouts();

            _userInterface.WriteMessage("Payouts made successfully.");
            return 0;
        }

        public class Settings : CommandSettings
        {
        }
    }
}