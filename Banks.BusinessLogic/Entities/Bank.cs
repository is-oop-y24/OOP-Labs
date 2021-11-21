using System;
using System.Collections.Generic;
using System.Linq;
using Banks.BusinessLogic.Tools;
using Kfc.Utility.Extensions;

namespace Banks
{
    public class Bank
    {
        private readonly List<Client> _clients;
        private readonly List<Account> _accounts;
        private readonly List<Transaction> _transactions;
        
        public Bank(decimal maxWithdrawForDoubtful)
        {
            _clients = new List<Client>();
            _accounts = new List<Account>();
            _transactions = new List<Transaction>();
            MaxWithdrawForDoubtful = maxWithdrawForDoubtful;
        }

        private Bank()
        {
        }

        public int Id { get; private init; }
        public decimal MaxWithdrawForDoubtful { get; private init; }

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
            Client client = clientBuilder.GetClient(bank: this);
            _clients.Add(client);
            return client;
        }

        public Client GetClient(int clientId)
        {
            Client client = _clients
                .FirstOrDefault(client => client.Id == clientId);
            return client ?? throw new BankException("Client is not registered.");
        }
        
        public Account GetAccount(int accountId)
        {
            Account account = _accounts
                .FirstOrDefault(acc => acc.Id == accountId);
            return account ?? throw new BankException("Account is not registered");
        }

        public void TopUp(Account account, decimal sum)
        {
            account.ThrowIfNull(nameof(account));
            if (account.Client.Bank != this)
                throw new BankException("Account doesnt belong to bank");
            
            Transactions.Add(account.TopUp(sum));
        }

        public void Withdraw(Account account, decimal sum)
        {
            account.ThrowIfNull(nameof(account));
            if (account.Client.Bank != this)
                throw new BankException("Account doesnt belong to bank");
            if (account.IsDoubtful && sum > MaxWithdrawForDoubtful)
                throw new BankException("Sum exceed maximum withdraw sum for doubtful account.");

            Transactions.Add(account.Withdraw(sum));
        }

        public void Transfer(Account source, Account destination, decimal sum)
        {
            source.ThrowIfNull(nameof(source));
            destination.ThrowIfNull(nameof(source));
            if (!_accounts.Contains(source) || !_accounts.Contains(destination))
                throw new BankException("One of accounts doesnt belong to bank");
            if (source.IsDoubtful && sum > MaxWithdrawForDoubtful)
                throw new BankException("Sum exceed maximum withdraw sum for doubtful account.");
            
            Transactions.Add(source.TransferTo(destination, sum));
        }

        public void Abort(Transaction transaction)
        {
            transaction.ThrowIfNull(nameof(transaction));
            if (!_transactions.Contains(transaction))
                throw new BankException("Transaction doesnt belong to bank.");
            
            transaction.Abort();
        }
        

        public void RegisterAccount(Client client, AccountOptions options)
        {
            var account = new Account(client, options);
            _accounts.Add(account);
        }
    }
}