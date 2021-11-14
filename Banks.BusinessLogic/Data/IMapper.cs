using Banks.DataAccessLayer.Models;

namespace Banks
{
    public interface IMapper
    {
        AccountModel GetAccountModel(Account account);
        Account GetAccount(AccountModel accountModel);
        BankModel GetBankModel(Bank bank);
        Bank GetBank(BankModel bankModel);
        ClientModel GetClientModel(Client client);
        Client GetClient(ClientModel clientModel);
        TransactionModel GetTransactionModel(Transaction transaction);
        Transaction GetTransaction(TransactionModel transactionModel);
    }
}