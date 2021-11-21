using Banks.BusinessLogic.Tools;
using Spectre.Console.Cli;

namespace Banks.Commands
{
    public class RegisterDebitAccountCommand : Command<RegisterDebitAccountCommand.Settings>
    {
        private ICentralBank _centralBank;
        private IUserInterface _userInterface;

        public RegisterDebitAccountCommand(ICentralBank centralBank, IUserInterface userInterface)
        {
            _centralBank = centralBank;
            _userInterface = userInterface;
        }

        public override int Execute(CommandContext context, Settings settings)
        {
            using (_centralBank)
            {
                Client client = _centralBank.GetClient(settings.ClientId);
                client.Bank.RegisterAccount(client, new DebitOptions(new Percent(settings.Percent)));
            }

            _userInterface.WriteMessage("Account successfully registered.");
            return 0;
        }

        public class Settings : CommandSettings
        {
            [CommandArgument(0, "<clientId>")]
            public int ClientId { get; init; }

            [CommandArgument(1, "<yearPercent>")]
            public decimal Percent { get; init; }
        }
    }
}