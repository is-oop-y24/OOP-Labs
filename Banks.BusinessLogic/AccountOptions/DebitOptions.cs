using System;
using Banks.BusinessLogic.Tools;

namespace Banks
{
    public class DebitOptions : AccountOptions
    {
        private DebitOptions()
        {
        }
        public DebitOptions(Percent percent)
        {
            Percent = percent.Value;
        }
        
        public decimal Percent { get; private init; }
        
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