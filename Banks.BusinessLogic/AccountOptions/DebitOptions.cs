using System;
using Banks.BusinessLogic.Tools;
using Kfc.Utility.Extensions;

namespace Banks
{
    public class DebitOptions : AccountOptions
    {
        public DebitOptions(Percent percent)
        {
            percent.ThrowIfNull(nameof(percent));
            Percent = percent.Value;
        }
        
        private DebitOptions()
        {
        }

        public decimal Percent { get; private init; }
        private decimal DailyPercentMultiplier()
        {
            return Percent / 365 / 100;
        }
        
        public override decimal CalculateAccumulated(DateTime startDate, DateTime finishDate, decimal sum)
        {
            decimal daysPassed = (decimal)(finishDate - startDate).TotalDays;
            if (daysPassed < 0)
                throw new BankException("Incorrect interval.");
            return sum * DailyPercentMultiplier() * daysPassed;
        }

        public override decimal MaxWithdrawSum(decimal currentSum)
        {
            if (currentSum < 0)
                throw new BankException("Deposit account sum cannot be negative.");
            return currentSum;
        }
    }
}