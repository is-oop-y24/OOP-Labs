using Spectre.Console.Cli;

namespace Banks.Commands
{
    public class SubscribeToChangesCommand : Command<SubscribeToChangesCommand.Settings>
    {
        private readonly ICentralBank _centralBank;
        private readonly IUserInterface _userInterface;

        public SubscribeToChangesCommand(IUserInterface userInterface, ICentralBank centralBank)
        {
            _centralBank = centralBank;
            _userInterface = userInterface;
        }

        public override int Execute(CommandContext context, Settings settings)
        {
            using (_centralBank)
                _centralBank.GetAccount(settings.AccountId).SubscribeClientToChanges();

            _userInterface.WriteMessage("Successful subscription.");
            return 0;
        }

        public class Settings : CommandSettings
        {
            [CommandArgument(0, "<accountId>")]
            public int AccountId { get; init; }
        }
    }
}