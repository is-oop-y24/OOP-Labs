using System;
using Banks.BusinessLogic.Tools;
using Kfc.Utility.Extensions;

namespace Banks
{
    public class Account
    {
        private AccountOptions _options;
        private DateTime _lastUpdate;

        private Account() { }

        public Account(Client client, AccountOptions options)
        {
            Client = client.ThrowIfNull(nameof(client));
            Options = options.ThrowIfNull(nameof(options));
            
            Sum = 0;
            ChangesNotify = false;
            _lastUpdate = DateTime.Now;
        }

        public int Id { get; private init; }
        public Client Client { get; private init; }
        public decimal Sum { get; private set; }
        public bool ChangesNotify { get; private set; }
        public decimal NextPayout { get; private set; }
        
        public delegate void AccountHandler(Transaction transaction);
        public event AccountHandler TransactionMade;

        public delegate void ChangesHandler(Account sender, string message);
        public event ChangesHandler OptionsChanged;

        public AccountOptions Options
        {
            get => _options;
            private set => _options = value ?? throw new BankException("Account options cannot be null.");
        }

        public void ChangeOptions(AccountOptions options)
        {
            Options = options;
            OptionsChanged?.Invoke(this, "*some message*");
        }

        public void SubscribeClientToChanges()
        {
            OptionsChanged += Client.OptionsChanged;
            ChangesNotify = true;
        }

        public void UnsubscribeClientFromChanges()
        {
            OptionsChanged -= Client.OptionsChanged;
            ChangesNotify = false;
        }

        public void Refresh()
        {
            DateTime now = DateTime.Now;
            NextPayout += Options.CalculateAccumulated(_lastUpdate, now, Sum);
            _lastUpdate = now;
        }

        public void MakePayout()
        {
            Sum += NextPayout;
            NextPayout = 0;
        }

        public void TopUp(decimal sum)
        {
            TopUp(sum, notify: true);
        }

        internal void TopUp(decimal sum, bool notify)
        {
            if (sum <= 0)
                throw new BankException("Sum to top up must be a positive number.");

            Sum += sum;
            Refresh();
            
            if (notify)
            {
                var transaction = new Transaction(DateTime.Now, source: null, destination: this, sum);
                TransactionMade?.Invoke(transaction);   
            }
        }

        public void Withdraw(decimal sum)
        {
            Withdraw(sum, notify: true);
        }

        internal void Withdraw(decimal sum, bool notify)
        {
            if (sum <= 0)
                throw new BankException("Sum to withdraw up must be a positive number.");
            if (sum > Options.MaxWithdrawSum(sum))
                throw new BankException("Sum is greater than possible one to withdraw.");
            
            Sum -= sum;
            Refresh();

            if (notify)
            {
                var transaction = new Transaction(DateTime.Now, source: this, destination: null, sum);
                TransactionMade?.Invoke(transaction);
            }
        }

        public void TransferTo(Account destination, decimal sum)
        {
            TransferTo(destination, sum, notify: true);
        }

        internal void TransferTo(Account destination, decimal sum, bool notify)
        {
            destination.ThrowIfNull(nameof(destination));
            Withdraw(sum, notify: false);
            destination.TopUp(sum, notify: false);
            Refresh();
            
            if (notify)
            {
                var transaction = new Transaction(DateTime.Now, source: this, destination, sum);
                TransactionMade?.Invoke(transaction);
            }
        }
    }
}