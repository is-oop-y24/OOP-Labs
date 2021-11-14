using System.Collections.Generic;

namespace Banks
{
    public class CentralBank : ICentralBank
    {
        private List<Bank> _banks;

        public CentralBank()
        {
        }
        
        internal CentralBank(List<Bank> banks)
        {
            _banks = banks;
        }

        public void MakePayouts()
        {
            throw new System.NotImplementedException();
        }

        public Bank RegisterBank(string bankName)
        {
            throw new System.NotImplementedException();
        }

        public Bank FindBank(int bankId)
        {
            throw new System.NotImplementedException();
        }

        public Bank GetBank(int bankId)
        {
            throw new System.NotImplementedException();
        }
    }
}