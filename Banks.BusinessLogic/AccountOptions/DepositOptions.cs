using System;

namespace Banks
{
    public class DepositOptions : AccountOptions
    {
        public IntervalSequence Intervals { get; private init; }

        private DepositOptions()
        {
        }
        
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