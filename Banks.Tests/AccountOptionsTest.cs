using System;
using System.Text.Json;
using NUnit.Framework;

namespace Banks.Tests
{
    public class PercentIntervalsTest
    {
        private PercentIntervals _percentIntervals;
        private const decimal _maxPercent = 10;

        [SetUp]
        public void SetUp()
        {
            _percentIntervals = new PercentIntervals(_maxPercent);
        }

        [Test]
        public void AddIntervalsAndGetPercents_CorrectPercentsReturned()
        {
            const decimal sumUnit = 100;
            const decimal percentUnit = 1;
            _percentIntervals.AddInterval(new PercentInterval(6 * sumUnit, 3 * percentUnit));
            _percentIntervals.AddInterval(new PercentInterval(4 * sumUnit, 2 * percentUnit));
            _percentIntervals.AddInterval(new PercentInterval(2 * sumUnit, 1 * percentUnit));

            Assert.AreEqual(1 * percentUnit, _percentIntervals.GetPercent(1 * sumUnit));
            Assert.AreEqual(1 * percentUnit, _percentIntervals.GetPercent(2 * sumUnit));
            Assert.AreEqual(3 * percentUnit, _percentIntervals.GetPercent(5 * sumUnit));
        }

        [Test]
        public void GetSumGreaterThanMax_CorrectPercentReturned()
        {
            const decimal sumUnit = 100;
            const decimal percentUnit = 1;
            _percentIntervals.AddInterval(new PercentInterval(6 * sumUnit, 3 * percentUnit));
            _percentIntervals.AddInterval(new PercentInterval(4 * sumUnit, 2 * percentUnit));
            _percentIntervals.AddInterval(new PercentInterval(2 * sumUnit, 1 * percentUnit));
            
            Assert.AreEqual(_maxPercent, _percentIntervals.GetPercent(7 * sumUnit));
        }
    }
}