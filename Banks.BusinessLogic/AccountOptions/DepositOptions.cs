using System;

namespace Banks
{
    public class DepositOptions : IAccountOptions
    {
        public int Id { get; init; }
        public PercentIntervals Intervals { get; init; }
        
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