using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json;
using Banks.BusinessLogic.Tools;

namespace Banks
{
    [Serializable]
    public class IntervalSequence
    {
        private SortedList<decimal, PercentInterval> _intervals = new SortedList<decimal, PercentInterval>();

        private IntervalSequence()
        {
        }
        
        public IntervalSequence(Percent maxPercent)
        {
            _intervals = new SortedList<decimal, PercentInterval>();
            MaxPercent = maxPercent.Value;
        }
        
        public int Id { get; private init; }
        public decimal MaxPercent { get; private init; }
        public AccountOptions AccountOptions { get; private init; }

        public List<PercentInterval> PercentIntervals
        {
            get => _intervals.Values.ToList();
            init
            {
                _intervals = new SortedList<decimal, PercentInterval>();
                foreach (PercentInterval percentInterval in value)
                {
                    _intervals.Add(percentInterval.MaxSum, percentInterval);
                }
            }
        }
        
        public void AddInterval(PercentInterval percentInterval)
        {
            _intervals.Add(percentInterval.MaxSum, percentInterval);
        }

        public decimal GetPercent(decimal sum)
        {
            decimal? percent = null;
            foreach (PercentInterval percentInterval in _intervals.Values.Reverse())
            {
                if (percentInterval.Contains(sum))
                    percent = percentInterval.Percent;
            }

            return percent ?? MaxPercent;
        }
    }
}