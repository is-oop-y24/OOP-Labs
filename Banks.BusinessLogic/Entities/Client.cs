using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using Kfc.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Banks
{
    public class Client
    {
        private List<Account> _accounts;
        
        private Client()
        {
        }

        public Client(string name, Bank bank, ClientIdentifier identifier)
        {
            bank.ThrowIfNull(nameof(bank));
            identifier.ThrowIfNull(nameof(bank));
            Name = name;
            Bank = bank.ThrowIfNull(nameof(bank));
            Identifier = identifier.ThrowIfNull(nameof(identifier));
            _accounts = new List<Account>();
        }

        public int Id { get; private init; }
        public string Name { get; private init; }
        public ClientIdentifier Identifier { get; private set; }
        
        public Bank Bank { get; private init; }

        [NotMapped]
        public ReadOnlyCollection<Account> Accounts => _accounts.AsReadOnly();
        
        [NotMapped]
        public bool IsDoubtful => !Identifier.IsIdentified;

        public void AddAccount(Account account)
        {
            _accounts.Add(account);
        }

        public void ChangeIdentifier(ClientIdentifier identifier)
        {
            Identifier = identifier.ThrowIfNull(nameof(identifier));
        }
        
        internal void OptionsChanged(Account account, string message)
        {
            // Some notify logic
        }
    }
}