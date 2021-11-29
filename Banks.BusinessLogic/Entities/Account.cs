using System;
using System.ComponentModel.DataAnnotations.Schema;
using Banks.BusinessLogic.Tools;
using Kfc.Utility.Extensions;

namespace Banks
{
    public class Account
    {
        private AccountOptions _options;

        private Account()
        {
        }

        public Account(Client client, AccountOptions options)
        {
            Client = client.ThrowIfNull(nameof(client));
            Options = options.ThrowIfNull(nameof(options));

            Sum = 0;
            ChangesNotify = false;
            LastUpdate = DateTime.Now;
        }

        public int Id { get; private init; }
        public Client Client { get; private init; }
        
        [Column()]
        public decimal Sum { get; private set; }
        public bool ChangesNotify { get; private set; }
        public decimal NextPayout { get; private set; }
        public DateTime LastUpdate { get; private set; }
        
        [NotMapped] public bool IsDoubtful => Client.IsDoubtful;

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

        public void Refresh(DateTime finishDate)
        {
            NextPayout += Options.CalculateAccumulated(LastUpdate, finishDate, Sum);
            LastUpdate = finishDate;
        }

        public void MakePayout()
        {
            Sum += NextPayout;
            NextPayout = 0;
        }
        

        internal Transaction TopUp(decimal sum)
        {
            if (sum <= 0)
                throw new BankException("Sum to top up must be a positive number.");

            Sum += sum;
            Refresh(DateTime.Now);
            return new Transaction(DateTime.Now, source: null, destination: this, sum);
        }

        internal Transaction Withdraw(decimal sum)
        {
            if (sum <= 0)
                throw new BankException("Sum to withdraw up must be a positive number.");
            if (sum > Options.MaxWithdrawSum(Sum))
                throw new BankException("Sum is greater than possible one to withdraw.");

            Sum -= sum;
            Refresh(DateTime.Now);
            return new Transaction(DateTime.Now, source: this, destination: null, sum);
        }

        internal Transaction TransferTo(Account destination, decimal sum)
        {
            destination.ThrowIfNull(nameof(destination));
            Withdraw(sum);
            destination.TopUp(sum);
            Refresh(DateTime.Now);
            return new Transaction(DateTime.Now, source: this, destination, sum);
        }
    }
}