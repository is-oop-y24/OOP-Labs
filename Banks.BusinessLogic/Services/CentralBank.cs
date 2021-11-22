using System;
using System.Collections.Generic;
using System.Linq;
using Banks.BusinessLogic.Data;
using Banks.BusinessLogic.Tools;

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

        private List<Client> GetClients()
        {
            return _bankRepository
                .GetBanks()
                .SelectMany(bank => bank.Clients)
                .ToList();
        }

        private List<Account> GetAccounts()
        {
            return _bankRepository
                .GetBanks()
                .SelectMany(bank => bank.Accounts)
                .ToList();
        }

        private List<Transaction> GetTransactions()
        {
            return _bankRepository
                .GetBanks()
                .SelectMany(bank => bank.Transactions)
                .ToList();
        }

        public List<Bank> GetBanks()
        {
            return _bankRepository.GetBanks();
        }

        public Client GetClient(int clientId)
        {
            Client client = GetClients()
                .FirstOrDefault(client => client.Id == clientId);
            return client ?? throw new BankException("Client is not registered.");
        }

        public Account GetAccount(int accountId)
        {
            Account account = GetAccounts()
                .FirstOrDefault(acc => acc.Id == accountId);
            return account ?? throw new BankException("Account is not registered.");
        }

        public Transaction GetTransaction(int transactionId)
        {
            Transaction transaction = GetTransactions()
                .FirstOrDefault(tr => tr.Id == transactionId);
            return transaction ?? throw new BankException("Transaction does not exist.");
        }

        public void Refresh(DateTime finishDate)
        {
            _bankRepository.GetBanks().ForEach(bank => bank.Refresh(finishDate));
        }

        public void Save()
        {
            _bankRepository.Save();
        }

        public void Dispose()
        {
            Save();
        }
    }
}