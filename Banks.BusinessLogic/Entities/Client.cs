using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Banks
{
    public class Client
    {
        private List<Account> _accounts;
        
        private Client()
        {
        }

        public Client(string name, Bank bank)
        {
            Name = name;
            Bank = bank;
            Accounts = new List<Account>();
        }

        public int Id { get; private init; }
        public string Name { get; private init; }
        public ClientId Identifier { get; private set; }
        public Bank Bank { get; private init; }
        public List<Account> Accounts { 
            get => new (_accounts); 
            init => _accounts = new List<Account>(value); 
        }
        
        [NotMapped]
        public bool IsDoubtful => (Identifier != null) && Identifier.IsIdentified;

        public void Notify()
        {
            // Some notify logic
        }
    }
}