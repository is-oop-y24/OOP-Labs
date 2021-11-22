using Spectre.Console.Cli;

namespace Banks.Commands
{
    public class IdentifyClientCommand : Command<IdentifyClientCommand.Settings>
    {
        private readonly ICentralBank _centralBank;
        private readonly IUserInterface _userInterface;

        public IdentifyClientCommand(IUserInterface userInterface, ICentralBank centralBank)
        {
            _centralBank = centralBank;
            _userInterface = userInterface;
        }

        public override int Execute(CommandContext context, Settings settings)
        {
            using (_centralBank)
            {
                Client client = _centralBank.GetClient(settings.ClientId);
                client.ChangeIdentifier(new ClientIdentifier
                {
                    Address = settings.Address,
                    Passport = settings.Passport,
                });
            }

            _userInterface.WriteMessage("Client identifier successfully changed.");
            return 0;
        }

        public class Settings : CommandSettings
        {
            [CommandArgument(0, "<clientId>")]
            public int ClientId { get; init; }

            [CommandOption("-a|--address")]
            public string Address { get; init; }

            [CommandOption("-p|--passport")]
            public string Passport { get; init; }
        }
    }
}