using System.Collections.Generic;
using Spectre.Console;

namespace Banks
{
    public interface ITableMaker
    {
        Table MakeBankTable(List<Bank> banks);
        Table MakeClientTable(List<Client> clients);
        Table MakeAccountTable(List<Account> accounts);
        Table MakeTransactionTable(List<Transaction> transactions);
    }
}