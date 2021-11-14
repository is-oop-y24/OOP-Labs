using System;

namespace Banks
{
    [Serializable]
    public class PercentInterval
    {
        public PercentInterval(decimal maxSum, decimal percent)
        {
            MaxSum = maxSum;
            Percent = percent;
        }
        
        public decimal MaxSum { get; }
        public decimal Percent { get; }

        public bool Contains(decimal number)
        {
            return number <= MaxSum;
        }
    }
}