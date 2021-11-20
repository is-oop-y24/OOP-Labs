using System.Collections.Generic;
using Spectre.Console;

namespace Banks
{
    public class TableMaker : ITableMaker
    {
        public Table MakeBankTable(List<Bank> banks)
        {
            var bankTable = new Table();
            bankTable.AddColumns("ID", "Max withdraw for doubtful");
            foreach (Bank bank in banks)
            {
                bankTable.AddRow(
                    bank.Id.ToString(),
                    $"{bank.MaxWithdrawForDoubtful:F2}");
            }

            return bankTable;
        }

        public Table MakeClientTable(List<Client> clients)
        {
            var clientTable = new Table();
            clientTable.AddColumns("ID", "Name", "Address", "Passport", "Verified");
            foreach (Client client in clients)
            {
                clientTable.AddRow(
                    client.Id.ToString(),
                    client.Name,
                    client.Identifier.Address ?? "-",
                    client.Identifier.Passport ?? "-",
                    !client.IsDoubtful ? "Yes" : "No");
            }

            return clientTable;
        }

        public Table MakeAccountTable(List<Account> accounts)
        {
            var accountTable = new Table();
            accountTable.AddColumns("ID", "Type", "Owner", "Sum", "Notifications");
            foreach (Account account in accounts)
            {
                accountTable.AddRow(
                    account.Id.ToString(),
                    account.Options.GetType().ToString(),
                    account.Client.Name,
                    $"{account.Sum:F2}",
                    account.ChangesNotify ? "Yes" : "No");
            }

            return accountTable;
        }

        public Table MakeTransactionTable(List<Transaction> transactions)
        {
            var transactionTable = new Table();
            transactionTable.AddColumns("ID", "Source account ID", "Recipient account ID", "Sum", "Date");
            foreach (Transaction transaction in transactions)
            {
                transactionTable.AddRow(
                    transaction.Id.ToString(),
                    $"{transaction.Source?.Id.ToString() ?? "-"}",
                    $"{transaction.Destination?.Id.ToString() ?? "-"}",
                    $"{transaction.Sum:F2}",
                    transaction.Date.ToShortDateString());
            }

            return transactionTable;
        }
    }
}