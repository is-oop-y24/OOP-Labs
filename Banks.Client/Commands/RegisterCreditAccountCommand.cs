using Spectre.Console.Cli;

namespace Banks.Commands
{
    public class RegisterCreditAccountCommand : Command<RegisterCreditAccountCommand.Settings>
    {
        private ICentralBank _centralBank;
        private IUserInterface _userInterface;

        public RegisterCreditAccountCommand(ICentralBank centralBank, IUserInterface userInterface)
        {
            _centralBank = centralBank;
            _userInterface = userInterface;
        }

        public override int Execute(CommandContext context, Settings settings)
        {
            using (_centralBank)
            {
                Client client = _centralBank.GetClient(settings.ClientId);
                client.Bank.RegisterAccount(client, new CreditOptions(settings.Commission, settings.Limit));
            }

            _userInterface.WriteMessage("Account successfully registered.");
            return 0;
        }

        public class Settings : CommandSettings
        {
            [CommandArgument(0, "<clientId>")]
            public int ClientId { get; init; }

            [CommandArgument(1, "<limit>")]
            public decimal Limit { get; init; }

            [CommandArgument(2, "<commission>")]
            public decimal Commission { get; init; }
        }
    }
}