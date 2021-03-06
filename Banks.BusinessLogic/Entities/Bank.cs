using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Banks.BusinessLogic.Tools;
using Kfc.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

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

        [NotMapped]
        public ReadOnlyCollection<Client> Clients => _clients.AsReadOnly();
        [NotMapped]
        public ReadOnlyCollection<Account> Accounts => _accounts.AsReadOnly();
        [NotMapped]
        public ReadOnlyCollection<Transaction> Transactions => _transactions.AsReadOnly();

        public void Refresh(DateTime finishDate)
        {
            _accounts.ForEach(acc => acc.Refresh(finishDate));
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
            
            _transactions.Add(account.TopUp(sum));
        }

        public void Withdraw(Account account, decimal sum)
        {
            account.ThrowIfNull(nameof(account));
            if (account.Client.Bank != this)
                throw new BankException("Account doesnt belong to bank");
            if (account.IsDoubtful && sum > MaxWithdrawForDoubtful)
                throw new BankException("Sum exceed maximum withdraw sum for doubtful account.");

            _transactions.Add(account.Withdraw(sum));
        }

        public void Transfer(Account source, Account destination, decimal sum)
        {
            source.ThrowIfNull(nameof(source));
            destination.ThrowIfNull(nameof(source));
            if (!_accounts.Contains(source) || !_accounts.Contains(destination))
                throw new BankException("One of accounts doesnt belong to bank");
            if (source.IsDoubtful && sum > MaxWithdrawForDoubtful)
                throw new BankException("Sum exceed maximum withdraw sum for doubtful account.");
            
            _transactions.Add(source.TransferTo(destination, sum));
        }

        public void AbortTransaction(Transaction transaction)
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
            client.AddAccount(account);
        }
    }
}