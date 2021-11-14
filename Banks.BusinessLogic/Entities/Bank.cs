using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Banks.DataAccessLayer.Models;

namespace Banks
{
    public class Bank
    {
        private readonly List<Client> _clients;
        private readonly List<Account> _accounts;
        private readonly List<Transaction> _transactions;

        public Bank()
        {
            _accounts = new List<Account>();
            _clients = new List<Client>();
            _transactions = new List<Transaction>();
        }
        internal Bank(List<Client> clients, List<Account> accounts, List<Transaction> transactions)
        {
            _clients = clients; 
            _accounts = accounts;
            _transactions = transactions;
        }

        public int Id { get; init; }

        public ReadOnlyCollection<Client> Clients => _clients.AsReadOnly();
        public ReadOnlyCollection<Account> Accounts => _accounts.AsReadOnly();
        public ReadOnlyCollection<Transaction> Transactions => _transactions.AsReadOnly();
        
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

        public void RegisterAccount(Client client, IAccountOptions options)
        {
            throw new System.NotImplementedException();
        }
    }
}