using System;
using Banks.BusinessLogic.Tools;
using Kfc.Utility.Extensions;

namespace Banks
{
    public class DepositOptions : AccountOptions
    {
        public IntervalSequence Intervals { get; private init; }
        public DateTime ExpiringDate { get; private init; }
        
        public DepositOptions(IntervalSequence sequence, DateTime expiringDate)
        {
            if (expiringDate < DateTime.Now)
                throw new BankException("Incorrect expiring date.");
            
            ExpiringDate = expiringDate;
            Intervals = sequence.ThrowIfNull(nameof(sequence));
        }
        
        private DepositOptions()
        {
        }

        private decimal DailyPercentMultiplier(decimal sum)
        {
            return Intervals.GetPercent(sum) / 365 / 100;
        }

        public override decimal CalculateAccumulated(DateTime startDate, DateTime finishDate, decimal sum)
        {
            decimal daysPassed = (decimal)(finishDate - startDate).TotalDays;
            if (daysPassed < 0)
                throw new BankException("Incorrect interval.");
            return sum * DailyPercentMultiplier(sum) * daysPassed;
        }

        public override decimal MaxWithdrawSum(decimal currentSum)
        {
            return ExpiringDate < DateTime.Now ? currentSum : 0;
        }
    }
}