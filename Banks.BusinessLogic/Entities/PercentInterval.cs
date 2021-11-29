using Banks.BusinessLogic.Tools;

namespace Banks
{
    public class PercentInterval
    {
        private PercentInterval()
        {
        }
        
        public PercentInterval(decimal maxSum, Percent percent)
        {
            if (maxSum <= 0)
                throw new BankException("Max sum must be a positive number.");
            MaxSum = maxSum;
            Percent = percent.Value;
        }
        
        public int Id { get; private init; }
        public decimal MaxSum { get; private init; }
        public decimal Percent { get; private init; }
        public IntervalSequence IntervalSequence { get; private init; }

        public bool Contains(decimal number)
        {
            return number <= MaxSum;
        }
    }
}