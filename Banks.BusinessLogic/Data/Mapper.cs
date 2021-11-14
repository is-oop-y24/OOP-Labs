using System.Collections.Generic;
using Banks.DataAccessLayer.Models;

namespace Banks
{
    public class Mapper : IMapper
    {
        public AccountModel GetAccountModel(Account account, Bank bank)
        {
            return new AccountModel{
                Bank = GetBankModel(bank),
                Client = account.Client, }
        }

        public Account GetAccount(AccountModel accountModel, IAccountOptions accountOptions)
        {
            throw new System.NotImplementedException();
        }

        public BankModel GetBankModel(Bank bank)
        {
            throw new System.NotImplementedException();
        }

        public Bank GetBank(BankModel bankModel, List<Client> clients, List<Transaction> transactions)
        {
            throw new System.NotImplementedException();
        }

        public Bank GetBank(BankModel bankModel, List<Client> clients, List<Account> accounts, List<Transaction> transactions)
        {
            throw new System.NotImplementedException();
        }

        public ClientModel GetClientModel(Client client)
        {
            throw new System.NotImplementedException();
        }

        public Client GetClient(ClientModel clientModel, List<Account> accounts)
        {
            throw new System.NotImplementedException();
        }

        public TransactionModel GetTransactionModel(Transaction transaction)
        {
            throw new System.NotImplementedException();
        }

        public Transaction GetTransaction(TransactionModel transactionModel, Client source, Client destination)
        {
            throw new System.NotImplementedException();
        }

        public AccountOptionsModel GetAccountOptionsModel(IAccountOptions accountOptions)
        {
            throw new System.NotImplementedException();
        }

        public IAccountOptions GetAccountOptions(AccountOptionsModel accountOptionsModel)
        {
            throw new System.NotImplementedException();
        }

        public string GetIntervalsString(PercentIntervals intervals)
        {
            throw new System.NotImplementedException();
        }

        public PercentIntervals GetIntervals(string intervalsString)
        {
            throw new System.NotImplementedException();
        }
    }
}