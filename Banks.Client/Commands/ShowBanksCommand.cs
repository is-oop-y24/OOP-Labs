using Spectre.Console;
using Spectre.Console.Cli;

namespace Banks.Commands
{
    public class ShowBanksCommand : Command<ShowBanksCommand.Settings>
    {
        private readonly ICentralBank _centralBank;
        private readonly IUserInterface _userInterface;
        private readonly ITableMaker _tableMaker;

        public ShowBanksCommand(IUserInterface userInterface, ICentralBank centralBank, ITableMaker tableMaker)
        {
            _centralBank = centralBank;
            _userInterface = userInterface;
            _tableMaker = tableMaker;
        }

        public override int Execute(CommandContext context, Settings settings)
        {
            Table bankTable = _tableMaker.MakeBankTable(_centralBank.GetBanks());
            _userInterface.ShowTable(bankTable);
            return 0;
        }

        public class Settings : CommandSettings
        {
        }
    }
}
