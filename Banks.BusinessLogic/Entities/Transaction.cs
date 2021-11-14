using System;

namespace Banks
{
    public class Transaction
    {
        public int Id { get; init; }
        public DateTime Date { get; init; }
        public Account Source { get; init; }
        public Account Destination { get; init; }
        public bool IsAborted { get; init; }
    }
}