using System.Collections.Generic;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Banks.Commands
{
    public class ShowTransactionsCommand : Command<ShowTransactionsCommand.Settings>
    {
        private readonly ICentralBank _centralBank;
        private readonly IUserInterface _userInterface;
        private readonly ITableMaker _tableMaker;

        public ShowTransactionsCommand(IUserInterface userInterface, ICentralBank centralBank, ITableMaker tableMaker)
        {
            _centralBank = centralBank;
            _userInterface = userInterface;
            _tableMaker = tableMaker;
        }

        public override int Execute(CommandContext context, Settings settings)
        {
            List<Transaction> transactions = _centralBank.GetBank(settings.BankId).Transactions;
            _userInterface.ShowTable(_tableMaker.MakeTransactionTable(transactions));
            return 0;
        }

        public class Settings : CommandSettings
        {
            [CommandArgument(0, "<bankId>")]
            public int BankId { get; init; }
        }
    }
}