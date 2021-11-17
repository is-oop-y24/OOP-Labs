using System.Collections.Generic;

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

        public int Id { get; init; }

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

        public void MakePayouts()
        {
            throw new System.NotImplementedException();
        }

        public Client RegisterClient(Client client)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateClient(Client client)
        {
            throw new System.NotImplementedException();
        }

        public void RegisterAccount(Client client, AccountOptions options)
        {
            throw new System.NotImplementedException();
        }
    }
}