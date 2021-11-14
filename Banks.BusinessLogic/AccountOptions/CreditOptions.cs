using System;

namespace Banks
{
    public class CreditOptions : IAccountOptions
    {
        public CreditOptions(decimal commission, decimal limit)
        {
            Commission = commission;
            Limit = limit;
        }
        
        public int Id { get; }
        public decimal Commission { get; }
        public decimal Limit { get; }
        
        public decimal CalculateAccumulated(DateTime calculateUntil)
        {
            throw new NotImplementedException();
        }

        public decimal MaxWithdrawSum(decimal currentSum)
        {
            throw new NotImplementedException();
        }
    }
}