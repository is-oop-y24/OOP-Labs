using Banks.BusinessLogic.Tools;
using Spectre.Console.Cli;

namespace Banks.Commands
{
    public class AbortTransactionCommand : Command<AbortTransactionCommand.Settings>
    {
        private readonly ICentralBank _centralBank;
        private readonly IUserInterface _userInterface;

        public AbortTransactionCommand(IUserInterface userInterface, ICentralBank centralBank)
        {
            _centralBank = centralBank;
            _userInterface = userInterface;
        }

        public override int Execute(CommandContext context, Settings settings)
        {
            using (_centralBank)
            {
                Transaction transaction = _centralBank.GetTransaction(settings.TransactionId);
                Bank bank = transaction?.Source?.Client?.Bank ??
                            transaction?.Destination?.Client?.Bank ??
                            throw new BankException("Cannot resolve bank of transaction.");
                bank.Abort(transaction);
            }

            _userInterface.WriteMessage("Transaction successfully aborted.");
            return 0;
        }

        public class Settings : CommandSettings
        {
            [CommandArgument(0, "<transactionId>")]
            public int TransactionId { get; init; }
        }
    }
}