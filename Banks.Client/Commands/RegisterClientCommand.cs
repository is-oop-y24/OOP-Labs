
using Banks.BusinessLogic.Tools;
using Spectre.Console.Cli;

namespace Banks.Commands
{
    public class RegisterClientCommand : Command<RegisterClientCommand.Settings>
    {
        private readonly ICentralBank _centralBank;
        private readonly IUserInterface _userInterface;

        public RegisterClientCommand(IUserInterface userInterface, ICentralBank centralBank)
        {
            _centralBank = centralBank;
            _userInterface = userInterface;
        }

        public override int Execute(CommandContext context, Settings settings)
        {
            using (_centralBank)
            {
                Bank bank = _centralBank.GetBank(settings.BankId);
                IClientBuilder clientBuilder = new ClientBuilder();
                clientBuilder.SetName(settings.Name);
                clientBuilder.SetAddress(settings.Address);
                clientBuilder.SetPassport(settings.Passport);
                bank.RegisterClient(clientBuilder);
            }

            _userInterface.WriteMessage("Client successfully registered.");
            return 0;
        }

        public class Settings : CommandSettings
        {
            [CommandArgument(0, "<bankId>")]
            public int BankId { get; init; }

            [CommandArgument(1, "<clientName>")]
            public string Name { get; init; }

            [CommandOption("-a|--address")]
            public string Address { get; init; }

            [CommandOption("-p|--passport")]
            public string Passport { get; init; }
        }
    }
}