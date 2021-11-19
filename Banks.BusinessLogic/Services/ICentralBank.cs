using System;

namespace Banks
{
    public interface ICentralBank : IDisposable
    {
        void MakePayouts();
        Bank RegisterBank(decimal maxWithdrawForDoubtful);
        Bank GetBank(int bankId);
        void Refresh();
    }
}