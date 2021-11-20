using System;
using System.Collections.Generic;

namespace Banks
{
    public interface ICentralBank : IDisposable
    {
        void MakePayouts();
        Bank RegisterBank(decimal maxWithdrawForDoubtful);
        Bank GetBank(int bankId);
        List<Bank> GetBanks();
        Client GetClient(int clientId);
        Account GetAccount(int accountId);
        Transaction GetTransaction(int transactionId);
        void Refresh();
        void Save();
    }
}