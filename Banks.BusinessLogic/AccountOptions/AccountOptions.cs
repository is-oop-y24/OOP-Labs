using System;
using Kfc.Utility.Extensions;

namespace Banks
{
    public abstract class AccountOptions
    {
        public int Id { get; private init; }
        
        protected AccountOptions()
        {
        }

        /// <summary>
        /// Calculate a sum accumulated on the account in days between startDate and finishDate.
        /// </summary>
        /// <param name="sum">Sum that is constant in given period.</param>
        /// <returns></returns>
        public abstract decimal CalculateAccumulated(DateTime startDate, DateTime finishDate, decimal sum);
        public abstract decimal MaxWithdrawSum(decimal currentSum);
    }
}