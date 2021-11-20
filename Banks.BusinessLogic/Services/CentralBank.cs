using System.Collections.Generic;
using Banks.BusinessLogic.Data;

namespace Banks
{
    public class CentralBank : ICentralBank
    {
        private IBankRepository _bankRepository;

        public CentralBank(IBankRepository bankRepository)
        {
            _bankRepository = bankRepository;
        }
        public void MakePayouts()
        {
            _bankRepository.GetBanks().ForEach(bank => bank.MakePayouts());
        }

        public Bank RegisterBank(decimal maxWithdrawForDoubtful)
        {
            var bank = new Bank(maxWithdrawForDoubtful);
            _bankRepository.AddBank(new Bank(maxWithdrawForDoubtful));
            return bank;
        }

        public Bank GetBank(int bankId)
        {
            return _bankRepository.GetBank(bankId);
        }

        public List<Bank> GetBanks()
        {
            return _bankRepository.GetBanks();
        }

        public void Refresh()
        {
            _bankRepository.GetBanks().ForEach(bank => bank.Refresh());
        }

        public void Save()
        {
            _bankRepository.Save();
        }
    }
}