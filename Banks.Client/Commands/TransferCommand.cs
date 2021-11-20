using Banks.BusinessLogic.Tools;
using Spectre.Console.Cli;

namespace Banks.Commands
{
    public class TransferCommand : Command<TransferCommand.Settings>
    {
        private readonly ICentralBank _centralBank;
        private readonly IUserInterface _userInterface;

        public TransferCommand(IUserInterface userInterface, ICentralBank centralBank)
        {
            _centralBank = centralBank;
            _userInterface = userInterface;
        }

        public override int Execute(CommandContext context, Settings settings)
        {
            using (_centralBank)
            {
                Account source = _centralBank.GetAccount(settings.SourceAccountId);
                Account destination = _centralBank.GetAccount(settings.SourceAccountId);
                source.Client.Bank.Transfer(source, destination, settings.Sum);
            }

            _userInterface.WriteMessage("Successful transfer.");
            return 0;
        }

        public class Settings : CommandSettings
        {
            [CommandArgument(0, "<sourceAccountId>")]
            public int SourceAccountId { get; init; }

            [CommandArgument(2, "<destinationAccountI>")]
            public int DestinationAccountId { get; init; }

            [CommandArgument(3, "<sum>")]
            public decimal Sum { get; init; }
        }
    }
}