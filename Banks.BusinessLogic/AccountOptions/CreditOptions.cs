using System;
using Banks.BusinessLogic.Tools;
using Kfc.Utility.Extensions;

namespace Banks
{
    public class CreditOptions : AccountOptions
    {
        public CreditOptions(decimal commission, decimal limit)
        {
            if (limit <= 0)
                throw new BankException("Credit limit must be a positive number.");
            Commission = commission.ThrowIfNull(nameof(commission));
            Limit = limit;
        }
        
        private CreditOptions()
        {
        }
        
        public decimal Commission { get; private init; }
        public decimal Limit { get; private init; }
        
        public override decimal CalculateAccumulated(DateTime startDate, DateTime finishDate, decimal sum)
        {
            decimal daysPassed = (decimal)(finishDate - startDate).TotalDays;
            if (daysPassed < 0)
                throw new BankException("Incorrect interval.");
            return (sum < -Limit) ? Commission * daysPassed : 0;
        }

        public override decimal MaxWithdrawSum(decimal currentSum)
        {
            return currentSum + Limit;
        }
    }
}