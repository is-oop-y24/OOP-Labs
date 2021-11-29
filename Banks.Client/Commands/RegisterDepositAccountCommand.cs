using System;
using System.Collections.Generic;
using System.ComponentModel;
using Banks.BusinessLogic.Tools;
using Spectre.Console.Cli;

namespace Banks.Commands
{
    public class RegisterDepositAccountCommand : Command<RegisterDepositAccountCommand.Settings>
    {
        private ICentralBank _centralBank;
        private IUserInterface _userInterface;

        public RegisterDepositAccountCommand(ICentralBank centralBank, IUserInterface userInterface)
        {
            _centralBank = centralBank;
            _userInterface = userInterface;
        }

        public override int Execute(CommandContext context, Settings settings)
        {
            using (_centralBank)
            {
                Client client = _centralBank.GetClient(settings.ClientId);
                var sequence = new IntervalSequence(new Percent(settings.MaxPercent));
                foreach (string limitPercentPair in settings.LimitPercentPairs)
                {
                    decimal limit = decimal.Parse(limitPercentPair.Split(":")[0]);
                    decimal percent = decimal.Parse(limitPercentPair.Split(":")[1]);
                    var interval = new PercentInterval(limit, new Percent(percent));
                    sequence.AddInterval(interval);
                }

                client.Bank.RegisterAccount(client, new DepositOptions(sequence, DateTime.Parse(settings.ExpiringDateString)));
            }

            _userInterface.WriteMessage("Account successfully registered.");
            return 0;
        }

        public class Settings : CommandSettings
        {
            [CommandArgument(0, "<clientId>")]
            public int ClientId { get; init; }

            [CommandArgument(1, "<dd.mm.yyyy>")]
            [Description("Expiring date.")]
            public string ExpiringDateString { get; init; }

            [CommandArgument(2, "<maxPercent>")]
            public decimal MaxPercent { get; init; }

            [CommandArgument(3, "[limit:percent]")]
            public string[] LimitPercentPairs { get; init; }
        }
    }
}