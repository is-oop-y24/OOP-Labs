using System;

namespace Banks
{
    public abstract class AccountOptions
    {
        public int Id { get; private init; }

        /// <summary>
        /// Calculate accumulated money from the last update date to calculateUntil param.
        /// </summary>
        /// <returns>Accumulated payout.</returns>
        public abstract decimal CalculateAccumulated(DateTime calculateUntil);
        public abstract decimal MaxWithdrawSum(decimal currentSum);
    }
}