using System;
using System.Collections.Generic;

namespace Banks
{
    public interface ICentralBank
    {
        void MakePayouts();
        Bank RegisterBank(decimal maxWithdrawForDoubtful);
        Bank GetBank(int bankId);
        List<Bank> GetBanks();
        void Refresh();
        void Save();
    }
}