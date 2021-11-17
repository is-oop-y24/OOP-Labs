using System;
using Banks.BusinessLogic.Tools;
using Kfc.Utility.Extensions;

namespace Banks
{
    public class CreditOptions : AccountOptions
    {
        private CreditOptions()
        {
        }

        public CreditOptions(decimal commission, decimal limit)
        {
            if (limit <= 0)
                throw new BankException("Credit limit must be a positive number.");
            Commission = commission.ThrowIfNull(nameof(commission));
            Limit = limit;
        }
        
        public decimal Commission { get; private init; }
        public decimal Limit { get; private init; }
        
        public override decimal CalculateAccumulated(DateTime calculateUntil)
        {
            throw new NotImplementedException();
        }

        public override decimal MaxWithdrawSum(decimal currentSum)
        {
            throw new NotImplementedException();
        }
    }
}