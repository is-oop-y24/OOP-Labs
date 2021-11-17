using Banks.BusinessLogic.Tools;

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
            Client = client;
            Options = options;
        }
        
        public int Id { get; init; }
        public Client Client { get; private init; }
        public decimal Sum { get; private set; }
        public bool Notify { get; private set; }
        public decimal NextPayout { get; private set; }

        public AccountOptions Options
        {
            get => _options;
            private set => _options = value ?? throw new BankException("Account options cannot be null.");
        }

        public void ChangeOptions(AccountOptions options)
        {
            Options = options;
            // TODO Notify logic
        }
        
    }
}