using System.Collections.Generic;
using Banks.DataAccessLayer.Models;

namespace Banks
{
    public interface IMapper
    {
        AccountModel GetAccountModel(Account account);
        Account GetAccount(AccountModel accountModel, IAccountOptions accountOptions);
        BankModel GetBankModel(Bank bank);
        Bank GetBank(BankModel bankModel, List<Client> clients, List<Transaction> transactions);
        ClientModel GetClientModel(Client client);
        Client GetClient(ClientModel clientModel, List<Account> accounts);
        TransactionModel GetTransactionModel(Transaction transaction);
        Transaction GetTransaction(TransactionModel transactionModel, Client source, Client destination);
        AccountOptionsModel GetAccountOptionsModel(IAccountOptions accountOptions);
        IAccountOptions GetAccountOptions(AccountOptionsModel accountOptionsModel);
        string GetIntervalsString(PercentIntervals intervals);
        PercentIntervals GetIntervals(string intervalsString);
    }
}