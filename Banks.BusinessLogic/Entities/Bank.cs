using System.Collections.Generic;
using Banks.BusinessLogic.Tools;
using Kfc.Utility.Extensions;

namespace Banks
{
    public class Bank
    {
        private readonly List<Client> _clients;
        private readonly List<Account> _accounts;
        private readonly List<Transaction> _transactions;
        
        public Bank()
        {
        }

        internal Bank(List<Client> clients, List<Account> accounts, List<Transaction> transactions)
        {
            _clients = clients; 
            _accounts = accounts;
            _transactions = transactions;
        }

        public int Id { get; private init; }

        public List<Client> Clients
        {
            get => new (_clients);
            private init => _clients = new List<Client>(value);
        }
        
        public List<Account> Accounts
        {
            get => new (_accounts);
            private init => _accounts = new List<Account>(value);
        }
        
        public List<Transaction> Transactions
        {
            get => new (_transactions);
            private init => _transactions = new List<Transaction>(value);
        }

        private void TransactionMade(Transaction transaction)
        {
            _transactions.Add(transaction);
        }

        public void Refresh()
        {
            _accounts.ForEach(acc => acc.Refresh());
        }
        
        public void MakePayouts()
        {
            _accounts.ForEach(acc => acc.MakePayout());
        }

        public Client RegisterClient(IClientBuilder clientBuilder)
        {
            clientBuilder.ThrowIfNull(nameof(clientBuilder));
            clientBuilder.SetBank(this);
            Client client = clientBuilder.GetClient();
            _clients.Add(client);
            return client;
        }

        public void RegisterAccount(Client client, AccountOptions options)
        {
            var account = new Account(client, options);
            account.TransactionMade += TransactionMade;
            _accounts.Add(account);
        }
    }
}