using System;
using System.ComponentModel;
using Spectre.Console.Cli;

namespace Banks.Commands
{
    public class RefreshCommand : Command<RefreshCommand.Settings>
    {
        private readonly ICentralBank _centralBank;
        private readonly IUserInterface _userInterface;

        public RefreshCommand(IUserInterface userInterface, ICentralBank centralBank)
        {
            _centralBank = centralBank;
            _userInterface = userInterface;
        }

        public override int Execute(CommandContext context, Settings settings)
        {
            using (_centralBank)
                _centralBank.Refresh(DateTime.Parse(settings.FinishDateString));

            _userInterface.WriteMessage("Successfully refreshed.");
            return 0;
        }

        public class Settings : CommandSettings
        {
            [CommandArgument(0, "<dd.mm.yyyy>")]
            [Description("Finish date to refresh")]
            public string FinishDateString { get; init; }
        }
    }
}