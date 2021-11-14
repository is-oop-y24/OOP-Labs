using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace Banks
{
    [Serializable]
    public class PercentIntervals
    {
        public SortedList<decimal, PercentInterval> Intervals { get; init; }

        public PercentIntervals()
        {
        }
        
        public PercentIntervals(decimal maxPercent)
        {
            Intervals = new SortedList<decimal, PercentInterval>();
            MaxPercent = maxPercent;
        }
        
        public decimal MaxPercent { get; init; }
        
        public void AddInterval(PercentInterval percentInterval)
        {
            Intervals.Add(percentInterval.MaxSum, percentInterval);
        }

        public decimal GetPercent(decimal sum)
        {
            decimal? percent = null;
            foreach (PercentInterval percentInterval in Intervals.Values.Reverse())
            {
                if (percentInterval.Contains(sum))
                    percent = percentInterval.Percent;
            }

            return percent ?? MaxPercent;
        }
    }
}