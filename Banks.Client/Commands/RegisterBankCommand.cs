using Spectre.Console.Cli;

namespace Banks.Commands
{
    public class RegisterBankCommand : Command<RegisterBankCommand.Settings>
    {
        private readonly ICentralBank _centralBank;
        private readonly IUserInterface _userInterface;

        public RegisterBankCommand(IUserInterface userInterface, ICentralBank centralBank)
        {
            _centralBank = centralBank;
            _userInterface = userInterface;
        }

        public override int Execute(CommandContext context, Settings settings)
        {
            using (_centralBank)
                _centralBank.RegisterBank(settings.MaxWithdrawForDoubtful);

            _userInterface.WriteMessage("Bank successfully registered.");
            return 0;
        }

        public class Settings : CommandSettings
        {
            [CommandArgument(0, "<maxWithdrawForDoubtful>")]
            public decimal MaxWithdrawForDoubtful { get; init; }
        }
    }
}