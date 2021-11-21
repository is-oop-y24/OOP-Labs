using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Kfc.Utility.Extensions;

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
            Accounts = new List<Account>();
        }

        public int Id { get; private init; }
        public string Name { get; private init; }
        public ClientIdentifier Identifier { get; private init; }
        
        public Bank Bank { get; private init; }
        public List<Account> Accounts { 
            get => new (_accounts); 
            init => _accounts = new List<Account>(value); 
        }
        
        [NotMapped]
        public bool IsDoubtful => !Identifier.IsIdentified;

        public void OptionsChanged(Account account, string message)
        {
            // Some notify logic
        }
    }
}