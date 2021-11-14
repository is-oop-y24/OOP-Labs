using System;

namespace Banks
{
    public class DebitOptions : IAccountOptions
    {
        public DebitOptions(decimal percent)
        {
            Percent = percent;
        }
        
        public int Id { get; }
        public decimal Percent { get; }
        
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