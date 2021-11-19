using System;
using Banks.BusinessLogic.Tools;

namespace Banks
{
    public class Transaction
    {
        private Transaction()
        {
        }

        public Transaction(DateTime date, Account source, Account destination, decimal sum)
        {
            Date = date;
            Source = source;
            Source = source;
            Destination = destination;
            Sum = sum;
            IsAborted = false;
        }
        
        public int Id { get; private init; }
        public DateTime Date { get; private init; }
        public Account Source { get; private init; }
        public Account Destination { get; private init; }
        public bool IsAborted { get; private set; }
        public decimal Sum { get; private init; }

        internal void Abort()
        {
            if (IsAborted)
                throw new BankException("Transaction is already aborted.");

            Destination?.Withdraw(Sum);
            Source?.TopUp(Sum);
            IsAborted = true;
        }
    }
}