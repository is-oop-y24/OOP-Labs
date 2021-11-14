using System;

namespace Banks
{
    public interface IAccountOptions
    {
        int Id { get; }

        /// <summary>
        /// Calculate accumulated money from the last update date to calculateUntil param.
        /// </summary>
        /// <returns>Accumulated payout.</returns>
        decimal CalculateAccumulated(DateTime calculateUntil);
        decimal MaxWithdrawSum(decimal currentSum);
    }
}