using System;
using Banks.BusinessLogic.Tools;
using NUnit.Framework;

namespace Banks.Tests
{
    public class AccountOptionsTest
    {
        private decimal _sum;
        private decimal _daysPassed; 
        DateTime startDate = DateTime.Now;
        DateTime finishDate = DateTime.Now.AddDays(10);

        [SetUp]
        public void SetUp()
        {
            _sum = 1000;
            _daysPassed = 10;
            
            startDate = DateTime.Now;
            finishDate = DateTime.Now.AddDays((int)_daysPassed);
        }
        
        [Test]
        public void DebitOptionsCalculateAccumulated_CorrectValueReturned()
        {
            const decimal percent = 365;
            const decimal result = 100;
            
            AccountOptions options = new DebitOptions(new Percent(percent));
            decimal delta = options.CalculateAccumulated(startDate, finishDate, _sum) - result;
            Assert.LessOrEqual(Math.Abs(delta), 0.001);
        }

        [Test]
        public void CreditOptionsCalculateAccumulated_PositiveSumAndNegativeSum_CorrectValuesReturned()
        {
            const decimal commission = 100;
            const decimal limit = 100;
            const decimal lowerThanLimit = -101;

            AccountOptions options = new CreditOptions(commission, limit);
            decimal delta = options.CalculateAccumulated(startDate, finishDate, _sum);
            Assert.LessOrEqual(Math.Abs(delta), 0.001);

            decimal result = commission * 10;
            delta = result - options.CalculateAccumulated(startDate, finishDate, lowerThanLimit);
            Assert.LessOrEqual(Math.Abs(delta), 0.001);
        }

        [Test]
        public void DepositOptionsTest()
        {
            const decimal maxPercent = 3650;

            decimal intervalMax1 = _sum / 10;
            decimal percent1 = maxPercent / 10;

            decimal intervalMax2 = _sum / 5;
            decimal percent2 = maxPercent / 5;
            
            decimal intervalMax3 = _sum * 8 / 10;
            decimal percent3 = maxPercent / 2;

            decimal sum1 = intervalMax1 / 2;
            decimal sum2 = (intervalMax1 + intervalMax2) / 2;
            decimal sum3 = (intervalMax2 + intervalMax3) / 2;

            decimal result1 = sum1 * percent1 / 100 / 365 * _daysPassed;
            decimal result2 = sum2 * percent2 / 100 / 365 * _daysPassed;
            decimal result3 = sum3 * percent3 / 100 / 365 * _daysPassed;
            
            DateTime expiringDay = DateTime.Now.AddDays(100);
            
            var sequence = new IntervalSequence(new Percent(maxPercent));
            sequence.AddInterval(new PercentInterval(intervalMax1, new Percent(percent1)));
            sequence.AddInterval(new PercentInterval(intervalMax2, new Percent(percent2)));
            sequence.AddInterval(new PercentInterval(intervalMax3, new Percent(percent3)));

            AccountOptions options = new DepositOptions(sequence, expiringDay);

            decimal delta;
            delta = result1 - options.CalculateAccumulated(startDate, finishDate, sum1);
            Assert.LessOrEqual(Math.Abs(delta), 0.001);
            delta = result2 - options.CalculateAccumulated(startDate, finishDate, sum2);
            Assert.LessOrEqual(Math.Abs(delta), 0.001);
            delta = result3 - options.CalculateAccumulated(startDate, finishDate, sum3);
            Assert.LessOrEqual(Math.Abs(delta), 0.001);
        }
    }
}