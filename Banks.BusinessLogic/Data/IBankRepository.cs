using System.Collections.Generic;

namespace Banks.BusinessLogic.Data
{
    public interface IBankRepository
    {
        void AddBank(Bank bank);
        Bank GetBank(int id);
        List<Bank> GetBanks();
        void Save();
    }
}