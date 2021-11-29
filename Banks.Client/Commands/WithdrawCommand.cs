using Spectre.Console.Cli;

namespace Banks.Commands
{
    public class WithdrawCommand : Command<WithdrawCommand.Settings>
    {
        private readonly ICentralBank _centralBank;
        private readonly IUserInterface _userInterface;

        public WithdrawCommand(IUserInterface userInterface, ICentralBank centralBank)
        {
            _centralBank = centralBank;
            _userInterface = userInterface;
        }

        public override int Execute(CommandContext context, Settings settings)
        {
            using (_centralBank)
            {
                Account account = _centralBank.GetAccount(settings.AccountId);
                account.Client.Bank.Withdraw(account, settings.Sum);
            }

            _userInterface.WriteMessage("Successful withdrawal.");
            return 0;
        }

        public class Settings : CommandSettings
        {
            [CommandArgument(0, "<accountId>")]
            public int AccountId { get; init; }

            [CommandArgument(1, "<sum>")]
            public decimal Sum { get; init; }
        }
    }
}