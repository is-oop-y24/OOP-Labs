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

        public Bank RegisterBank()
        {
            var bank = new Bank();
            _bankRepository.AddBank(new Bank());
            return bank;
        }

        public Bank GetBank(int bankId)
        {
            return _bankRepository.GetBank(bankId);
        }

        public void Refresh()
        {
            _bankRepository.GetBanks().ForEach(bank => bank.Refresh());
        }

        public void Dispose()
        {
            _bankRepository.Dispose();
        }
    }
}