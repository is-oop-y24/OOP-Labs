using System;
using System.Text.Json;
using Banks.BusinessLogic.Tools;
using NUnit.Framework;

namespace Banks.Tests
{
    public class IntervalSequenceTest
    {
        private IntervalSequence _intervalSequence;
        private const decimal _maxPercent = 10;

        [SetUp]
        public void SetUp()
        {
            _intervalSequence = new IntervalSequence(new Percent(_maxPercent));
        }

        [Test]
        public void AddIntervalsAndGetPercents_CorrectPercentsReturned()
        {
            const decimal sumUnit = 100;
            const decimal percentUnit = 1;
            _intervalSequence.AddInterval(new PercentInterval(6 * sumUnit, new Percent(3 * percentUnit)));
            _intervalSequence.AddInterval(new PercentInterval(4 * sumUnit, new Percent(2 * percentUnit)));
            _intervalSequence.AddInterval(new PercentInterval(2 * sumUnit, new Percent(1 * percentUnit)));

            Assert.AreEqual(1 * percentUnit, _intervalSequence.GetPercent(1 * sumUnit));
            Assert.AreEqual(1 * percentUnit, _intervalSequence.GetPercent(2 * sumUnit));
            Assert.AreEqual(3 * percentUnit, _intervalSequence.GetPercent(5 * sumUnit));
        }

        [Test]
        public void GetSumGreaterThanMax_CorrectPercentReturned()
        {
            const decimal sumUnit = 100;
            const decimal percentUnit = 1;
            _intervalSequence.AddInterval(new PercentInterval(6 * sumUnit, new Percent(3 * percentUnit)));
            _intervalSequence.AddInterval(new PercentInterval(4 * sumUnit, new Percent(2 * percentUnit)));
            _intervalSequence.AddInterval(new PercentInterval(2 * sumUnit, new Percent(1 * percentUnit)));
            
            Assert.AreEqual(_maxPercent, _intervalSequence.GetPercent(7 * sumUnit));
        }
    }
}