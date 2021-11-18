using System;

namespace Banks
{
    public interface ICentralBank : IDisposable
    {
        void MakePayouts();
        Bank RegisterBank();
        Bank GetBank(int bankId);
        void Refresh();
    }
}